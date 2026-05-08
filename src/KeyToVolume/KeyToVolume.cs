using System;

namespace MornLib
{
    [Serializable]
    internal struct KeyToVolume
    {
        public MornSoundMixerType VolumeType;
        [NoLabel] public string[] MixerKeys;
    }
}