// Copyright (c) 2021 Koji Hasegawa.
// This software is released under the MIT License.

namespace TestDoubleExample.Dices
{
    /// <summary>
    /// 曖昧なテストしかできないダイス
    /// </summary>
    public class UncontrollableDice
    {
        /// <summary>
        /// 六面体ダイスを振る
        /// </summary>
        /// <returns>1〜6のランダムな値</returns>
        public int Roll()
        {
            return UnityEngine.Random.Range(1, 7);
        }
    }
}
