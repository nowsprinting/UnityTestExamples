// Copyright (c) 2021 Koji Hasegawa.
// This software is released under the MIT License.

using NUnit.Framework;

namespace TestDoubleExample.Dices
{
    /// <summary>
    /// テストダブルによるテストの例
    /// </summary>
    public class DiceTest
    {
        [Test]
        public void Roll_テストスタブを注入して間接入力を与えることによる決定的なテスト()
        {
            var stub = new StubRandom(1); // 必ず1を返すスタブを生成
            var sut = new Dice(stub); // テスト対象にスタブをセット
            var actual = sut.Roll();

            Assert.That(actual, Is.EqualTo(1));
        }

        /// <summary>
        /// テストスタブ実装
        /// </summary>
        private class StubRandom : IRandom
        {
            private readonly int _response;

            public StubRandom(int response)
            {
                _response = response;
            }

            /// <summary>
            /// 固定値を返すスタブメソッド
            /// </summary>
            /// <returns>常にコンストラクタで指定された固定値を返す</returns>
            public int Next(int minValue, int maxValue)
            {
                return _response;
            }
        }

        [Test]
        public void Roll_テストスパイを注入することによる間接出力のテスト()
        {
            var spy = new SpyRandom();
            var sut = new Dice(spy);
            sut.Roll();

            Assert.That(spy._actualMinValue, Is.EqualTo(1));
            Assert.That(spy._actualMaxValue, Is.EqualTo(7));
        }

        /// <summary>
        /// テストスパイ実装
        /// </summary>
        private class SpyRandom : IRandom
        {
            /// <summary>
            /// <see cref="Next(int, int)"/>の第一引数をキャプチャ
            /// </summary>
            internal int _actualMinValue;

            /// <summary>
            /// <see cref="Next(int, int)"/>の第二引数をキャプチャ
            /// </summary>
            internal int _actualMaxValue;

            /// <summary>
            /// 引数をキャプチャするスパイメソッド
            /// </summary>
            /// <returns>常に1を返す</returns>
            public int Next(int minValue, int maxValue)
            {
                this._actualMinValue = minValue;
                this._actualMaxValue = maxValue;

                return 1; // テスト結果に影響しないのであれば、固定値を返しても、本物の依存オブジェクトを使用してもよい
            }
        }
    }
}
