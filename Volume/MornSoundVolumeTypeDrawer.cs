#if UNITY_EDITOR
using MornEnum;
using UnityEditor;

namespace MornSound
{
    [CustomPropertyDrawer(typeof(MornSoundVolumeType))]
    public class MornSoundVolumeTypeDrawer : MornEnumDrawerBase
    {
        protected override string[] Values => MornSoundGlobal.I.VolumeKeys;
    }
}
#endif