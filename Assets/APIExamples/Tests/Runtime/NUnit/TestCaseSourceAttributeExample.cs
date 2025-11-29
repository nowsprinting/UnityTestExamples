// Copyright (c) 2021-2025 Koji Hasegawa.
// This software is released under the MIT License.

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace APIExamples.NUnit
{
    /// <summary>
    /// <see cref="TestCaseSourceAttribute"/> によるパラメタライズドテストの記述例.
    /// <p/>
    /// <see cref="UnityTestAttribute"/> と組み合わせては使用できません。
    /// </summary>
    /// <remarks>
    /// 1xダメージのテストケースは <see cref="ValueSourceAttributeExample"/> を参照してください。
    /// </remarks>
    /// <seealso cref="TestCaseAttributeExample"/>
    [TestFixture]
    [SuppressMessage("ReSharper", "AccessToStaticMemberViaDerivedType")]
    public class TestCaseSourceAttributeExample
    {
        // テストケースを静的フィールドで定義
        private static TestCaseData[] s_weaknessElementCombinationCases =
        {
            new TestCaseData(Element.Fire, Element.Water), // 火 ← 水
            new TestCaseData(Element.Water, Element.Wood), // 水 ← 木
            new TestCaseData(Element.Wood, Element.Fire),  // 木 ← 火
        };

        [TestCaseSource(nameof(s_weaknessElementCombinationCases))]
        public void GetDamageMultiplier_弱点属性からの攻撃はダメージ2x(Element defence, Element attack)
        {
            var actual = defence.GetDamageMultiplier(attack);
            Assert.That(actual, Is.EqualTo(2.0f));
        }

        // テストケースを返す静的メソッド
        public static IEnumerable<TestCaseData> GetWeaknessElementCombinationCases()
        {
            yield return new TestCaseData(Element.Fire, Element.Water); // 火 ← 水
            yield return new TestCaseData(Element.Water, Element.Wood); // 水 ← 木
            yield return new TestCaseData(Element.Wood, Element.Fire);  // 木 ← 火
        }

        [TestCaseSource(nameof(GetWeaknessElementCombinationCases))]
        public void GetDamageMultiplier_弱点属性からの攻撃はダメージ2x_メソッド指定の例(Element defence, Element attack)
        {
            var actual = defence.GetDamageMultiplier(attack);
            Assert.That(actual, Is.EqualTo(2.0f));
        }

        // 別クラスに定義されたテストケースの例
        private class TestCasesInClass
        {
            public static IEnumerable<TestCaseData> GetWeaknessElementCombinationCasesInClass()
            {
                yield return new TestCaseData(Element.Fire, Element.Water);
                yield return new TestCaseData(Element.Water, Element.Wood);
                yield return new TestCaseData(Element.Wood, Element.Fire);
            }
        }

        [TestCaseSource(typeof(TestCasesInClass), nameof(TestCasesInClass.GetWeaknessElementCombinationCasesInClass))]
        public void GetDamageMultiplier_弱点属性からの攻撃はダメージ2x_クラス指定の例(Element defence, Element attack)
        {
            var actual = defence.GetDamageMultiplier(attack);
            Assert.That(actual, Is.EqualTo(2.0f));
        }

        // 名前付きTestCaseData
        private static TestCaseData[] s_weaknessElementCombinationCasesWithName =
        {
            new TestCaseData(Element.Fire, Element.Water).SetName("火 ← 水"),
            new TestCaseData(Element.Water, Element.Wood).SetName("水 ← 木"),
            new TestCaseData(Element.Wood, Element.Fire).SetName("木 ← 火"),
        };

        [TestCaseSource(nameof(s_weaknessElementCombinationCasesWithName))]
        public void TestCaseDataにTestNameプロパティを指定した例(Element defence, Element attack)
        {
            var actual = defence.GetDamageMultiplier(attack);
            Assert.That(actual, Is.EqualTo(2.0f));
        }

        [TestCaseSource(nameof(s_weaknessElementCombinationCases))]
        public async Task 非同期テストでもTestCaseSource属性を使用できる(Element defence, Element attack)
        {
            var actual = defence.GetDamageMultiplier(attack);
            await Task.Yield();

            Assert.That(actual, Is.EqualTo(2.0f));
        }
    }
}
