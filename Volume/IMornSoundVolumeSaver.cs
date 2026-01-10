using System;

namespace MornLib
{
    public interface IMornSoundVolumeSaver
    {
        IObservable<MornSoundVolumeType> OnVolumeChanged { get; }
        float Load(MornSoundVolumeType key);
        void Save(MornSoundVolumeType key, float volumeRate);
    }
}