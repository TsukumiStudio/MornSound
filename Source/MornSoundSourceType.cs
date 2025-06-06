using System;
using MornEnum;

namespace MornSound
{
    [Serializable]
    public class MornSoundSourceType : MornEnumBase
    {
        protected override string[] Values => MornSoundGlobal.I.SourceKeys;
    }
}