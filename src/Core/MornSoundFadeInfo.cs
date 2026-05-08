using System.Threading;

namespace MornLib
{
    public struct MornSoundFadeInfo
    {
        public MornSoundMixerType SoundVolumeType;
        public bool IsFadeIn;
        public float? Duration;
        public MornEaseType? EaseType;
        public CancellationToken CancellationToken;
    }
}