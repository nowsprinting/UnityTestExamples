// Copyright (c) 2021-2022 Koji Hasegawa.
// This software is released under the MIT License.

namespace TestDoubleExample.Janken
{
    /// <summary>
    /// 非決定的なテストしかできないじゃんけんロジック
    /// </summary>
    public class NondeterministicJanken
    {
        /// <summary>
        /// じゃんけんぽん
        /// </summary>
        /// <returns>ランダムな手</returns>
        public Hand Pon()
        {
            return (Hand)UnityEngine.Random.Range(0, 3);
        }
    }
}
