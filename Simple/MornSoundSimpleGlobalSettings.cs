using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace MornSound
{
    [CreateAssetMenu(fileName = nameof(MornSoundSimpleGlobalSettings),
        menuName = "Morn/" + nameof(MornSoundSimpleGlobalSettings))]
    public class MornSoundSimpleGlobalSettings : ScriptableObject
    {
        [SerializeField] private AudioMixerGroup _audioMixerGroup;
        [SerializeField] private List<MornSoundSimpleClipEntity> _clipEntities;
        [SerializeField] private List<AudioClip> _clips;
        internal static MornSoundSimpleGlobalSettings Instance { get; private set; }
        internal AudioMixerGroup AudioMixerGroup => _audioMixerGroup;
        internal List<MornSoundSimpleClipEntity> ClipEntities => _clipEntities;
        internal List<AudioClip> Clips => _clips;

        private void OnEnable()
        {
            Instance = this;
            MornSoundUtil.Log("Global Settings Loaded");
        }

        private void OnDisable()
        {
            Instance = null;
            MornSoundUtil.Log("Global Settings Unloaded");
        }
    }
}