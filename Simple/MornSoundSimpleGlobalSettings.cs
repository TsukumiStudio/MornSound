using System.Collections.Generic;
using UnityEngine;

namespace MornSound
{
    [CreateAssetMenu(fileName = nameof(MornSoundSimpleGlobalSettings),
        menuName = "Morn/" + nameof(MornSoundSimpleGlobalSettings))]
    internal class MornSoundSimpleGlobalSettings : ScriptableObject
    {
        [SerializeField] private List<MornSoundSimpleClipEntity> _clipEntities;
        internal static MornSoundSimpleGlobalSettings Instance { get; private set; }
        internal List<MornSoundSimpleClipEntity> ClipEntities => _clipEntities;

        private void OnEnable()
        {
            Instance = this;
            MornSoundUtil.Log($"{nameof(MornSoundSimpleGlobalSettings)} Loaded");
        }

        private void OnDisable()
        {
            Instance = null;
            MornSoundUtil.Log($"{nameof(MornSoundSimpleGlobalSettings)} Unloaded");
        }
    }
}