using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
#if UNITY_EDITOR
using UnityEditor;
#endif

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
#if UNITY_EDITOR
            var preloadedAssets = PlayerSettings.GetPreloadedAssets().ToList();
            if (preloadedAssets.Contains(this) &&
                preloadedAssets.Count(x => x is MornSoundSimpleGlobalSettings) == 1) return;
            preloadedAssets.RemoveAll(x => x is MornSoundSimpleGlobalSettings);
            preloadedAssets.Add(this);
            PlayerSettings.SetPreloadedAssets(preloadedAssets.ToArray());
            MornSoundUtil.Log("Global Settings Added to Preloaded Assets!");
#endif
        }

        private void OnDisable()
        {
            Instance = null;
            MornSoundUtil.Log("Global Settings Unloaded");
        }
    }
}