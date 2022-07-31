// Copyright (c) 2021-2022 Koji Hasegawa.
// This software is released under the MIT License.

using NUnit.Framework;

namespace TestDoubleExample.Janken
{
    /// <summary>
    /// テストダブルによるテストの例
    /// </summary>
    public class JankenTest
    {
        [Test]
        public void Pon_テストスタブを注入して間接入力を与えることによる決定的なテスト()
        {
            var stub = new StubRandom(2); // 常に2を返すテストスタブを生成
            var sut = new Janken(stub); // テスト対象にスタブを注入
            var actual = sut.Pon();

            Assert.That(actual, Is.EqualTo(Hand.Paper)); // 結果は常に「ぱー」
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
            public int Range(int minValue, int maxValue)
            {
                return _response; // 常にコンストラクタで指定された固定値を返す
            }
        }

        [Test]
        public void Pon_テストスパイを注入することによる間接出力のテスト()
        {
            var spy = new SpyRandom(); // テストスパイを生成
            var sut = new Janken(spy); // テスト対象にスパイを注入
            sut.Pon();

            Assert.That(spy._actualMinValue, Is.EqualTo(0));
            Assert.That(spy._actualMaxValue, Is.EqualTo(3));
        }

        /// <summary>
        /// テストスパイ実装
        /// </summary>
        private class SpyRandom : IRandom
        {
            /// <summary>
            /// <see cref="Range"/>の第一引数をキャプチャ
            /// </summary>
            internal int _actualMinValue;

            /// <summary>
            /// <see cref="Range"/>の第二引数をキャプチャ
            /// </summary>
            internal int _actualMaxValue;

            /// <summary>
            /// 引数をキャプチャするスパイメソッド
            /// </summary>
            /// <returns>常に1を返す</returns>
            public int Range(int minValue, int maxValue)
            {
                this._actualMinValue = minValue; // 第一引数をキャプチャ
                this._actualMaxValue = maxValue; // 第二引数をキャプチャ

                return 1; // テスト結果に影響しないのであれば、スタブのように固定値を返しても、本物の依存オブジェクトに委譲してもよい
            }
        }
    }
}
