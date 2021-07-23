// Copyright (c) 2021 Koji Hasegawa.
// This software is released under the MIT License.

using NUnit.Framework;

namespace BasicExample.Entities
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
        [TestCase(Element.Wood, Element.Fire, 2.0f)]
        [TestCase(Element.Wood, Element.Water, 0.5f)]
        [TestCase(Element.Wood, Element.None, 1.0f)]
        [TestCase(Element.Fire, Element.Water, 2.0f)]
        [TestCase(Element.Fire, Element.Wood, 0.5f)]
        [TestCase(Element.Fire, Element.None, 1.0f)]
        [TestCase(Element.Water, Element.Wood, 2.0f)]
        [TestCase(Element.Water, Element.Fire, 0.5f)]
        [TestCase(Element.Water, Element.None, 1.0f)]
        public void GetDamageMultiplier_木火水の相性によるダメージ倍率は正しい(Element def, Element atk, float expected)
        {
            var actual = def.GetDamageMultiplier(atk);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase(Element.Earth, Element.Metal, ExpectedResult = 2.0f)]
        [TestCase(Element.Earth, Element.None, ExpectedResult = 0.5f)]
        [TestCase(Element.Metal, Element.Earth, ExpectedResult = 2.0f)]
        [TestCase(Element.Metal, Element.None, ExpectedResult = 0.5f)]
        public float GetDamageMultiplier_土金の相性によるダメージ倍率は正しい(Element def, Element atk)
        {
            var actual = def.GetDamageMultiplier(atk);

            return actual;
        }

        [Test]
        public void GetDamageMultiplier_無属性の相性によるダメージ倍率は正しい(
            [Values(Element.None)] Element def,
            [Values] Element atk)
        {
            var actual = def.GetDamageMultiplier(atk);

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
