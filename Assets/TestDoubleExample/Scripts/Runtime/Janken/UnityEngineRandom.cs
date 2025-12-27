// Copyright (c) 2021-2025 Koji Hasegawa.
// This software is released under the MIT License.

using UnityEngine;

namespace TestDoubleExample.Janken
{
    /// <summary>
    /// <see cref="UnityEngine.Random"/> を使用する <see cref="IRandom"/> 実装
    /// </summary>
    public class UnityEngineRandom : IRandom
    {
        /// <inheritdoc/>
        public float Range(float minInclusive, float maxInclusive)
        {
            return Random.Range(minInclusive, maxInclusive);
        }
    }
}
