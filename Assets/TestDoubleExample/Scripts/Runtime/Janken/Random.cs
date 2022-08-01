// Copyright (c) 2021-2022 Koji Hasegawa.
// This software is released under the MIT License.

namespace TestDoubleExample.Janken
{
    /// <inheritdoc/>
    public class Random : IRandom
    {
        /// <inheritdoc/>
        public int Range(int minValue, int maxValue)
        {
            return UnityEngine.Random.Range(minValue, maxValue);
        }
    }
}
