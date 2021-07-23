// Copyright (c) 2021 Koji Hasegawa.
// This software is released under the MIT License.

using NUnit.Framework;

namespace TestDoubleExample.Dices
{
    /// <summary>
    /// テストスパイによるテストの例
    /// </summary>
    /// <remarks>
    /// 実装のテストになりすぎるので、実際はこの程度であればスパイは使いません。これは例として。
    /// </remarks>
    public class DiceTestUsingSpy
    {
        /// <summary>
        /// テストスパイ
        /// </summary>
        private class SpyRandom : IRandom
        {
            /// <summary>
            /// <see cref="Next(int, int)"/>の引数をキャプチャします
            /// </summary>
            public int _actualMinValue;

            /// <summary>
            /// <see cref="Next(int, int)"/>の引数をキャプチャします
            /// </summary>
            public int _actualMaxValue;

            /// <inheritdoc/>
            public int Next(int minValue, int maxValue)
            {
                this._actualMinValue = minValue;
                this._actualMaxValue = maxValue;

                return 1; // テスト結果に影響しないのであれば、固定値を返しても、本物の依存オブジェクトを使用してもよい
            }
        }

        [Test]
        public void Roll_テストスパイを注入することで間接出力をテストする例()
        {
            var stub = new SpyRandom();
            var sut = new Dice(stub);
            sut.Roll();

            Assert.That(stub._actualMinValue, Is.EqualTo(1));
            Assert.That(stub._actualMaxValue, Is.EqualTo(7));
        }
    }
}
