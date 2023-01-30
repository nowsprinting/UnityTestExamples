// Copyright (c) 2021-2023 Koji Hasegawa.
// This software is released under the MIT License.

using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using UnityEngine.TestTools;

#pragma warning disable CS1998

// ReSharper disable AccessToStaticMemberViaDerivedType

namespace APIExamples.NUnit
{
    /// <summary>
    /// BasicExamplesに含まれる<see cref="Element"/>のパラメタライズドテスト記述例
    /// <see cref="TestCaseSourceAttribute"/>は、入力要素と期待値の組み合わせを<see cref="IEnumerable"/>で指定できます
    /// </summary>
    /// <remarks>
    /// <see cref="UnityTestAttribute"/>と組み合わせては使用できません
    /// </remarks>
    [TestFixture]
    public class TestCaseSourceAttributeExample
    {
        private static object[] s_testCases =
        {
            new object[] { Element.Wood, Element.Fire, 2.0f }, new object[] { Element.Wood, Element.Water, 0.5f },
            new object[] { Element.Wood, Element.None, 1.0f }, new object[] { Element.Fire, Element.Water, 2.0f },
            new object[] { Element.Fire, Element.Wood, 0.5f }, new object[] { Element.Fire, Element.None, 1.0f },
            new object[] { Element.Water, Element.Wood, 2.0f }, new object[] { Element.Water, Element.Fire, 0.5f },
            new object[] { Element.Water, Element.None, 1.0f },
        };

        [TestCaseSource(nameof(s_testCases))]
        public void GetDamageMultiplier_相性によるダメージ倍率は正しい(Element def, Element atk, float expected)
        {
            var actual = def.GetDamageMultiplier(atk);

            Assert.That(actual, Is.EqualTo(expected));
        }

        public static IEnumerable<object[]> GetEnumerableCase()
        {
            yield return new object[] { Element.Earth, Element.Metal, 2.0f };
            yield return new object[] { Element.Earth, Element.None, 0.5f };
        }

        [TestCaseSource(nameof(GetEnumerableCase))]
        public void GetDamageMultiplier_相性によるダメージ倍率は正しい_Method指定(Element def, Element atk,
            float expected)
        {
            var actual = def.GetDamageMultiplier(atk);

            Assert.That(actual, Is.EqualTo(expected));
        }

        private class TestCasesInClass
        {
            public static IEnumerable<object[]> GetEnumerableCaseInClass()
            {
                yield return new object[] { Element.Metal, Element.Earth, 2.0f };
                yield return new object[] { Element.Metal, Element.None, 0.5f };
            }
        }

        [TestCaseSource(typeof(TestCasesInClass), nameof(TestCasesInClass.GetEnumerableCaseInClass))]
        public void GetDamageMultiplier_相性によるダメージ倍率は正しい_Class指定(Element def, Element atk, float expected)
        {
            var actual = def.GetDamageMultiplier(atk);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Explicit("実行すると次のメッセージを伴って失敗します: Method has non-valid return value, but no result is expected")]
        [UnityTest]
        [TestCaseSource(nameof(s_testCases))]
        public IEnumerator UnityTestでTestCaseSource属性は使用できない(Element def, Element atk, float expected)
        {
            yield return null;
            var actual = def.GetDamageMultiplier(atk);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCaseSource(nameof(s_testCases))]
        public async Task 非同期テストではTestCaseSource属性を使用できる(Element def, Element atk, float expected)
        {
            var actual = def.GetDamageMultiplier(atk);

            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
