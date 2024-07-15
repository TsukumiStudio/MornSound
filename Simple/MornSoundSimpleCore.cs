using System.Collections.Generic;
using UnityEngine;

namespace MornSound
{
    public sealed class MornSoundSimpleCore : IMornSoundSimple
    {
        private readonly Dictionary<string, AudioClip> _cachedClipDict;
        private readonly Dictionary<string, MornSoundSimpleClipEntity> _cachedClipEntitiesDict;
        private readonly MornSoundSimplePool _pool;

        public MornSoundSimpleCore(Transform parent)
        {
            _pool = new MornSoundSimplePool(parent);
            _cachedClipEntitiesDict = new Dictionary<string, MornSoundSimpleClipEntity>();
            foreach (var clipEntity in MornSoundSimpleGlobalSettings.Instance.ClipEntities)
                _cachedClipEntitiesDict.Add(clipEntity.name, clipEntity);
            _cachedClipDict = new Dictionary<string, AudioClip>();
            foreach (var clip in MornSoundSimpleGlobalSettings.Instance.Clips) _cachedClipDict.Add(clip.name, clip);
            foreach (var key in _cachedClipDict.Keys)
                if (_cachedClipEntitiesDict.ContainsKey(key))
                    MornSoundUtil.LogError($"Duplicate key {key}.");
        }

        void IMornSoundSimple.Play(string clipName, float volumeRate = 1)
        {
            if (_cachedClipEntitiesDict.TryGetValue(clipName, out var clipEntity))
            {
                Play(clipEntity, volumeRate);
                return;
            }

            if (_cachedClipDict.TryGetValue(clipName, out var clip))
            {
                Play(clip, volumeRate);
                return;
            }

            MornSoundUtil.LogWarning($"Clip name {clipName} not found.");
        }

        void IMornSoundSimple.Play(MornSoundSimpleClipEntity clipEntity, float volumeRate = 1)
        {
            Play(clipEntity, volumeRate);
        }

        void IMornSoundSimple.Play(AudioClip clip, float volumeRate = 1)
        {
            Play(clip, volumeRate);
        }
        
        private void Play(MornSoundSimpleClipEntity clipEntity, float volumeRate = 1)
        {
            var soundPlayer = _pool.Rent();
            soundPlayer.Play(
                new MornSoundSimpleConfig
                {
                    MixerGroup = MornSoundSimpleGlobalSettings.Instance.AudioMixerGroup,
                    Clip = clipEntity.AudioClip,
                    Pitch = clipEntity.Pitch,
                    Volume = clipEntity.VolumeRate * volumeRate
                }, _pool.Return);
        }

        private void Play(AudioClip clip, float volumeRate = 1)
        {
            var soundPlayer = _pool.Rent();
            soundPlayer.Play(
                new MornSoundSimpleConfig
                {
                    MixerGroup = MornSoundSimpleGlobalSettings.Instance.AudioMixerGroup,
                    Clip = clip,
                    Pitch = 1,
                    Volume = volumeRate
                }, _pool.Return);
        }
    }
}