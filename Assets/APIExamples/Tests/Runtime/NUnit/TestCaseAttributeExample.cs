// Copyright (c) 2021-2025 Koji Hasegawa.
// This software is released under the MIT License.

using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace APIExamples.NUnit
{
    /// <summary>
    /// <see cref="TestCaseAttribute"/> によるパラメタライズドテストの記述例.
    /// <p/>
    /// <see cref="UnityTestAttribute"/> と組み合わせては使用できません。
    /// </summary>
    /// <remarks>
    /// 1xダメージのテストケースは <see cref="ValuesAttributeExample"/> を参照してください。
    /// </remarks>
    /// <seealso cref="TestCaseSourceAttributeExample"/>
    [TestFixture]
    [SuppressMessage("ReSharper", "AccessToStaticMemberViaDerivedType")]
    public class TestCaseAttributeExample
    {
        [TestCase(Element.Fire, Element.Water)] // 火 ← 水
        [TestCase(Element.Water, Element.Wood)] // 水 ← 木
        [TestCase(Element.Wood, Element.Fire)]  // 木 ← 火
        public void GetDamageMultiplier_弱点属性からの攻撃はダメージ2x(Element defence, Element attack)
        {
            var actual = defence.GetDamageMultiplier(attack);
            Assert.That(actual, Is.EqualTo(2.0f));
        }

        [TestCase(Element.Fire, Element.None, ExpectedResult = 1.0f)]
        [TestCase(Element.Fire, Element.Water, ExpectedResult = 2.0f)]
        [TestCase(Element.Fire, Element.Wood, ExpectedResult = 1.0f)]
        [TestCase(Element.Water, Element.None, ExpectedResult = 1.0f)]
        [TestCase(Element.Water, Element.Fire, ExpectedResult = 1.0f)]
        [TestCase(Element.Water, Element.Wood, ExpectedResult = 2.0f)]
        [TestCase(Element.Wood, Element.None, ExpectedResult = 1.0f)]
        [TestCase(Element.Wood, Element.Fire, ExpectedResult = 2.0f)]
        [TestCase(Element.Wood, Element.Water, ExpectedResult = 1.0f)]
        public float ExpectedResultによるアサーションの例(Element defence, Element attack)
        {
            var actual = defence.GetDamageMultiplier(attack);
            return actual; // 戻り値をExpectedResultと比較するアサーションが行われる
        }

        [TestCase(Element.Fire, Element.Water)] // 火 ← 水
        [TestCase(Element.Water, Element.Wood)] // 水 ← 木
        [TestCase(Element.Wood, Element.Fire)]  // 木 ← 火
        public async Task 非同期テストでもTestCase属性を使用できる(Element defence, Element attack)
        {
            await Task.Yield();
            var actual = defence.GetDamageMultiplier(attack);
            Assert.That(actual, Is.EqualTo(2.0f));
        }
    }
}
