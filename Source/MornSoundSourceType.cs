using System;

namespace MornLib
{
    [Serializable]
    public class MornSoundSourceType : MornEnumBase
    {
        protected override string[] Values => MornSoundGlobal.I.SourceKeys;
    }
}