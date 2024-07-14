using UnityEngine;

namespace MornSound
{
    public class MornSoundVolumeCore
    {
        private readonly IMornSoundVolumeSaver _volumeSaver;

        public MornSoundVolumeCore(IMornSoundVolumeSaver volumeSaver)
        {
            _volumeSaver = volumeSaver;
            var solver = new GameObject("MornSoundVolumeSolver").AddComponent<MornSoundVolumeSolver>();
            solver.Initialize(LoadAndApplyVolumes);
        }

        private void LoadAndApplyVolumes()
        {
            foreach (var volumeKey in MornSoundVolumeGlobalSettings.Instance.VolumeKeys)
            {
                var volumeRate = _volumeSaver.Load(volumeKey);
                var volumeDecibel = VolumeRateToDecibel(volumeRate);
                MornSoundVolumeGlobalSettings.Instance.Mixer.SetFloat(volumeKey, volumeDecibel);
            }
        }

        public void ApplyVolume(string volumeKey, float volumeRate)
        {
            var volumeDecibel = VolumeRateToDecibel(volumeRate);
            MornSoundVolumeGlobalSettings.Instance.Mixer.SetFloat(volumeKey, volumeDecibel);
        }

        public void SaveAndApplyVolume(string volumeKey, float volumeRate)
        {
            var volumeDecibel = VolumeRateToDecibel(volumeRate);
            MornSoundVolumeGlobalSettings.Instance.Mixer.SetFloat(volumeKey, volumeDecibel);
            _volumeSaver.Save(volumeKey, volumeRate);
        }

        private static float VolumeRateToDecibel(float rate)
        {
            return rate <= 0 ? -5000 : (1 - rate) * -30;
        }
    }
}