// Copyright (c) 2021-2022 Koji Hasegawa.
// This software is released under the MIT License.

namespace TestDoubleExample.Janken
{
    /// <summary>
    /// <see cref="IRandom"/>の実装
    /// <see cref="Range"/>はvirtualメソッド
    /// </summary>
    public class VRandom : IRandom
    {
        /// <inheritdoc/>
        public virtual int Range(int minValue, int maxValue)
        {
            return UnityEngine.Random.Range(minValue, maxValue);
        }
    }
}
