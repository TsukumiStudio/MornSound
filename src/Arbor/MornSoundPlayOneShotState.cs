#if USE_MORNSTATE || USE_ARBOR
#if USE_MORNSTATE
using MornLib;
using StateBehaviour = MornLib.MornStateBehaviour;
#elif USE_ARBOR
using Arbor;
#endif
using System;
using UnityEngine;

namespace MornLib
{
    [Serializable]
    internal sealed class MornSoundPlayOneShotState : StateBehaviour
    {
        [SerializeField] private MornSoundMixerType _sourceType;
        [SerializeField] private AudioClip _clip;

        public override void OnStateBegin()
        {
            var source = _sourceType.ToSource();
            source.MornPlayOneShot(_clip);
        }
    }
}
#endif // USE_MORNSTATE || USE_ARBOR
