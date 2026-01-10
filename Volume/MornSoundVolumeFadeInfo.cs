using System.Threading;

namespace MornLib
{
    public struct MornSoundVolumeFadeInfo
    {
        public MornSoundVolumeType SoundVolumeType;
        public bool IsFadeIn;
        public float? Duration;
        public CancellationToken CancellationToken;
    }
}