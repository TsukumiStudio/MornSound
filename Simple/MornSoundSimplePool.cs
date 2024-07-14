using System.Collections.Generic;
using UnityEngine;

namespace MornSound
{
    internal class MornSoundSimplePool
    {
        private readonly Queue<MornSoundSimpleSe> _cachedPlayer = new();
        private readonly Transform _parent;

        internal MornSoundSimplePool(Transform parent)
        {
            _parent = parent;
        }

        internal MornSoundSimpleSe Rent()
        {
            if (_cachedPlayer.TryDequeue(out var player))
            {
                player.gameObject.SetActive(true);
                return player;
            }

            player = new GameObject().AddComponent<MornSoundSimpleSe>();
            player.name = nameof(MornSoundSimpleSe);
            player.transform.SetParent(_parent);
            return player;
        }

        internal void Return(MornSoundSimpleSe simpleSe)
        {
            simpleSe.gameObject.SetActive(false);
            _cachedPlayer.Enqueue(simpleSe);
        }
    }
}