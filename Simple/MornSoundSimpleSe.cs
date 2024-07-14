using System;
using UnityEngine;

namespace MornSound
{
    [AddComponentMenu("")]
    internal sealed class MornSoundSimpleSe : MonoBehaviour
    {
        private AudioSource _audioSource;
        private Action<MornSoundSimpleSe> _playEnd;

        private void Awake()
        {
            _audioSource = TryGetComponent<AudioSource>(out var source)
                ? source
                : gameObject.AddComponent<AudioSource>();
        }

        private void Update()
        {
            if (_audioSource.isPlaying) return;
            _playEnd(this);
        }

        internal void Play(MornSoundSimpleConfig config, Action<MornSoundSimpleSe> playEnd)
        {
            _audioSource.outputAudioMixerGroup = config.MixerGroup;
            _audioSource.clip = config.Clip;
            _audioSource.pitch = config.Pitch;
            _audioSource.volume = config.Volume;
            _audioSource.Play();
            _playEnd = playEnd;
        }
    }
}