using System;

namespace MornLib
{
    [Serializable]
    public class MornSoundSourceType : MornEnumBase
    {
        public override string[] Values => MornSoundGlobal.I.SourceKeys;
        public override UnityEngine.Object PingTarget => MornSoundGlobal.I;
    }
}
