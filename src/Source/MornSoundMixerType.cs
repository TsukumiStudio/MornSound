using System;

namespace MornLib
{
    [Serializable]
    public class MornSoundMixerType : MornEnumBase
    {
        public override string[] Values => MornSoundGlobal.I.MixerKeys;
        public override UnityEngine.Object PingTarget => MornSoundGlobal.I;
    }
}
