// Copyright (c) 2021 Koji Hasegawa.
// This software is released under the MIT License.

using NUnit.Framework;

namespace APIExamples.NUnit
{
    /// <summary>
    /// 複数アサーションの記述例
    /// </summary>
    [TestFixture]
    public class MultipleAssertsExample
    {
        [Test]
        public void Constructor_プロパティは仕様通り設定される()
        {
            var sut = new Character();

            Assert.That(sut.MaxHitPoint, Is.EqualTo(100), "MaxHitPointは正しい");
            Assert.That(sut.Attack, Is.EqualTo(20), "Attackは正しい");
            Assert.That(sut.Defence, Is.EqualTo(20), "Defenceは正しい");
        }

        private class Character
        {
            public int MaxHitPoint { get; private set; }
            public int Attack { get; private set; }
            public int Defence { get; private set; }

            public Character()
            {
                MaxHitPoint = 100;
                Attack = 20;
                Defence = 20;
            }
        }
    }
}
