using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace MornSound
{
    internal class MornSoundVolumeSolver : MonoBehaviour, IMornSoundVolume
    {
        [SerializeField] private float _defaultFadeInDuration = 0.3f;
        [SerializeField] private float _defaultFadeOutDuration = 0.6f;
        private readonly Subject<FadeVolumePair> _fadeUpdateSubject = new();
        private readonly Dictionary<string, FadeVolumePair> _fadeVolumeDict = new();
        private CancellationTokenSource _cts;
        private Action _onStart;
        public IObservable<FadeVolumePair> OnFadeUpdate => _fadeUpdateSubject;

        private void Start()
        {
            _onStart?.Invoke();
        }

        void IMornSoundVolume.FadeInImmediate(string key)
        {
            FadeFillAsync(key, 0).Forget();
        }

        void IMornSoundVolume.FadeIn(string key)
        {
            FadeFillAsync(key, _defaultFadeInDuration).Forget();
        }

        void IMornSoundVolume.FadeIn(string key, float duration)
        {
            FadeFillAsync(key, duration).Forget();
        }

        async UniTask IMornSoundVolume.FadeInAsync(string key, CancellationToken ct)
        {
            await FadeFillAsync(key, _defaultFadeInDuration, ct);
        }

        UniTask IMornSoundVolume.FadeInAsync(string key, float duration, CancellationToken ct)
        {
            return FadeFillAsync(key, duration, ct);
        }

        void IMornSoundVolume.FadeOutImmediate(string key)
        {
            FadeClearAsync(key, 0).Forget();
        }

        void IMornSoundVolume.FadeOut(string key)
        {
            FadeClearAsync(key, _defaultFadeOutDuration).Forget();
        }

        async UniTask IMornSoundVolume.FadeOutAsync(string key, CancellationToken ct)
        {
            await FadeClearAsync(key, _defaultFadeOutDuration, ct);
        }

        async UniTask IMornSoundVolume.FadeOutAsync(string key, float duration, CancellationToken ct)
        {
            await FadeClearAsync(key, duration, ct);
        }

        public void Initialize(Action onStart)
        {
            _onStart = onStart;
        }

        private async UniTask FadeClearAsync(string key, float duration, CancellationToken ct = default)
        {
            _cts?.Cancel();
            _cts = CancellationTokenSource.CreateLinkedTokenSource(ct);
            if (!_fadeVolumeDict.TryGetValue(key, out var pair))
            {
                pair = new FadeVolumePair(key);
                _fadeVolumeDict.Add(key, pair);
            }

            await VolumeTweenTask(pair, pair.Volume, 0, duration, _cts.Token);
        }

        private async UniTask FadeFillAsync(string key, float duration, CancellationToken ct = default)
        {
            _cts?.Cancel();
            _cts = CancellationTokenSource.CreateLinkedTokenSource(ct);
            if (!_fadeVolumeDict.TryGetValue(key, out var pair))
            {
                pair = new FadeVolumePair(key);
                _fadeVolumeDict.Add(key, pair);
            }

            await VolumeTweenTask(pair, pair.Volume, 1, duration, _cts.Token);
        }

        private async UniTask VolumeTweenTask(FadeVolumePair pair, float startVolume, float endVolume, float duration,
            CancellationToken ct)
        {
            var elapsedTime = 0f;
            while (elapsedTime < duration)
            {
                elapsedTime += Time.unscaledDeltaTime;
                pair.Volume = Mathf.Lerp(startVolume, endVolume, elapsedTime / duration);
                _fadeUpdateSubject.OnNext(pair);
                await UniTask.Yield(ct);
            }

            pair.Volume = endVolume;
            _fadeUpdateSubject.OnNext(pair);
        }
    }
}