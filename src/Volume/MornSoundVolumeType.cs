using System;

namespace MornLib
{
    [Serializable]
    public class MornSoundVolumeType : MornEnumBase
    {
        public override string[] Values => MornSoundGlobal.I.VolumeKeys;
        public override UnityEngine.Object PingTarget => MornSoundGlobal.I;
    }
}
