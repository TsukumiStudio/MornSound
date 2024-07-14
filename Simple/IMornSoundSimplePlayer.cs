using UnityEngine;

namespace MornSound
{
    public interface IMornSoundSimplePlayer
    {
        void Play(string clipName, float volumeRate = 1);
        void Play(MornSoundSimpleClipEntity clipEntity, float volumeRate = 1);
        void Play(AudioClip clip, float volumeRate = 1);
    }
}