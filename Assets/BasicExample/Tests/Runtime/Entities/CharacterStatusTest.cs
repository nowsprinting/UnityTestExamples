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
            Assume.That(sut.HitPoint, Is.EqualTo(1));

            Assert.That(sut.IsDestroyed(), Is.False);
        }

        [Test]
        public void IsDestroyed_もう死んでいる()
        {
            var sut = new CharacterStatus(hp: 0);
            Assume.That(sut.HitPoint, Is.EqualTo(0));

            Assert.That(sut.IsDestroyed(), Is.True);
        }

        [Test]
        public void TakeDamage_防御力3に対して攻撃力1_ダメージなし()
        {
            // Setup
            var sut = new CharacterStatus(Element.None, defense: 3);

            // Exercise
            var damage = sut.TakeDamage(Element.None, attack: 1);

            // Verify
            Assert.That(damage, Is.False);
        }

        [Test]
        public void TakeDamage_防御力3に対して攻撃力1_HP減少なし()
        {
            // Setup
            var beforeHp = 100;
            var sut = new CharacterStatus(Element.None, defense: 3, hp: beforeHp);

            // Exercise
            sut.TakeDamage(Element.None, attack: 1);

            // Verify
            var deltaHp = sut.HitPoint - beforeHp;
            Assert.That(deltaHp, Is.EqualTo(0));
        }

        [Test]
        public void TakeDamage_防御力0に対して攻撃力1_ダメージあり()
        {
            var sut = new CharacterStatus(Element.None, defense: 0);

            var damage = sut.TakeDamage(Element.None, attack: 1);

            Assert.That(damage, Is.True);
        }

        [Test]
        public void TakeDamage_防御力2に対して攻撃力3_HPが1減少()
        {
            var beforeHp = 100;
            var sut = new CharacterStatus(Element.None, defense: 2, hp: beforeHp);

            sut.TakeDamage(Element.None, attack: 3);

            var deltaHp = sut.HitPoint - beforeHp;
            Assert.That(deltaHp, Is.EqualTo(-1));
        }

        [TestCase(2, 3, -1)]
        [TestCase(5, 7, -2)]
        public void TakeDamage_防御力より攻撃力が大きい_HPが差分だけ減少(int defence, int attackPower, int expected)
        {
            var beforeHp = 100;
            var sut = new CharacterStatus(Element.None, defense: defence, hp: beforeHp);

            sut.TakeDamage(Element.None, attack: attackPower);

            var deltaHp = sut.HitPoint - beforeHp;
            Assert.That(deltaHp, Is.EqualTo(expected));
        }
    }
}
