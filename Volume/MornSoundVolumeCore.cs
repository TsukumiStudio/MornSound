using Cysharp.Threading.Tasks;
using UnityEngine;

namespace MornSound
{
    public sealed class MornSoundVolumeCore
    {
        private readonly MornSoundVolumeSolver _solver;

        public MornSoundVolumeCore(IMornSoundVolumeSaver soundVolumeSaver)
        {
            _solver = new GameObject(nameof(MornSoundVolumeSolver)).AddComponent<MornSoundVolumeSolver>();
            _solver.Initialize(soundVolumeSaver);
            Object.DontDestroyOnLoad(_solver.gameObject);
        }

        public async UniTask FadeAsync(MornSoundVolumeFadeInfo fadeInfo)
        {
            await _solver.FadeAsync(fadeInfo);
        }
    }
}