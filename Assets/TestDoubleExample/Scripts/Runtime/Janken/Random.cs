// Copyright (c) 2021-2022 Koji Hasegawa.
// This software is released under the MIT License.

namespace TestDoubleExample.Janken
{
    /// <summary>
    /// <see cref="IRandom"/>の実装
    /// <see cref="UnityEngine.Random"/>を使用
    /// </summary>
    public class Random : IRandom
    {
        /// <inheritdoc/>
        public int Range(int minValue, int maxValue)
        {
            return UnityEngine.Random.Range(minValue, maxValue);
        }
    }
}
