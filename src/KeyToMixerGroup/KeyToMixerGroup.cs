using System;
using UnityEngine.Audio;

namespace MornLib
{
    [Serializable]
    internal struct KeyToMixerGroup
    {
        public MornSoundMixerType SourceType;
        public AudioMixerGroup MixerGroup;
    }
}