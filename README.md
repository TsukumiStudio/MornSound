# MornSound

## 概要

AudioSourceの再生とボリュームフェード管理を行うライブラリ。

## 依存関係

| 種別 | 名前 |
|------|------|
| 外部パッケージ | UniTask |
| Mornライブラリ | MornGlobal, MornEnum |

## 使い方

### セットアップ

1. Projectウィンドウで右クリック → `Morn/MornSoundGlobal` を作成
2. `AudioMixer` を設定
3. `Infos` にAudioClipごとの設定を追加（音量、ピッチ範囲）
4. `VolumeKeys` / `SourceKeys` を設定

### AudioSourceの取得

```csharp
AudioSource source = sourceType.ToSource();
AudioMixerGroup group = sourceType.ToMixerGroup();
```

### 再生

```csharp
// MornSoundInfoの設定を自動適用して再生
source.MornPlay(clip);
source.MornPlay(clip, volumeScale: 0.5f);

// OneShot再生
source.MornPlayOneShot(clip);
```

### ボリューム関連

```csharp
// 0〜1のレートをデシベルに変換
float db = rate.ToDecibel();

// VolumeTypeからMixerのキー配列を取得
string[] keys = volumeType.ToMixerKeys();
```
