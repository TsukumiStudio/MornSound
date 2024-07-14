using System;
using UnityEngine;

namespace MornSound
{
    internal class MornSoundVolumeSolver : MonoBehaviour
    {
        private Action _onStart;

        private void Start()
        {
            _onStart?.Invoke();
            Destroy(gameObject);
        }

        public void Initialize(Action onStart)
        {
            _onStart = onStart;
        }
    }
}