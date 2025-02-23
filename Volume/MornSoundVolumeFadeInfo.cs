using System.Threading;

namespace MornSound
{
    public struct MornSoundVolumeFadeInfo
    {
        public MornSoundVolumeType SoundVolumeType;
        public bool IsFadeIn;
        public float? Duration;
        public CancellationToken CancellationToken;
    }
}