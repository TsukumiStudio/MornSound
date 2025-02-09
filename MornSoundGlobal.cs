using System.Collections.Generic;
using System.Linq;
using MornGlobal;
using UnityEngine;

namespace MornSound
{
    [CreateAssetMenu(fileName = nameof(MornSoundGlobal), menuName = "Morn/" + nameof(MornSoundGlobal))]
    public sealed class MornSoundGlobal : MornGlobalBase<MornSoundGlobal>
    {
#if DISABLE_MORN_SOUND_LOG
        protected override bool ShowLog => false;
#else
        protected override bool ShowLog => true;
#endif
        protected override string ModuleName => nameof(MornSound);
        [SerializeField] private List<MornSoundInfo> _infos;

        internal bool TryGetInfo(AudioClip clip, out MornSoundInfo info)
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
    }
}