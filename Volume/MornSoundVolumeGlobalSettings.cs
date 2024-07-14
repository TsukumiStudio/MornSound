using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace MornSound
{
    [CreateAssetMenu(fileName = nameof(MornSoundVolumeGlobalSettings),
        menuName = "Morn/" + nameof(MornSoundVolumeGlobalSettings))]
    internal class MornSoundVolumeGlobalSettings : ScriptableObject
    {
        [SerializeField] private AudioMixer _mixer;
        [SerializeField] private List<string> _volumeKeys;
        internal static MornSoundVolumeGlobalSettings Instance { get; private set; }
        internal AudioMixer Mixer => _mixer;
        internal IReadOnlyList<string> VolumeKeys => _volumeKeys;

        private void OnEnable()
        {
            Instance = this;
            MornSoundUtil.Log($"{nameof(MornSoundVolumeGlobalSettings)} Loaded");
        }

        private void OnDisable()
        {
            Instance = null;
            MornSoundUtil.Log($"{nameof(MornSoundVolumeGlobalSettings)} Unloaded");
        }
    }
}