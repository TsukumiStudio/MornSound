using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace MornLib
{
    public sealed class MornSoundService : MonoBehaviour
    {
        private static MornSoundService _instance;
        private readonly Dictionary<string, AudioSource> _audioSourceCache = new();
        private readonly Dictionary<string, CancellationTokenSource> _fadeTokenCache = new();
        public static MornSoundService I
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindAnyObjectByType<MornSoundService>();
                    if (_instance == null)
                    {
                        var obj = new GameObject(nameof(MornSoundService));
                        _instance = obj.AddComponent<MornSoundService>();
                    }
                }

                return _instance;
            }
        }

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else if (_instance != this)
            {
                Destroy(gameObject);
            }
        }

        internal AudioSource GetSource(MornSoundSourceType sourceType)
        {
            var key = sourceType.Key;
            if (!_audioSourceCache.TryGetValue(key, out var audioSource) || audioSource == null)
            {
                var child = transform.Find(key);
                if (child == null)
                {
                    child = new GameObject(key).transform;
                    child.SetParent(transform);
                }

                audioSource = child.GetComponent<AudioSource>();
                if (audioSource == null)
                {
                    audioSource = child.gameObject.AddComponent<AudioSource>();
                }

                _audioSourceCache[key] = audioSource;
            }

            audioSource.outputAudioMixerGroup = sourceType.ToMixerGroup();
            return audioSource;
        }

        internal CancellationToken GetFadeToken(MornSoundSourceType sourceType)
        {
            var key = sourceType.Key;

            // 既存のトークンがあればキャンセル
            if (_fadeTokenCache.TryGetValue(key, out var existingCts))
            {
                existingCts.Cancel();
                existingCts.Dispose();
            }

            // 新しいトークンを作成
            var cts = new CancellationTokenSource();
            _fadeTokenCache[key] = cts;
            return cts.Token;
        }

        private void OnDestroy()
        {
            // すべてのCancellationTokenSourceを破棄
            foreach (var cts in _fadeTokenCache.Values)
            {
                cts?.Cancel();
                cts?.Dispose();
            }

            _fadeTokenCache.Clear();
        }
    }
}