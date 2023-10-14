// Copyright (c) 2021-2022 Koji Hasegawa.
// This software is released under the MIT License.

using System.Collections;
using BasicExample.Entities.Enums;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.TestTools.Utils;

namespace BasicExample.Entities
{
    [TestFixture]
    public class PassiveEffectTest
    {
        [TestFixture]
        public class 状態遷移テスト
        {
            private static readonly FloatEqualityComparer s_expireTimeComparer = new FloatEqualityComparer(0.1f);

            [TestFixture]
            public class 通常状態からの遷移
            {
                private PassiveEffect _sut;

                [SetUp]
                public void SetUp()
                {
                    _sut = new GameObject().AddComponent<PassiveEffect>(); // 通常状態
                }

                [Test]
                public void デバフ_弱体状態になること()
                {
                    _sut.DeBuff();
                    Assert.That(_sut.State(), Is.EqualTo(PassiveEffectState.Weakening));
                }

                [Test]
                public void バフ_強化状態になること()
                {
                    _sut.Buff();
                    Assert.That(_sut.State(), Is.EqualTo(PassiveEffectState.Strengthening));
                }
            }

            [TestFixture]
            public class 弱体状態からの遷移
            {
                private PassiveEffect _sut;

                [SetUp]
                public void SetUp()
                {
                    _sut = new GameObject().AddComponent<PassiveEffect>();
                    _sut.DeBuff(); // 弱体状態
                }

                [TearDown]
                public void TearDown()
                {
                    Time.timeScale = 1.0f;
                }

                [Test]
                public void デバフ_弱体状態が維持されること()
                {
                    _sut.DeBuff();
                    Assert.That(_sut.State(), Is.EqualTo(PassiveEffectState.Weakening));
                }

                [UnityTest]
                public IEnumerator デバフ_持続時間が30秒にリセットされること()
                {
                    Time.timeScale = 100.0f;
                    yield return new WaitForSeconds(10f);

                    _sut.DeBuff();
                    Assert.That(_sut.State(), Is.EqualTo(PassiveEffectState.Weakening));
                    Assert.That(_sut.PassiveEffectExpireTime(), Is.EqualTo(30f).Using(s_expireTimeComparer));
                }

                [Test]
                public void バフ_通常状態になること()
                {
                    _sut.Buff();
                    Assert.That(_sut.State(), Is.EqualTo(PassiveEffectState.Normal));
                }

                [UnityTest]
                public IEnumerator 時間切れ_通常状態になること()
                {
                    Time.timeScale = 100.0f;
                    yield return new WaitForSeconds(30f);
                    Assert.That(_sut.State(), Is.EqualTo(PassiveEffectState.Normal));
                }
            }

            [TestFixture]
            public class 強化状態からの遷移
            {
                private PassiveEffect _sut;

                [SetUp]
                public void SetUp()
                {
                    _sut = new GameObject().AddComponent<PassiveEffect>();
                    _sut.Buff(); // 強化状態
                }

                [TearDown]
                public void TearDown()
                {
                    Time.timeScale = 1.0f;
                }

                [Test]
                public void デバフ_通常状態になること()
                {
                    _sut.DeBuff();
                    Assert.That(_sut.State(), Is.EqualTo(PassiveEffectState.Normal));
                }

                [Test]
                public void バフ_強化状態が維持されること()
                {
                    _sut.Buff();
                    Assert.That(_sut.State(), Is.EqualTo(PassiveEffectState.Strengthening));
                }

                [UnityTest]
                public IEnumerator バフ_持続時間が30秒にリセットされること()
                {
                    Time.timeScale = 100.0f;
                    yield return new WaitForSeconds(10f);

                    _sut.Buff();
                    Assert.That(_sut.State(), Is.EqualTo(PassiveEffectState.Strengthening));
                    Assert.That(_sut.PassiveEffectExpireTime(), Is.EqualTo(30f).Using(s_expireTimeComparer));
                }

                [UnityTest]
                public IEnumerator 時間切れ_通常状態になること()
                {
                    Time.timeScale = 100.0f;
                    yield return new WaitForSeconds(30f);
                    Assert.That(_sut.State(), Is.EqualTo(PassiveEffectState.Normal));
                }
            }
        }
    }
}
