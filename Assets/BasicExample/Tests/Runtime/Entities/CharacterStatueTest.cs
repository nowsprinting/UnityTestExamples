// Copyright (c) 2021 Koji Hasegawa.
// This software is released under the MIT License.

using NUnit.Framework;

namespace BasicExample.Entities
{
    [TestFixture]
    public class CharacterStatueTest
    {
        [Test]
        public void IsDestroyed_まだ生きている()
        {
            var sut = new CharacterStatus(maxHp: 1);

            Assert.That(sut.IsDestroyed(), Is.False);
            Assert.That(sut.HitPoint, Is.EqualTo(1));
        }

        [Test]
        public void IsDestroyed_もう死んでいる()
        {
            var sut = new CharacterStatus(maxHp: 1);
            sut.GiveAttack(1, Element.None);

            Assert.That(sut.IsDestroyed(), Is.True);
        }

        [Test]
        public void GiveAttack_防御力5に対して攻撃力1_被ダメージなし()
        {
            var sut = new CharacterStatus(ele: Element.None, maxHp: 2, def: 5);
            var damage = sut.GiveAttack(1, Element.None);

            Assert.That(damage, Is.False);
        }

        [Test]
        public void GiveAttack_防御力5に対して攻撃力1_HP減少なし()
        {
            var sut = new CharacterStatus(ele: Element.None, maxHp: 2, def: 5);
            var damage = sut.GiveAttack(1, Element.None);

            Assert.That(sut.HitPoint, Is.EqualTo(2));
        }

        [Test]
        public void GiveAttack_防御力0に対して攻撃力1_被ダメージあり()
        {
            var sut = new CharacterStatus(ele: Element.None, maxHp: 2, def: 0);
            var damage = sut.GiveAttack(1, Element.None);

            Assert.That(damage, Is.True);
        }

        [Test]
        public void GiveAttack_防御力0に対して攻撃力1_HP減少()
        {
            var sut = new CharacterStatus(ele: Element.None, maxHp: 2, def: 0);
            var damage = sut.GiveAttack(1, Element.None);

            Assert.That(sut.HitPoint, Is.EqualTo(1));
        }
    }
}
