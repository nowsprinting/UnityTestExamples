// Copyright (c) 2021 Koji Hasegawa.
// This software is released under the MIT License.

using NUnit.Framework;

namespace TestDoubleExample.Dices
{
    /// <summary>
    /// モックオブジェクトによるテストの例
    /// </summary>
    /// <remarks>
    /// 実装のテストになりすぎるので、実際はこの程度であればモックは使いません。これは例として。
    /// </remarks>
    public class DiceTestUsingMock
    {
        /// <summary>
        /// モックオブジェクト
        /// </summary>
        private class MockRandom : IRandom
        {
            private readonly int _expectedMinValue;
            private readonly int _expectedMaxValue;
            private readonly int _expectedNumberCall;
            private int _actualNumberCall;

            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="expectedMinValue">期待する最小値</param>
            /// <param name="expectedMaxValue">期待する最大値</param>
            /// <param name="expectedNumberCall">期待する呼び出し回数</param>
            public MockRandom(int expectedMinValue, int expectedMaxValue, int expectedNumberCall)
            {
                this._expectedMinValue = expectedMinValue;
                this._expectedMaxValue = expectedMaxValue;
                this._expectedNumberCall = expectedNumberCall;
            }

            /// <inheritdoc/>
            public int Next(int minValue, int maxValue)
            {
                Assert.That(minValue, Is.EqualTo(this._expectedMinValue));
                Assert.That(maxValue, Is.EqualTo(this._expectedMaxValue));
                this._actualNumberCall++;

                return 1; // テストが継続可能な戻り値
            }

            /// <summary>
            /// IRandom.Next(int,int) を呼び出された回数を検証
            /// </summary>
            public void Verify()
            {
                Assert.That(this._actualNumberCall, Is.EqualTo(this._expectedNumberCall));
            }
        }

        [Test]
        public void Roll_モックオブジェクトを注入することで間接出力をテストする例()
        {
            var mock = new MockRandom(1, 7, 1); // 期待値をセット
            var sut = new Dice(mock);
            sut.Roll();

            mock.Verify();
        }
    }
}
