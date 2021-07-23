// Copyright (c) 2021 Koji Hasegawa.
// This software is released under the MIT License.

using NUnit.Framework;

namespace TestDoubleExample.Dices
{
    /// <summary>
    /// テストスタブによるテストの例
    /// </summary>
    public class DiceTestUsingStub
    {
        /// <summary>
        /// テストスタブ
        /// </summary>
        private class StubRandom : IRandom
        {
            private readonly int _response;

            public StubRandom(int response)
            {
                _response = response;
            }

            /// <summary>
            /// 固定値を返すスタブ実装
            /// </summary>
            /// <param name="minValue"></param>
            /// <param name="maxValue"></param>
            /// <returns></returns>
            public int Next(int minValue, int maxValue)
            {
                return _response;
            }
        }

        [Test]
        public void Roll_テストスタブを注入して間接入力を与えることで決定的なテストが可能()
        {
            var stub = new StubRandom(1);
            var sut = new Dice(stub);
            var actual = sut.Roll();

            Assert.That(actual, Is.EqualTo(1));
        }
    }
}
