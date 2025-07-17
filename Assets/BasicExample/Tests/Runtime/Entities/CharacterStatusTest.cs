// Copyright (c) 2021-2025 Koji Hasegawa.
// This software is released under the MIT License.

using BasicExample.Entities.Enums;
using NUnit.Framework;

namespace BasicExample.Entities
{
    [TestFixture]
    public class CharacterStatusTest
    {
        [Test]
        public void IsDestroyed_まだ生きている()
        {
            var sut = new CharacterStatus(hp: 1);

            Assert.That(sut.IsDestroyed(), Is.False);
            Assert.That(sut.HitPoint, Is.EqualTo(1));
        }

        [Test]
        public void IsDestroyed_もう死んでいる()
        {
            var sut = new CharacterStatus(hp: 1);
            sut.TakeDamage(Element.None, 1);

            Assert.That(sut.IsDestroyed(), Is.True);
        }

        [Test]
        public void TakeDamage_防御力5に対して攻撃力1_被ダメージなし()
        {
            var sut = new CharacterStatus(element: Element.None, hp: 2, defense: 5);
            var damaged = sut.TakeDamage(Element.None, 1);

            Assert.That(damaged, Is.False);
        }

        [Test]
        public void TakeDamage_防御力5に対して攻撃力1_HP減少なし()
        {
            var sut = new CharacterStatus(element: Element.None, hp: 2, defense: 5);
            sut.TakeDamage(Element.None, 1);

            Assert.That(sut.HitPoint, Is.EqualTo(2));
        }

        [Test]
        public void TakeDamage_防御力0に対して攻撃力1_被ダメージあり()
        {
            var sut = new CharacterStatus(element: Element.None, hp: 2, defense: 0);
            var damaged = sut.TakeDamage(Element.None, 1);

            Assert.That(damaged, Is.True);
        }

        [Test]
        public void TakeDamage_防御力2に対して攻撃力3_HPが1減少()
        {
            var sut = new CharacterStatus(element: Element.None, hp: 5, defense: 2);
            sut.TakeDamage(element: Element.None, attackPower: 3);

            var actual = sut.HitPoint - 5; // delta HP
            Assert.That(actual, Is.EqualTo(-1));
        }

        [TestCase(2, 3, -1)]
        [TestCase(5, 7, -2)]
        public void TakeDamage_防御力より攻撃力が大きい_HPが差分だけ減少(int defence, int attackPower, int expected)
        {
            var beforeHp = 5;
            var sut = new CharacterStatus(element: Element.None, hp: beforeHp, defense: defence);
            sut.TakeDamage(element: Element.None, attackPower: attackPower);

            var actual = sut.HitPoint - beforeHp; // delta HP
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
