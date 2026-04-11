using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace MornLib
{
    [AddComponentMenu("")]
    internal sealed class MornSoundVolumeSolver : MonoBehaviour
    {
        private IMornSoundVolumeSaver _saver;
        private readonly Dictionary<string, float> _fadeRateDict = new();
        private readonly Dictionary<string, CancellationTokenSource> _ctsDict = new();
        private const float DefaultFadeRate = 1;

        public void Initialize(IMornSoundVolumeSaver saver)
        {
            _saver = saver;
            _saver.OnVolumeChanged.Subscribe(ApplyVolume).AddTo(this);
        }

        private async void Start()
        {
            if (_saver == null)
            {
                MornSoundGlobal.Logger.LogError("Initializeされていません");
                return;
            }

            // 1F待ってから反映
            await UniTask.DelayFrame(1);
            var tmpKey = new MornSoundVolumeType();
            foreach (var key in MornSoundGlobal.I.VolumeKeys)
            {
                tmpKey.Key = key;
                ApplyVolume(tmpKey);
            }
        }

        private void ApplyVolume(MornSoundVolumeType soundVolumeType)
        {
            var fadeRate = _fadeRateDict.GetValueOrDefault(soundVolumeType.Key, DefaultFadeRate);
            var saveValue = _saver.Load(soundVolumeType);
            var volumeDecibel = (saveValue * fadeRate).ToDecibel();
            foreach (var mixerKey in soundVolumeType.ToMixerKeys())
            {
                MornSoundGlobal.I.Mixer.SetFloat(mixerKey, volumeDecibel);
            }
        }

        public async UniTask FadeAsync(MornSoundVolumeFadeInfo fadeInfo)
        {
            if (_ctsDict.TryGetValue(fadeInfo.SoundVolumeType.Key, out var cts))
            {
                cts.Cancel();
            }

            var key = fadeInfo.SoundVolumeType.Key;
            _ctsDict[key] = CancellationTokenSource.CreateLinkedTokenSource(fadeInfo.CancellationToken);
            var token = _ctsDict[key].Token;
            var duration = fadeInfo.Duration ?? 0;
            var easeType = fadeInfo.EaseType ?? MornEaseType.Linear;
            var startValue = _fadeRateDict.GetValueOrDefault(fadeInfo.SoundVolumeType.Key, DefaultFadeRate);
            var aimValue = fadeInfo.IsFadeIn ? 1 : 0;
            var isSkip = Mathf.Approximately(startValue, aimValue) || duration <= 0;
            if (!isSkip)
            {
                var startTime = Time.time;
                duration *= Mathf.Abs(startValue - aimValue);
                while (Time.time - startTime < duration)
                {
                    var timeRate = (Time.time - startTime) / duration;
                    var rate = Mathf.Clamp01(timeRate).Ease(easeType);
                    _fadeRateDict[fadeInfo.SoundVolumeType.Key] = Mathf.Lerp(startValue, aimValue, rate);
                    ApplyVolume(fadeInfo.SoundVolumeType);
                    await UniTask.Yield(cancellationToken: token);
                }
            }

            _fadeRateDict[fadeInfo.SoundVolumeType.Key] = aimValue;
            ApplyVolume(fadeInfo.SoundVolumeType);
        }

        public void FadeImmediate(MornSoundVolumeType volumeType, bool isFadeIn)
        {
            if (_ctsDict.TryGetValue(volumeType.Key, out var cts))
            {
                cts.Cancel();
                cts.Dispose();
                _ctsDict.Remove(volumeType.Key);
            }

            _fadeRateDict[volumeType.Key] = isFadeIn ? 1 : 0;
            ApplyVolume(volumeType);
        }
    }
}