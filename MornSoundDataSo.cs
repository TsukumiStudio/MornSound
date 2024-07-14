using UnityEngine;

namespace MornSound
{
    [CreateAssetMenu(fileName = nameof(MornSoundDataSo), menuName = nameof(MornSoundDataSo))]
    public sealed class MornSoundDataSo : ScriptableObject
    {
        [SerializeField] private AudioClip _audioClip;
        [SerializeField] [Range(0, 1)] private float _volumeRate = 1;
        [SerializeField] private float _semitoneDownRange;
        [SerializeField] private float _semitoneUpRange;

        public AudioClip AudioClip => _audioClip;
        public float VolumeRate => _volumeRate;
        public float PitchRate => 1 * Mathf.Pow(2, Random.Range(_semitoneDownRange, _semitoneUpRange) / 12f);
    }
}