#if USE_ARBOR
using Arbor;
using UnityEngine;

namespace MornLib
{
    internal sealed class MornSoundPlayOneShotState : StateBehaviour
    {
        [SerializeField] private MornSoundSourceType _sourceType;
        [SerializeField] private AudioClip _clip;

        public override void OnStateBegin()
        {
            var source = _sourceType.ToSource();
            source.MornPlayOneShot(_clip);
        }
    }
}
#endif