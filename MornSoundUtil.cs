using UnityEngine;

namespace MornSound
{
    public static class MornSoundUtil
    {
        public static void MornPlay(this AudioSource source, AudioClip clip, float volumeScale = 1f)
        {
            if (clip == null)
            {
                return;
            }

            if (MornSoundGlobal.I.TryGetInfo(clip, out var info))
            {
                source.clip = clip;
                source.pitch = info.Pitch;
                source.volume = info.VolumeRate * volumeScale;
                source.Play();
            }
            else
            {
                source.clip = clip;
                source.pitch = 1f;
                source.volume = volumeScale;
                source.Play();
            }
        }

        public static void MornPlayOneShot(this AudioSource source, AudioClip clip, float volumeScale = 1f)
        {
            if (clip == null)
            {
                MornSoundGlobal.LogWarning("指定されたAudioClipがnullです");
                return;
            }

            if (MornSoundGlobal.I.TryGetInfo(clip, out var info))
            {
                source.pitch = info.Pitch;
                source.volume = 1f;
                source.PlayOneShot(clip, info.VolumeRate * volumeScale);
            }
            else
            {
                source.pitch = 1f;
                source.volume = 1f;
                source.PlayOneShot(clip, volumeScale);
            }
        }
    }
}