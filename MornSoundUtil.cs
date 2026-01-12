using System.Threading;
using UnityEngine;
using UnityEngine.Audio;

namespace MornLib
{
    public static class MornSoundUtil
    {
        public static float ToDecibel(this float rate)
        {
            return MornSoundGlobal.I.ToDecibel(rate);
        }

        public static string[] ToMixerKeys(this MornSoundVolumeType volumeType)
        {
            return MornSoundGlobal.I.ToMixerKeys(volumeType);
        }

        public static AudioMixerGroup ToMixerGroup(this MornSoundSourceType sourceType)
        {
            return MornSoundGlobal.I.ToMixerGroup(sourceType);
        }

        public static AudioSource ToSource(this MornSoundSourceType sourceType)
        {
            return MornSoundService.I.GetSource(sourceType);
        }

        public static CancellationToken ToToken(this MornSoundSourceType sourceType)
        {
            return MornSoundService.I.GetFadeToken(sourceType);
        }

        public static bool TryGetInfo(this AudioClip clip, out MornSoundInfo info)
        {
            return MornSoundGlobal.I.TryGetInfo(clip, out info);
        }

        public static void MornPlay(this AudioSource source, AudioClip clip, float volumeScale = 1f)
        {
            if (clip == null)
            {
                return;
            }

            if (clip.TryGetInfo(out var info))
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
                MornSoundGlobal.Logger.LogWarning("指定されたAudioClipがnullです");
                return;
            }

            MornSoundGlobal.Logger.Log("MornPlayOneShot: " + clip.name);
            if (clip.TryGetInfo(out var info))
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