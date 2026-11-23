using System.Collections.Generic;

namespace MornLib
{
    /// <summary>同じ音が短い間に重ねて鳴らされたとき、2回目以降を間引く判定。Unity に依存しない</summary>
    public sealed class MornSoundDuplicateGuard
    {
        private readonly Dictionary<object, double> _lastPlayedAt = new();

        /// <summary>鳴らしてよければ true を返し、鳴らした時刻を記録する。guardSeconds が 0 以下なら常に true</summary>
        public bool TryPass(object key, double now, double guardSeconds)
        {
            if (guardSeconds > 0 && _lastPlayedAt.TryGetValue(key, out var lastPlayedAt) && now - lastPlayedAt < guardSeconds)
            {
                return false;
            }

            _lastPlayedAt[key] = now;
            return true;
        }
    }
}
