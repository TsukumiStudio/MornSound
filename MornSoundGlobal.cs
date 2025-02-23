using System.Collections.Generic;
using System.Linq;
using MornGlobal;
using UnityEngine;

namespace MornSound
{
    [CreateAssetMenu(fileName = nameof(MornSoundGlobal), menuName = "Morn/" + nameof(MornSoundGlobal))]
    internal sealed class MornSoundGlobal : MornGlobalBase<MornSoundGlobal>
    {
        [SerializeField] private List<MornSoundInfo> _infos;
        protected override string ModuleName => nameof(MornSound);

        public bool TryGetInfo(AudioClip clip, out MornSoundInfo info)
        {
            var found = _infos.FirstOrDefault(x => x.AudioClip == clip);
            if (found != null)
            {
                info = found;
                return true;
            }

            info = null;
            return false;
        }

        public static void Log(string message)
        {
            I.LogInternal(message);
        }

        public static void LogWarning(string message)
        {
            I.LogWarningInternal(message);
        }

        public static void LogError(string message)
        {
            I.LogErrorInternal(message);
        }
    }
}