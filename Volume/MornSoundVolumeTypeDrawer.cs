#if UNITY_EDITOR
using MornEnum;
using UnityEditor;
using UnityEngine;

namespace MornSound
{
    [CustomPropertyDrawer(typeof(MornSoundVolumeType))]
    public class MornSoundVolumeTypeDrawer : MornEnumDrawerBase
    {
        protected override string[] Values => MornSoundGlobal.I.VolumeKeys;
        protected override Object PingTarget => MornSoundGlobal.I;
    }
}
#endif