// Copyright (c) 2021-2025 Koji Hasegawa.
// This software is released under the MIT License.

using TestDoubleExample.Janken;

namespace TestDoubleExample.TestDoubles
{
    /// <summary>
    /// <see cref="IRandom"/> のテストスタブ実装
    /// </summary>
    public class StubRandom : IRandom
    {
        private readonly float _value;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="value"><see cref="Range"/>メソッドが返す値を指定</param>
        public StubRandom(float value)
        {
            _value = value;
        }

        /// <inheritdoc/>
        /// <remarks>
        /// 常にコンストラクタで指定された値を返す
        /// </remarks>
        public float Range(float minInclusive, float maxInclusive)
        {
            return _value;
        }
    }
}
