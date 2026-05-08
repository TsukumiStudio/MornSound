using System;

namespace MornLib
{
    public interface IMornSoundSaver
    {
        IObservable<MornSoundMixerType> OnVolumeChanged { get; }
        float Load(MornSoundMixerType key);
    }
}