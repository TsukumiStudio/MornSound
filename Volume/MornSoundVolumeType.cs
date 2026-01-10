using System;
using MornEnum;

namespace MornLib
{
    [Serializable]
    public class MornSoundVolumeType : MornEnumBase
    {
        protected override string[] Values => MornSoundGlobal.I.VolumeKeys;
    }
}