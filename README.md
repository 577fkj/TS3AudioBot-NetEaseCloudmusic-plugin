# TS3AudioBot-NetEaseCloudmusic-plugin

> 此插件基于 Splamy/TS3AudioBot 项目 https://github.com/Splamy/TS3AudioBot  
> 以及网易云音乐 API 开发https://github.com/Binaryify/NeteaseCloudMusicApi  
> 此插件安装方法同样见 TS3AudioBot 项目 wiki  
> **2.0 版本之后可以不需要本地部署网易云 API 了, 但是强烈建议自行部署防止隐私泄露 **  
> **最好给音乐机器人超管权限保证能正常更新头像和描述**

## 关于 2.0 版本

似乎在播放列表的 bug 应该已经修复了, 如果还有问题请 github 创建 issue。

## 关于设置文件 YunSettings.yml

`version` 配置文件版本, 不要更改！

`playMode` 是播放模式
- `SeqPlay` 顺序播放
- `SeqLoopPlay` 顺序循环
- `RandomPlay` 随机播放
- `RandomLoopPlay` 随机循环

`neteaseApi` 是网易云 API 地址
 
`isQrlogin` 是否验证码登录, 用于判断是否需要刷新Cookie(不需要修改)
`cookieUpdateIntervalMin` 刷新Cookie间隔(分钟)

`autoPause` 无人时候自动暂停

`Header` 请求头配置
- `Cookie` 网易云Cookie
- `User-Agent` 请求UA
需要其他请求头可自行添加

## 目前的指令：

正在播放的歌单的图片和名称可以点机器人看它的头像和描述  
vip 音乐想要先登陆才能播放完整版本:
二维码登录：(输入指令后扫描机器人头像二维码登陆)  
`!yun login qr`
验证码登录：
`!yun login sms [手机号] {验证码}`
- 先使用 `!yun login sms [手机号]` 获取验证码
- 在使用 `!yun login sms [手机号] {验证码}` 登录

双击机器人, 目前有以下指令(把[xxx]替换成对应信息, **包括中括号**)  
1.立即播放网易云音乐  
`!yun play [音乐名称/音乐网址/音乐ID]`

2.添加音乐到下一首  
`!yun add [音乐名称/音乐网址/音乐ID]`

3.播放网易云音乐歌单
`!yun gedan [歌单名称/歌单网址/歌单ID] {长度(默认100, max无限制)}`

4.播放列表中的下一首  
`!yun next`

5.修改播放模式  
`!yun mode [模式选择数字0-3]`  
`0 = 顺序播放`
`1 = 顺序循环`
`2 = 随机播放`
`3 = 随机循环`

6.查看播放列表
`!yun list`

7.清空歌单
`!yun clear`

8.查看状态
`!yun status`

9.重载插件配置
`!yun reload`

10.让机器人前往当前频道
`!here`
需要在服务器聊天框发送

需要注意的是如果歌单歌曲过多需要时间加载, 期间一定一定不要输入其他指令

### TS 频道描述(复制代码到频道描述)

```
[COLOR=#ff5500][B]正在播放的歌单的图片和名称可以点机器人看它的头像和描述[/B][/COLOR]
[COLOR=#aa00ff]机器人现在可以通过歌单播放vip音乐, 如果遇到其他问题可以联系Github[/COLOR]

[COLOR=#0055ff]双击机器人, 目前有以下指令([I]把[xxx]替换成对应信息, 包括中括号[/I])[/COLOR]
1.立即播放网易云音乐
[COLOR=#00aa00]!yun play [音乐名称/音乐网址/音乐ID][/COLOR]
2.添加音乐到下一首
[COLOR=#00aa00]!yun add [音乐名称/音乐网址/音乐ID][/COLOR]
3.播放网易云音乐歌单
[COLOR=#00aa00]!yun gedan [歌单名称/歌单网址/歌单ID][/COLOR]
4.播放列表中的下一首
[COLOR=#00aa00]!yun next[/COLOR]
5.播放模式选择【0=顺序播放 1=顺序循环 2=随机 3=随即循环】
[COLOR=#00aa00]!yun mode[/COLOR]
6.登陆账户
[COLOR=#00aa00]!yun login[/COLOR]
7.查看播放列表
[COLOR=#00aa00]!yun list[/COLOR]
8.查看状态
[COLOR=#00aa00]!yun status[/COLOR]
需要注意的是如果歌单歌曲过多需要时间加载(重写后应该只需要几秒), 期间[B]一定一定不要[/B]输入其他指令
```
