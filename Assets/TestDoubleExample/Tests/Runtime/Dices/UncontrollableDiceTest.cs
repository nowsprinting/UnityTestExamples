// Copyright (c) 2021 Koji Hasegawa.
// This software is released under the MIT License.

using NUnit.Framework;

namespace TestDoubleExample.Dices
{
    /// <summary>
    /// 曖昧なテストしかできないダイスに対するテストの例
    /// </summary>
    /// <remarks>
    /// 六面体ダイスくらいの試行回数であればテストダブルに頼らないテストでも十分ですが、これは例として。
    /// </remarks>
    public class UncontrollableDiceTest
    {
        [Test]
        [Repeat(100)]
        public void Roll_曖昧なテストしかできないのでInRangeとRepeatを使ってテストする例()
        {
            var sut = new UncontrollableDice();
            var actual = sut.Roll();

            Assert.That(actual, Is.InRange(1, 6));
        }
    }
}
