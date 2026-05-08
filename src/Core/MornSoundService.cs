using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace MornLib
{
    public sealed class MornSoundService : MornGlobalMonoBase<MornSoundService>
    {
        private const float DefaultFadeRate = 1;
        private readonly Dictionary<string, AudioSource> _audioSourceCache = new();
        private readonly Dictionary<string, CancellationTokenSource> _fadeTokenCache = new();
        private readonly Dictionary<string, float> _fadeRateDict = new();
        private readonly Dictionary<string, CancellationTokenSource> _ctsDict = new();
        private IMornSoundSaver _saver;
        protected override string ModuleName => "MornSound";

        protected override void OnInitialized()
        {
        }

        public void Initialize(IMornSoundSaver saver)
        {
            _saver = saver;
            _saver.OnVolumeChanged.Subscribe(ApplyVolume).AddTo(this);
            ApplyAllVolumesDelayed().Forget();
        }

        private async UniTaskVoid ApplyAllVolumesDelayed()
        {
            await UniTask.DelayFrame(1, cancellationToken: destroyCancellationToken);
            var tmpKey = new MornSoundMixerType();
            foreach (var key in MornSoundGlobal.I.MixerKeys)
            {
                tmpKey.Key = key;
                ApplyVolume(tmpKey);
            }
        }

        private void ApplyVolume(MornSoundMixerType soundVolumeType)
        {
            if (_saver == null) return;
            var fadeRate = _fadeRateDict.GetValueOrDefault(soundVolumeType.Key, DefaultFadeRate);
            var saveValue = _saver.Load(soundVolumeType);
            var volumeDecibel = (saveValue * fadeRate).ToDecibel();
            foreach (var mixerKey in soundVolumeType.ToMixerKeys())
            {
                MornSoundGlobal.I.Mixer.SetFloat(mixerKey, volumeDecibel);
            }
        }

        public async UniTask FadeAsync(MornSoundFadeInfo fadeInfo)
        {
            if (_ctsDict.TryGetValue(fadeInfo.SoundVolumeType.Key, out var existingCts))
            {
                existingCts.Cancel();
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

        public void FadeImmediate(MornSoundFadeInfo fadeInfo)
        {
            FadeImmediate(fadeInfo.SoundVolumeType, fadeInfo.IsFadeIn);
        }

        public void FadeImmediate(MornSoundMixerType volumeType, bool isFadeIn)
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

        internal AudioSource GetSource(MornSoundMixerType sourceType)
        {
            var key = sourceType.Key;
            if (!_audioSourceCache.TryGetValue(key, out var audioSource) || audioSource == null)
            {
                var child = transform.Find(key);
                if (child == null)
                {
                    child = new GameObject(key).transform;
                    child.SetParent(transform);
                }

                audioSource = child.GetComponent<AudioSource>();
                if (audioSource == null)
                {
                    audioSource = child.gameObject.AddComponent<AudioSource>();
                }

                _audioSourceCache[key] = audioSource;
            }

            audioSource.outputAudioMixerGroup = sourceType.ToMixerGroup();
            return audioSource;
        }

        internal CancellationToken GetFadeToken(MornSoundMixerType sourceType)
        {
            var key = sourceType.Key;
            if (_fadeTokenCache.TryGetValue(key, out var existingCts))
            {
                existingCts.Cancel();
                existingCts.Dispose();
            }

            var cts = new CancellationTokenSource();
            _fadeTokenCache[key] = cts;
            return cts.Token;
        }

        private void OnDestroy()
        {
            foreach (var cts in _fadeTokenCache.Values)
            {
                cts?.Cancel();
                cts?.Dispose();
            }
            _fadeTokenCache.Clear();
            foreach (var cts in _ctsDict.Values)
            {
                cts?.Cancel();
                cts?.Dispose();
            }
            _ctsDict.Clear();
        }
    }
}
