using System;
using UnityEngine.Audio;

namespace MornLib
{
    [Serializable]
    internal struct KeyToMixerGroup
    {
        public MornSoundSourceType SourceType;
        public AudioMixerGroup MixerGroup;
    }
}