using System.Collections.Generic;
using UnityEngine;

namespace MornSound
{
    public sealed class MornSoundSimpleCore : IMornSoundSimplePlayer
    {
        private readonly Dictionary<string, MornSoundSimpleClipEntity> _cachedSoundDict;
        private readonly MornSoundSimplePool _pool;

        public MornSoundSimpleCore(Transform parent)
        {
            _pool = new MornSoundSimplePool(parent);
            _cachedSoundDict = new Dictionary<string, MornSoundSimpleClipEntity>();
            foreach (var clipEntity in MornSoundSimpleGlobalSettings.Instance.ClipEntities)
                _cachedSoundDict.Add(clipEntity.name, clipEntity);
        }

        public void Play(string clipName, float volumeRate = 1)
        {
            if (!_cachedSoundDict.TryGetValue(clipName, out var clipEntity))
            {
                MornSoundUtil.LogError($"Clip {clipName} not found");
                return;
            }

            Play(clipEntity, volumeRate);
        }

        public void Play(MornSoundSimpleClipEntity clipEntity, float volumeRate = 1)
        {
            var soundPlayer = _pool.Rent();
            soundPlayer.Play(
                new MornSoundSimpleConfig
                {
                    MixerGroup = clipEntity.AudioMixerGroup,
                    Clip = clipEntity.AudioClip,
                    Pitch = clipEntity.Pitch,
                    Volume = clipEntity.VolumeRate * volumeRate
                }, _pool.Return);
        }
    }
}