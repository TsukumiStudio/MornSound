#if USE_ARBOR
using System.Threading;
using Arbor;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace MornSound
{
    internal sealed class MornSoundStopState : StateBehaviour
    {
        [SerializeField] private MornSoundSourceType _sourceType;
        [SerializeField] private float _duration;

        public override void OnStateBegin()
        {
            var ctrl = MornSoundSourceCtrl.I;
            var source = ctrl.GetSource(_sourceType);
            var token = ctrl.GetFadeToken(_sourceType);
            if (_duration > 0)
            {
                FadeOutAsync(source, _duration, token).Forget();
            }
            else
            {
                source.Stop();
            }
        }

        private async static UniTask FadeOutAsync(AudioSource source, float duration, CancellationToken token)
        {
            if (source == null || !source.isPlaying)
            {
                return;
            }

            var startVolume = source.volume;
            var elapsed = 0f;
            while (elapsed < duration)
            {
                if (token.IsCancellationRequested)
                {
                    return;
                }

                elapsed += Time.deltaTime;
                var t = elapsed / duration;
                source.volume = Mathf.Lerp(startVolume, 0f, t);
                await UniTask.Yield(PlayerLoopTiming.Update, token);
            }

            if (token.IsCancellationRequested)
            {
                return;
            }

            source.Stop();
            source.volume = startVolume;
        }
    }
}
#endif