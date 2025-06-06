using System;

namespace MornSound
{
    [Serializable]
    internal struct KeyToVolume
    {
        public MornSoundVolumeType VolumeType;
        public string[] MixerKeys;
    }
}