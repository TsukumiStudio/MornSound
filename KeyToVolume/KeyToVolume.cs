using System;

namespace MornLib
{
    [Serializable]
    internal struct KeyToVolume
    {
        public MornSoundVolumeType VolumeType;
        public string[] MixerKeys;
    }
}