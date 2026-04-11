#if USE_ARBOR
using System;
using Arbor;
using UnityEngine;

namespace MornLib
{
    [Obsolete("MornSoundPlayOneShotStateやMornSoundPlayOneShotStateを使用してください。")]
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
#endif