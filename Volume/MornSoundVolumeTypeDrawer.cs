#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace MornLib
{
    [CustomPropertyDrawer(typeof(MornSoundVolumeType))]
    public class MornSoundVolumeTypeDrawer : MornEnumDrawerBase
    {
        protected override string[] Values => MornSoundGlobal.I.VolumeKeys;
        protected override Object PingTarget => MornSoundGlobal.I;
    }
}
#endif