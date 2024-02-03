﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TS3AudioBot;
using TS3AudioBot.Audio;
using TS3AudioBot.Helper;
using TS3AudioBot.ResourceFactories;

public class PlayControl
{
    private PlayManager playManager;
    private InvokerData invoker;
    private Ts3Client ts3Client;
    private List<MusicInfo> songList = new List<MusicInfo>();
    private int randomOffset = 0;
    private NLog.Logger Log;
    private string neteaseApi;
    private string neteaseApiUNM;
    private Mode mode;
    private int currentPlay = 0;
    private string cookies = "";
    private MusicInfo currentPlayMusicInfo;

    public PlayControl(PlayManager playManager, Ts3Client ts3Client, NLog.Logger log, string neteaseApi, string neteaseApiUNM)
    {
        Log = log;
        this.neteaseApi = neteaseApi;
        this.playManager = playManager;
        this.ts3Client = ts3Client;
        this.neteaseApiUNM = neteaseApiUNM;
    }

    public MusicInfo GetCurrentPlayMusicInfo()
    {
        return currentPlayMusicInfo;
    }

    public string GetCookies()
    {
        return cookies;
    }

    public void SetCookies(string cookies)
    {
        this.cookies = cookies;
    }

    public InvokerData Getinvoker()
    {
        return this.invoker;
    }

    public void SetInvoker(InvokerData invoker)
    {
        this.invoker = invoker;
    }

    public Mode GetMode() { 
        return this.mode;
    }

    public void SetMode(Mode mode) {
        this.mode = mode;
    }

    public void SetPlayList(List<MusicInfo> list)
    {
        songList = new List<MusicInfo>(list);
        currentPlay = 0;
        if (mode == Mode.RandomPlay || mode == Mode.RandomLoopPlay) {
            Utils.ShuffleArrayList(songList);
        }
    }

    public List<MusicInfo> GetPlayList()
    {
        return songList;
    }

    public void AddMusic(MusicInfo musicInfo)
    {
        songList.Insert(0, musicInfo);
    }

    public async Task PlayNextMusic()
    {
        if (songList.Count == 0)
        {
            return;
        }
        var musicInfo = GetNextMusic();
        await PlayMusic(musicInfo);
    }

    private async Task PlayMusic(MusicInfo musicInfo)
    {
        var invoker = Getinvoker();

        currentPlayMusicInfo = musicInfo;

        await musicInfo.InitMusicInfo(neteaseApi);
        string musicUrl = await musicInfo.getMusicUrl(neteaseApi, neteaseApiUNM, cookies);
        Log.Info($"Music name: {musicInfo.name}, picUrl: {musicInfo.img}, url: {musicUrl}");

        if (musicUrl == "error")
        {
            await ts3Client.SendChannelMessage($"音乐链接获取失败 [{musicInfo.name}]");
            await PlayNextMusic();
            return;
        }

        await playManager.Play(invoker, new MediaPlayResource(musicUrl, musicInfo.GetMusicInfo(), await musicInfo.GetImage(), false));

        string author = musicInfo.author != "" ? $" - {musicInfo.author}" : "";
        await ts3Client.SendChannelMessage($"► 正在播放：[URL={musicInfo.detailUrl}]{musicInfo.name}[/URL]{author}");

        // 获取后面三首歌
        //var musicList = GetNextPlayList();
        //var desc = $"当前正在播放：{musicInfo.name}\n播放列表[{currentPlay}/{songList.Count}]\n";
        //for (var i = 0; i < musicList.Count; i++)
        //{
        //    var music = musicList[i];
        //    music.initMusicInfo(neteaseApi);
        //    desc += $"{i}: {music.name}\n";
        //}
        var desc = $"[{currentPlay}/{songList.Count}] {musicInfo.name}";
        await ts3Client.ChangeDescription(desc);
        await MainCommands.CommandBotAvatarSet(ts3Client, musicInfo.img);
    }

    public List<MusicInfo> GetNextPlayList(int limit = 3) {
        var list = new List<MusicInfo>();
        if (songList.Count <= limit) {
            limit = songList.Count - 1;
        }
        for (int i = 0; i < limit; i++)
        {
            list.Add(songList[i]);
        }
        return list;
    }

    private MusicInfo GetNextMusic()
    {
        MusicInfo result;
        switch (mode)
        {
            case Mode.SeqPlay:
                result = songList[0];
                songList.RemoveAt(0);
                break;
            case Mode.SeqLoopPlay:
                result = songList[0];
                songList.RemoveAt(0);
                songList.Add(result);
                break;

            case Mode.RandomPlay: // 随机播放
            case Mode.RandomLoopPlay:
                if (randomOffset < 0)
                {
                    randomOffset = songList.Count - 1;
                    Utils.ShuffleArrayList(songList);
                    currentPlay = 0;
                }
                randomOffset -= 1;

                result = songList[0];
                songList.RemoveAt(0);
                if (mode == Mode.RandomLoopPlay) {
                    songList.Add(result);
                }
                break;

            default:
                Log.Error($"Mode is not found! {mode}");
                result = songList[0];
                songList.RemoveAt(0);
                break;
        }
        currentPlay += 1;
        return result;
    }

    public async Task<string> GetPlayListString()
    {
        var musicList = GetNextPlayList();
        var musicInfo = GetCurrentPlayMusicInfo();
        var desc = $"\n当前正在播放：{musicInfo.name}\n播放列表 [{currentPlay}/{songList.Count}]\n";
        for (var i = 0; i < musicList.Count; i++)
        {
            var music = musicList[i];
            await music.InitMusicInfo(neteaseApi);
            desc += $"{i + 1}: {music.name}\n";
        }
        return desc;
    }
}
