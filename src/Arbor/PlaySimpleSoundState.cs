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
    [Obsolete("MornSoundPlayOneShotStateやMornSoundPlayOneShotStateを使用してください。")]
    [Serializable]
    public sealed class PlaySimpleSoundState : StateBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _audioClip;

        public override void OnStateBegin()
        {
            _audioSource.MornPlay(_audioClip);
        }
    }
}
#endif // USE_MORNSTATE || USE_ARBOR
