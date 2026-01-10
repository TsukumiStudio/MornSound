# MornSound

AudioSourceの再生とボリューム管理を行うライブラリ

## 依存関係

- UniTask
- MornGlobal
- MornEnum

## セットアップ

1. `Project`を右クリック → `Morn/MornSoundGlobal`を作成
2. `AudioMixer`を設定
3. `Infos`にAudioClipごとの設定を追加可能（音量、ピッチ範囲）
4. `VolumeKeys` / `SourceKeys`を設定可能

## 使い方

### AudioSourceの取得

```csharp
// MornSoundSourceTypeからAudioSourceを取得
AudioSource source = sourceType.ToSource();

// AudioMixerGroupを取得
AudioMixerGroup group = sourceType.ToMixerGroup();
```

### 再生

```csharp
// MornSoundInfoの設定（音量・ピッチ）を自動適用して再生
source.MornPlay(clip);
source.MornPlay(clip, volumeScale: 0.5f);

// OneShot再生
source.MornPlayOneShot(clip);
source.MornPlayOneShot(clip, volumeScale: 0.5f);
```

### ボリューム関連

```csharp
// 0〜1のレートをデシベルに変換
float db = rate.ToDecibel();

// VolumeTypeからMixerのキー配列を取得
string[] keys = volumeType.ToMixerKeys();
```

### その他

```csharp
// フェード用CancellationTokenの取得
CancellationToken token = sourceType.ToToken();

// AudioClipからMornSoundInfo取得
if (clip.TryGetInfo(out var info))
{
    // info.VolumeRate, info.Pitch
}
```

## 主要クラス

| クラス | 機能 |
|---|---|
| `MornSoundUtil` | 拡張メソッド群 |
| `MornSoundService` | AudioSourceのシングルトン管理 |
| `MornSoundVolumeCore` | ボリュームフェード制御 |
| `MornSoundInfo` | AudioClipごとの設定（音量、ピッチ範囲） |

## Arbor対応

`Arbor`フォルダにステートマシン用のステートあり
