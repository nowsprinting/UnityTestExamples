// Copyright (c) 2021-2025 Koji Hasegawa.
// This software is released under the MIT License.

using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using UnityEngine.TestTools;

// ReSharper disable AccessToStaticMemberViaDerivedType

namespace APIExamples.NUnit
{
    /// <summary>
    /// <see cref="TestCaseSourceAttribute"/> によるパラメタライズドテストの記述例.
    /// <p/>
    /// <see cref="TestCaseSourceAttribute"/> は、入力要素と期待値の組み合わせを <c>static</c> <see cref="IEnumerable"/> で指定できます。
    /// <see cref="UnityTestAttribute"/> と組み合わせては使用できません。
    /// </summary>
    /// <remarks>
    /// 1xダメージのテストケースは <see cref="ValueSourceAttributeExample"/> を参照してください。
    /// </remarks>
    [TestFixture]
    public class TestCaseSourceAttributeExample
    {
        // テストケースを静的フィールドで定義
        // Note: TestCaseData は NUnit でなく、Unity Test Framework の提供するデータ型です
        private static TestCaseData[] s_weaknessElementCombinationCases =
        {
            new TestCaseData(Element.Fire, Element.Water), // 火 ← 水
            new TestCaseData(Element.Water, Element.Wood), // 水 ← 木
            new TestCaseData(Element.Wood, Element.Fire),  // 木 ← 火
        };

        [TestCaseSource(nameof(s_weaknessElementCombinationCases))] // フィールドを指定する例
        public void GetDamageMultiplier_弱点属性からの攻撃はダメージ2x(Element defence, Element attack)
        {
            var actual = defence.GetDamageMultiplier(attack);

            Assert.That(actual, Is.EqualTo(2.0f));
        }

        // テストケースを返す静的メソッド
        // Note: TestCaseData は NUnit でなく、Unity Test Framework の提供するデータ型です
        public static IEnumerable<TestCaseData> GetWeaknessElementCombinationCases()
        {
            yield return new TestCaseData(Element.Fire, Element.Water); // 火 ← 水
            yield return new TestCaseData(Element.Water, Element.Wood); // 水 ← 木
            yield return new TestCaseData(Element.Wood, Element.Fire);  // 木 ← 火
        }

        [TestCaseSource(nameof(GetWeaknessElementCombinationCases))] // メソッドを指定する例
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
        public void GetDamageMultiplier_弱点属性からの攻撃はダメージ2x_別クラスの例(Element defence, Element attack)
        {
            var actual = defence.GetDamageMultiplier(attack);

            Assert.That(actual, Is.EqualTo(2.0f));
        }

        [TestCaseSource(nameof(s_weaknessElementCombinationCases))]
        public async Task 非同期テストではTestCaseSource属性を使用できる(Element defence, Element attack)
        {
            var actual = defence.GetDamageMultiplier(attack);
            await Task.Yield();

            Assert.That(actual, Is.EqualTo(2.0f));
        }
    }
}
