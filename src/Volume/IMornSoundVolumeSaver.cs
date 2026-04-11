using System;

namespace MornLib
{
    public interface IMornSoundVolumeSaver
    {
        IObservable<MornSoundVolumeType> OnVolumeChanged { get; }
        float Load(MornSoundVolumeType key);
    }
}