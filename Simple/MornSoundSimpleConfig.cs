using UnityEngine;
using UnityEngine.Audio;

namespace MornSound
{
    internal struct MornSoundSimpleConfig
    {
        internal AudioMixerGroup MixerGroup;
        internal AudioClip Clip;
        internal float Pitch;
        internal float Volume;
    }
}