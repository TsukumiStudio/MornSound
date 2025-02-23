using System;

namespace MornSound
{
    public interface IMornSoundVolumeSaver
    {
        IObservable<MornSoundVolumeType> OnVolumeChanged { get; }
        float Load(MornSoundVolumeType key);
        void Save(MornSoundVolumeType key, float volumeRate);
    }
}