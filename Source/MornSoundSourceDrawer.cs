#if UNITY_EDITOR
using MornEnum;
using UnityEditor;
using UnityEngine;

namespace MornSound
{
    [CustomPropertyDrawer(typeof(MornSoundSourceType))]
    public class MornSoundSourceDrawer : MornEnumDrawerBase
    {
        protected override string[] Values => MornSoundGlobal.I.SourceKeys;
        protected override Object PingTarget => MornSoundGlobal.I;
    }
}
#endif