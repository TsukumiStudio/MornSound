using UnityEngine;

namespace MornSound
{
    [CreateAssetMenu(fileName = nameof(MornSoundGlobalSettings), menuName = "Morn/" + nameof(MornSoundGlobalSettings))]
    internal class MornSoundGlobalSettings : ScriptableObject
    {
        internal MornSoundGlobalSettings Instance { get; private set; }

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