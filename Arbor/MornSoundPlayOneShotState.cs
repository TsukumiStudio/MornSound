#if USE_ARBOR
using Arbor;
using UnityEngine;

namespace MornSound
{
    internal sealed class MornSoundPlayOneShotState : StateBehaviour
    {
        [SerializeField] private MornSoundSourceType _sourceType;
        [SerializeField] private AudioClip _clip;

        public override void OnStateBegin()
        {
            var ctrl = MornSoundSourceCtrl.I;
            var source = ctrl.GetSource(_sourceType);
            source.MornPlayOneShot(_clip);
        }
    }
}
#endif