// Copyright (c) 2021-2022 Koji Hasegawa.
// This software is released under the MIT License.

namespace TestDoubleExample.Janken
{
    /// <summary>
    /// 擬似乱数発生器インタフェース
    /// </summary>
    public interface IRandom
    {
        /// <summary>
        /// 指定範囲内のランダムな整数を返す
        /// </summary>
        /// <param name="minValue">下限値</param>
        /// <param name="maxValue">上限値（結果に含まない）</param>
        /// <returns></returns>
        int Next(int minValue, int maxValue);
    }
}
