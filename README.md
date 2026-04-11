# MornSound

<p align="center">
  <img src="src/Editor/MornSound.png" alt="MornSound" width="640" />
</p>

<p align="center">
  <img src="https://img.shields.io/github/license/TsukumiStudio/MornSound" alt="License" />
</p>

## 概要

Unity の AudioSource / AudioMixer を統一して扱うサウンドシステムラッパー。Volume / Source 種別の文字列キー管理、フェード、Arbor 連携などを提供する。

## 導入方法

Unity Package Manager で以下の Git URL を追加:

```
https://github.com/TsukumiStudio/MornSound.git?path=src#1.0.0
```

`Window > Package Manager > + > Add package from git URL...` に貼り付けてください。

### 依存パッケージ

- [UniTask](https://github.com/Cysharp/UniTask) (`com.cysharp.unitask`)
- [UniRx](https://github.com/neuecc/UniRx) (`com.neuecc.unirx`)
- [Arbor](https://arbor.caitsithware.com/) (Arbor State 連携用)
- [MornGlobal](https://github.com/TsukumiStudio/MornGlobal) (`com.tsukumistudio.mornglobal`)
- [MornEnum](https://github.com/TsukumiStudio/MornEnum) (`com.tsukumistudio.mornenum`)

## ライセンス

[The Unlicense](LICENSE)
