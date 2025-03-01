// Copyright (c) 2021-2025 Koji Hasegawa.
// This software is released under the MIT License.

using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using NUnit.Framework;
using UnityEngine.TestTools;

// ReSharper disable AccessToStaticMemberViaDerivedType

namespace APIExamples.NUnit
{
    /// <summary>
    /// <see cref="TestCaseAttribute"/> によるパラメタライズドテストの記述例.
    /// <p/>
    /// <see cref="TestCaseAttribute"/> は、入力要素と期待値の組み合わせを指定できます。
    /// <see cref="UnityTestAttribute"/> と組み合わせては使用できません。
    /// </summary>
    /// <remarks>
    /// 1xダメージのテストケースは <see cref="ValuesAttributeExample"/> を参照してください。
    /// </remarks>
    [TestFixture]
    public class TestCaseAttributeExample
    {
        [TestCase(Element.Fire, Element.Water)]
        [TestCase(Element.Water, Element.Wood)]
        [TestCase(Element.Wood, Element.Fire)]
        public void GetDamageMultiplier_弱点属性からの攻撃はダメージ2x(Element defence, Element attack)
        {
            var actual = defence.GetDamageMultiplier(attack);

            Assert.That(actual, Is.EqualTo(2.0f));
        }

        [Explicit("実行すると次のメッセージを伴って失敗します: Method has non-valid return value, but no result is expected")]
        [UnityTest]
        [TestCase(Element.Fire, Element.Water)]
        [TestCase(Element.Water, Element.Wood)]
        [TestCase(Element.Wood, Element.Fire)]
        [SuppressMessage("ReSharper", "NUnit.TestCaseAttributeRequiresExpectedResult")]
        public IEnumerator UnityTestでTestCase属性は使用できない(Element defence, Element attack)
        {
            var actual = defence.GetDamageMultiplier(attack);
            yield return null;

            Assert.That(actual, Is.EqualTo(2.0f));
        }

        [TestCase(Element.Fire, Element.Water)]
        [TestCase(Element.Water, Element.Wood)]
        [TestCase(Element.Wood, Element.Fire)]
        public async Task 非同期テストではTestCase属性を使用できる(Element defence, Element attack)
        {
            var actual = defence.GetDamageMultiplier(attack);
            await Task.Yield();

            Assert.That(actual, Is.EqualTo(2.0f));
        }
    }
}
