using System.Collections.Generic;
using UnityEngine;

namespace MornSound
{
    public sealed class MornSoundSimpleCore : IMornSoundSimple
    {
        private readonly Dictionary<string, AudioClip> _cachedClipDict;
        private readonly Dictionary<string, MornSoundSimpleClipEntity> _cachedClipEntitiesDict;
        private readonly Dictionary<string, float> _lastPlayTimeCache = new();
        private readonly MornSoundSimplePool _pool;
        private const float DuplicateInterval = 0.03f;

        public MornSoundSimpleCore(Transform parent)
        {
            _pool = new MornSoundSimplePool(parent);
            _cachedClipEntitiesDict = new Dictionary<string, MornSoundSimpleClipEntity>();
            foreach (var clipEntity in MornSoundGlobal.I.ClipEntities) _cachedClipEntitiesDict.Add(clipEntity.name, clipEntity);
            _cachedClipDict = new Dictionary<string, AudioClip>();
            foreach (var clip in MornSoundGlobal.I.Clips) _cachedClipDict.Add(clip.name, clip);
            foreach (var key in _cachedClipDict.Keys)
            {
                if (_cachedClipEntitiesDict.ContainsKey(key))
                {
                    MornSoundGlobal.I.LogError($"Duplicate key {key}.");
                }
            }
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

            MornSoundGlobal.I.LogWarning($"Clip name {clipName} not found.");
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
            if (_lastPlayTimeCache.TryGetValue(clipEntity.AudioClip.name, out var lastPlayTime) && Mathf.Abs(Time.unscaledTime - lastPlayTime) < DuplicateInterval)
            {
                return;
            }

            _lastPlayTimeCache[clipEntity.AudioClip.name] = Time.unscaledTime;
            var soundPlayer = _pool.Rent();
            soundPlayer.Play(new MornSoundSimpleConfig { MixerGroup = MornSoundGlobal.I.AudioMixerGroup, Clip = clipEntity.AudioClip, Pitch = clipEntity.Pitch, Volume = clipEntity.VolumeRate * volumeRate }, _pool.Return);
        }

        private void Play(AudioClip clip, float volumeRate = 1)
        {
            if (_lastPlayTimeCache.TryGetValue(clip.name, out var lastPlayTime) && Mathf.Abs(Time.unscaledTime - lastPlayTime) < DuplicateInterval)
            {
                return;
            }

            _lastPlayTimeCache[clip.name] = Time.unscaledTime;
            var soundPlayer = _pool.Rent();
            soundPlayer.Play(new MornSoundSimpleConfig { MixerGroup = MornSoundGlobal.I.AudioMixerGroup, Clip = clip, Pitch = 1, Volume = volumeRate }, _pool.Return);
        }
    }
}