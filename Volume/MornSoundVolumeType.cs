using System;
using MornEnum;

namespace MornSound
{
    [Serializable]
    public class MornSoundVolumeType : MornEnumBase
    {
        protected override string[] Values => MornSoundGlobal.I.VolumeKeys;
    }
}