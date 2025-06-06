using System.Collections.Generic;
using System.Linq;
using MornGlobal;
using UnityEngine;
using UnityEngine.Audio;

namespace MornSound
{
    [CreateAssetMenu(fileName = nameof(MornSoundGlobal), menuName = "Morn/" + nameof(MornSoundGlobal))]
    internal sealed class MornSoundGlobal : MornGlobalBase<MornSoundGlobal>
    {
        [SerializeField] private List<MornSoundInfo> _infos;
        [SerializeField] private AudioMixer _mixer;
        [SerializeField] private float _minDb = -80;
        [Header("Volume")]
        [SerializeField] private string[] _volumeKeys;
        [SerializeField] private List<KeyToVolume> _toMixerKeyList;
        [Header("AudioSource")]
        [SerializeField] private string[] _sourceKeys;
        [SerializeField] private List<KeyToMixerGroup> _toMixerGroupList;
        protected override string ModuleName => nameof(MornSound);
        public AudioMixer Mixer => _mixer;
        public string[] VolumeKeys => _volumeKeys;
        public string[] SourceKeys => _sourceKeys;

        public string[] ToMixerKeys(MornSoundVolumeType volumeType)
        {
            foreach (var toMixerKey in _toMixerKeyList)
            {
                if (toMixerKey.VolumeType.Key == volumeType.Key)
                {
                    return toMixerKey.MixerKeys;
                }
            }

            return null;
        }

        public AudioMixerGroup ToMixerGroup(MornSoundSourceType sourceType)
        {
            foreach (var toMixerGroup in _toMixerGroupList)
            {
                if (toMixerGroup.SourceType.Key == sourceType.Key)
                {
                    return toMixerGroup.MixerGroup;
                }
            }

            return null;
        }

        public float VolumeRateToDecibel(float rate)
        {
            return rate <= 0 ? -5000 : (1 - rate) * _minDb;
        }

        public bool TryGetInfo(AudioClip clip, out MornSoundInfo info)
        {
            var found = _infos.FirstOrDefault(x => x.AudioClip == clip);
            if (found != null)
            {
                info = found;
                return true;
            }

            info = null;
            return false;
        }

        internal static void Log(string message)
        {
            I.LogInternal(message);
        }

        internal static void LogWarning(string message)
        {
            I.LogWarningInternal(message);
        }

        internal static void LogError(string message)
        {
            I.LogErrorInternal(message);
        }
    }
}