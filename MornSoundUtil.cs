using UnityEngine;

namespace MornSound
{
    internal static class MornSoundUtil
    {
#if DISABLE_MORN_SOUND_LOG
        private const bool ShowLOG = false;
#else
        private const bool ShowLOG = true;
#endif

        private const string Prefix = "[MornSound] ";

        internal static void Log(string message)
        {
            if (ShowLOG) Debug.Log(Prefix + message);
        }

        internal static void LogError(string message)
        {
            if (ShowLOG) Debug.LogError(Prefix + message);
        }

        internal static void LogWarning(string message)
        {
            if (ShowLOG) Debug.LogWarning(Prefix + message);
        }
    }
}