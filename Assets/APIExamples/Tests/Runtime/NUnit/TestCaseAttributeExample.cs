// Copyright (c) 2021 Koji Hasegawa.
// This software is released under the MIT License.

using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

// ReSharper disable AccessToStaticMemberViaDerivedType

namespace APIExamples.NUnit
{
    /// <summary>
    /// BasicExamplesに含まれる<see cref="Element"/>のパラメタライズドテスト記述例
    /// <see cref="TestCaseAttribute"/>は、入力要素と期待値の組み合わせを指定できます
    /// </summary>
    /// <remarks>
    ///<see cref="UnityTestAttribute"/>と組み合わせては使用できません
    /// </remarks>
    [TestFixture]
    public class TestCaseAttributeExample
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

        [UnityTest, UnityPlatform(exclude = new[] { RuntimePlatform.WebGLPlayer })]
        [TestCase(Element.None, Element.None, 1.0f)]
        [Explicit("実行すると次のメッセージを伴って失敗します: Method has non-valid return value, but no result is expected")]
        public IEnumerator UnityTestとTestCase属性は組み合わせて使用できない(Element def, Element atk, float expected)
        {
            yield return null;
            var actual = def.GetDamageMultiplier(atk);

            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
