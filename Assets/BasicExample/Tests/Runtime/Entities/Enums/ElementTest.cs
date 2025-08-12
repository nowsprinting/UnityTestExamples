// Copyright (c) 2021-2025 Koji Hasegawa.
// This software is released under the MIT License.

using NUnit.Framework;
using UnityEngine.TestTools;

namespace BasicExample.Entities.Enums
{
    /// <summary>
    /// Element（元素的な属性）のテスト
    /// </summary>
    /// <remarks>
    /// <see cref="TestCaseAttribute"/>および<see cref="ValuesAttribute"/>の使用例
    /// </remarks>
    [TestFixture]
    public class ElementTest
    {
        [TestCase(Element.Wood, Element.Fire)]
        [TestCase(Element.Fire, Element.Water)]
        [TestCase(Element.Water, Element.Wood)]
        public void GetDamageMultiplier_弱点属性からの攻撃_ダメージ2倍(Element defence, Element attack)
        {
            var actual = defence.GetDamageMultiplier(attack);
            Assert.That(actual, Is.EqualTo(2.0f));
        }

        [Test]
        [ParametrizedIgnore(Element.Wood, Element.Fire)]
        [ParametrizedIgnore(Element.Fire, Element.Water)]
        [ParametrizedIgnore(Element.Water, Element.Wood)]
        public void GetDamageMultiplier_相性なし_ダメージは等倍([Values] Element defence, [Values] Element attack)
        {
            var actual = defence.GetDamageMultiplier(attack);
            Assert.That(actual, Is.EqualTo(1.0f));
        }

        [Test]
        public void GetName_有効な文字列が返ること([Values] Element e)
        {
            var actual = e.GetName();

            Assert.That(actual, Is.Not.Empty); // 妥当性までは確認しない
        }
    }
}
