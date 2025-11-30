// Copyright (c) 2021-2025 Koji Hasegawa.
// This software is released under the MIT License.

namespace TestDoubleExample.Janken
{
    /// <summary>
    /// 擬似乱数生成器インタフェース
    /// </summary>
    public interface IRandom
    {
        /// <summary>
        /// 指定された範囲のランダムな浮動小数点数を返す
        /// </summary>
        float Range(float minInclusive, float maxInclusive);
    }
}
