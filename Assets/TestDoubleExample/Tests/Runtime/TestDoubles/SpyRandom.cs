// Copyright (c) 2021-2025 Koji Hasegawa.
// This software is released under the MIT License.

using TestDoubleExample.Janken;

namespace TestDoubleExample.TestDoubles
{
    /// <summary>
    /// <see cref="IRandom"/> のテストスパイ実装
    /// </summary>
    public class SpyRandom : IRandom
    {
        public float LastMinInclusive { get; private set; }
        public float LastMaxInclusive { get; private set; }

        /// <inheritdoc/>
        /// <remarks>
        /// 引数で指定された値をプロパティに保存し、<see cref="UnityEngine.Random"/> による擬似乱数を返す
        /// </remarks>
        public float Range(float minInclusive, float maxInclusive)
        {
            LastMinInclusive = minInclusive;
            LastMaxInclusive = maxInclusive;

            return 0.0f;
            // Note: テスト結果に影響しないのであれば、スタブのように固定値を返しても、本物の依存オブジェクトに委譲してもよい
        }
    }
}
