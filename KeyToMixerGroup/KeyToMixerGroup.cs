using System;
using UnityEngine.Audio;

namespace MornSound
{
    [Serializable]
    internal struct KeyToMixerGroup
    {
        public MornSoundSourceType SourceType;
        public AudioMixerGroup MixerGroup;
    }
}