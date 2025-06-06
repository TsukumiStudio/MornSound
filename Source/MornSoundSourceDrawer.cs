#if UNITY_EDITOR
using MornEnum;
using UnityEditor;

namespace MornSound
{
    [CustomPropertyDrawer(typeof(MornSoundSourceType))]
    public class MornSoundSourceDrawer : MornEnumDrawerBase
    {
        protected override string[] Values => MornSoundGlobal.I.SourceKeys;
    }
}
#endif