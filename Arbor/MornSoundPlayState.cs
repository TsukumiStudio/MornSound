#if USE_ARBOR
using System.Threading;
using Arbor;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace MornSound
{
    internal sealed class MornSoundPlayState : StateBehaviour
    {
        [SerializeField] private MornSoundSourceType _sourceType;
        [SerializeField] private bool _isRepeat;
        [SerializeField] private bool _forceInitialize;
        [SerializeField] private AudioClip _clip;
        [SerializeField] private float _duration;

        public override void OnStateBegin()
        {
            var ctrl = MornSoundSourceCtrl.I;
            var source = ctrl.GetSource(_sourceType);
            if (source.clip == _clip && source.isPlaying && !_forceInitialize)
            {
                // 既に同じクリップが再生中で強制初期化が無い場合は何もしない
                return;
            }

            var token = ctrl.GetFadeToken(_sourceType);
            if (_duration > 0)
            {
                FadeInAsync(source, _clip, _duration, token).Forget();
            }
            else
            {
                source.MornPlay(_clip);
            }

            source.loop = _isRepeat;
        }

        private async static UniTask FadeInAsync(AudioSource source, AudioClip clip, float duration,
            CancellationToken token)
        {
            if (source == null || clip == null)
            {
                return;
            }

            // 音量を保存して0から開始
            var targetVolume = 1f;
            if (MornSoundGlobal.I.TryGetInfo(clip, out var info))
            {
                targetVolume = info.VolumeRate;
            }

            source.volume = 0f;
            source.MornPlay(clip, 0f); // 音量0で開始
            var elapsed = 0f;
            while (elapsed < duration)
            {
                if (token.IsCancellationRequested)
                {
                    // キャンセルされたら現在の音量を維持
                    return;
                }

                elapsed += Time.deltaTime;
                var t = elapsed / duration;
                source.volume = Mathf.Lerp(0f, targetVolume, t);
                await UniTask.Yield(PlayerLoopTiming.Update, token);
            }

            if (token.IsCancellationRequested)
            {
                return;
            }

            source.volume = targetVolume;
        }
    }
}
#endif