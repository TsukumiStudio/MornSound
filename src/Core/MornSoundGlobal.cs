using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

namespace MornLib
{
    [CreateAssetMenu(fileName = nameof(MornSoundGlobal), menuName = "Morn/" + nameof(MornSoundGlobal))]
    public sealed class MornSoundGlobal : MornGlobalBase<MornSoundGlobal>
    {
        [SerializeField, NoLabel] private List<MornSoundInfo> _infos;
        [SerializeField] private AudioMixer _mixer;
        [SerializeField] private float _minDb = -80;
        [Header("Volume")]
        [SerializeField, NoLabel] private string[] _mixerKeys;
        [SerializeField] private MornSoundMixerType _masteType;
        [SerializeField] private MornSoundMixerType _bgmKey;
        [SerializeField] private MornSoundMixerType _seKey;
        [SerializeField, NoLabel] private List<KeyToVolume> _toMixerKeyList;
        [Header("AudioSource")]
        [SerializeField, NoLabel] private List<KeyToMixerGroup> _toMixerGroupList;
        protected override string ModuleName => "MornSound";
        public AudioMixer Mixer => _mixer;
        public string[] MixerKeys => _mixerKeys;
        public MornSoundMixerType MasterKey => _masteType;
        public MornSoundMixerType BgmKey => _bgmKey;
        public MornSoundMixerType SeKey => _seKey;

        public string[] ToMixerKeys(MornSoundMixerType volumeType)
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

        public AudioMixerGroup ToMixerGroup(MornSoundMixerType sourceType)
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

        public float ToDecibel(float rate)
        {
            return rate <= 0 ? -5000 : _minDb - _minDb * Mathf.Log10(rate * 9 + 1);
        }

        public bool TryGetInfo(AudioClip clip, out MornSoundInfo info)
        {
            if (clip == null)
            {
                info = null;
                return false;
            }

            var found = _infos.FirstOrDefault(x => x.AudioClip == clip);
            if (found != null)
            {
                info = found;
                return true;
            }

            info = null;
            return false;
        }
    }
}