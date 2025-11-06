// Copyright (c) 2021-2025 Koji Hasegawa.
// This software is released under the MIT License.

using System.Collections;
using BasicExample.Entities.Enums;
using NUnit.Framework;
using TestHelper.Attributes;
using UnityEngine;
using UnityEngine.TestTools;

namespace BasicExample.Entities
{
    [TestFixture]
    public class PassiveEffectTest
    {
        private static PassiveEffect CreatePassiveEffect(bool buff = false, bool debuff = false)
        {
            var passiveEffect = new GameObject().AddComponent<PassiveEffect>();

            if (buff)
            {
                passiveEffect.Buff();
            }

            if (debuff)
            {
                passiveEffect.DeBuff();
            }

            return passiveEffect;
        }

        [TestFixture]
        public class 状態遷移テスト
        {
            [TestFixture]
            public class 通常状態からの遷移
            {
                [Test]
                public void デバフ_弱体状態になること()
                {
                    var sut = CreatePassiveEffect();
                    sut.DeBuff();
                    Assert.That(sut.State(), Is.EqualTo(PassiveEffectState.Weakening));
                }

                [Test]
                public void バフ_強化状態になること()
                {
                    var sut = CreatePassiveEffect();
                    sut.Buff();
                    Assert.That(sut.State(), Is.EqualTo(PassiveEffectState.Strengthening));
                }
            }

            [TestFixture]
            public class 弱体状態からの遷移
            {
                [Test]
                public void バフ_通常状態になること()
                {
                    var sut = CreatePassiveEffect(debuff: true);
                    sut.Buff();
                    Assert.That(sut.State(), Is.EqualTo(PassiveEffectState.Normal));
                }

                [Test]
                public void デバフ_弱体状態が維持されること()
                {
                    var sut = CreatePassiveEffect(debuff: true);
                    sut.DeBuff();
                    Assert.That(sut.State(), Is.EqualTo(PassiveEffectState.Weakening));
                }

                [UnityTest]
                [TimeScale(10.0f)]
                public IEnumerator デバフ_持続時間が30秒にリセットされること()
                {
                    var sut = CreatePassiveEffect(debuff: true);
                    yield return new WaitForSeconds(5.0f);
                    Assume.That(sut.State(), Is.EqualTo(PassiveEffectState.Weakening));
                    Assume.That(sut.PassiveEffectExpireTime(), Is.EqualTo(25.0f).Within(0.1f));

                    sut.DeBuff();

                    Assert.That(sut.State(), Is.EqualTo(PassiveEffectState.Weakening));
                    Assert.That(sut.PassiveEffectExpireTime(), Is.EqualTo(30.0f).Within(0.1f));
                }

                [UnityTest]
                [TimeScale(10.0f)]
                public IEnumerator 時間切れ_通常状態になること()
                {
                    var sut = CreatePassiveEffect(debuff: true);
                    yield return new WaitForSeconds(30.2f);
                    Assert.That(sut.State(), Is.EqualTo(PassiveEffectState.Normal));
                }
            }

            [TestFixture]
            public class 強化状態からの遷移
            {
                [Test]
                public void デバフ_通常状態になること()
                {
                    var sut = CreatePassiveEffect(buff: true);
                    sut.DeBuff();
                    Assert.That(sut.State(), Is.EqualTo(PassiveEffectState.Normal));
                }

                [Test]
                public void バフ_強化状態が維持されること()
                {
                    var sut = CreatePassiveEffect(buff: true);
                    sut.Buff();
                    Assert.That(sut.State(), Is.EqualTo(PassiveEffectState.Strengthening));
                }

                [UnityTest]
                [TimeScale(10.0f)]
                public IEnumerator バフ_持続時間が30秒にリセットされること()
                {
                    var sut = CreatePassiveEffect(buff: true);
                    yield return new WaitForSeconds(5.0f);
                    Assume.That(sut.State(), Is.EqualTo(PassiveEffectState.Strengthening));
                    Assume.That(sut.PassiveEffectExpireTime(), Is.EqualTo(25.0f).Within(0.2f));

                    sut.Buff();

                    Assert.That(sut.State(), Is.EqualTo(PassiveEffectState.Strengthening));
                    Assert.That(sut.PassiveEffectExpireTime(), Is.EqualTo(30.0f).Within(0.1f));
                }

                [UnityTest]
                [TimeScale(10.0f)]
                public IEnumerator 時間切れ_通常状態になること()
                {
                    var sut = CreatePassiveEffect(buff: true);
                    yield return new WaitForSeconds(30.2f);
                    Assert.That(sut.State(), Is.EqualTo(PassiveEffectState.Normal));
                }
            }
        }
    }
}
