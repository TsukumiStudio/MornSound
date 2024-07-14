using UnityEngine;
using UnityEngine.Audio;

namespace MornSound
{
    [CreateAssetMenu(fileName = nameof(MornSoundSimpleClipEntity),
        menuName = "Morn/" + nameof(MornSoundSimpleClipEntity))]
    public sealed class MornSoundSimpleClipEntity : ScriptableObject
    {
        [SerializeField] private AudioClip _audioClip;
        [SerializeField] private AudioMixerGroup _audioMixerGroup;
        [SerializeField] [Range(0, 1)] private float _volumeRate = 1;
        [SerializeField] private float _semitoneDownRange;
        [SerializeField] private float _semitoneUpRange;
        public AudioClip AudioClip => _audioClip;
        public AudioMixerGroup AudioMixerGroup => _audioMixerGroup;
        public float VolumeRate => _volumeRate;
        public float Pitch => 1 * Mathf.Pow(2, Random.Range(_semitoneDownRange, _semitoneUpRange) / 12f);
    }
}