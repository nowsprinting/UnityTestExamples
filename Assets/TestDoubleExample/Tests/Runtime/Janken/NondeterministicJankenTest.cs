// Copyright (c) 2021-2022 Koji Hasegawa.
// This software is released under the MIT License.

using NUnit.Framework;

namespace TestDoubleExample.Janken
{
    /// <summary>
    /// 非決定的なテストしかできないじゃんけんロジックに対するテストの例
    /// </summary>
    /// <remarks>
    /// じゃんけんくらいの試行回数であればテストダブルに頼らないテストという選択肢もあります
    /// </remarks>
    public class NondeterministicJankenTest
    {
        [Test]
        [Repeat(100)]
        public void Pon_非決定的なテストしかできないのでOrとRepeatを使ってテストする例()
        {
            var sut = new NondeterministicJanken();
            var actual = sut.Pon();

            Assert.That(actual, Is.EqualTo(Hand.Rock).Or.EqualTo(Hand.Paper).Or.EqualTo(Hand.Scissors));
        }
    }
}
