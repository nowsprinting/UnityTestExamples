// Copyright (c) 2021-2025 Koji Hasegawa.
// This software is released under the MIT License.

using System.Collections;
using System.Threading.Tasks;
using NUnit.Framework;
using UnityEngine.TestTools;

// ReSharper disable AccessToStaticMemberViaDerivedType

namespace APIExamples.NUnit
{
    /// <summary>
    /// <see cref="ValuesAttribute"/> によるパラメタライズドテストの記述例.
    /// <p/>
    /// <see cref="ValuesAttribute"/> は組み合わせテストになるため、期待値が同じになる要素ごとにメソッドを定義するとわかりやすくなります。
    /// <p/>
    /// 組み合わせ生成ルールのデフォルトは全網羅です。明示的に全網羅であることを示すには <see cref="CombinatorialAttribute"/> を使用できます。
    /// 組み合わせを絞り込む <see cref="PairwiseAttribute"/> のサンプルは <see cref="PairwiseAttributeExample"/> を参照してください。
    /// 組み合わせを固定する <see cref="SequentialAttribute"/> のサンプルは <see cref="SequentialAttributeExample"/> を参照してください。
    /// </summary>
    /// <remarks>
    /// 数値型にでたらめな値を与える <see cref="RandomAttribute"/> のサンプルは <see cref="RandomAttributeExample"/> を参照してください。
    /// 数値型の値を範囲指定で与える <see cref="RangeAttribute"/> のサンプルは <see cref="RangeAttributeExample"/> を参照してください。
    /// <p/>
    /// 2xダメージのテストケースは <see cref="TestCaseAttributeExample"/> を参照してください。
    /// </remarks>
    [TestFixture]
    public class ValuesAttributeExample
    {
        [Test]
        public void GetDamageMultiplier_無属性からの攻撃はすべて等倍ダメージ(
            [Values(Element.None, Element.Fire, Element.Water, Element.Wood)]
            Element defence,
            [Values(Element.None)] Element attack)
        {
            var actual = defence.GetDamageMultiplier(attack);

            Assert.That(actual, Is.EqualTo(1.0f));
        }

        [Test]
        public void GetDamageMultiplier_無属性への攻撃はすべて等倍ダメージ(
            [Values(Element.None)] Element defence,
            [Values] Element attack) // Note: enumおよびbool型に対してValue属性の引数を省略したときはすべての値が渡されます
        {
            var actual = defence.GetDamageMultiplier(attack);

            Assert.That(actual, Is.EqualTo(1.0f));
        }

        [Test]
        [ParametrizedIgnore(Element.Fire, Element.Water)]
        [ParametrizedIgnore(Element.Water, Element.Wood)]
        [ParametrizedIgnore(Element.Wood, Element.Fire)]
        public void GetDamageMultiplier_ParametrizedIgnore属性で等倍ダメージにならない組み合わせを除外_すべて等倍ダメージ(
            // Note: ParametrizedIgnore 属性は NUnit でなく、Unity Test Framework v1.4 で追加された属性です
            [Values] Element defence,
            [Values] Element attack)
        {
            var actual = defence.GetDamageMultiplier(attack);

            Assert.That(actual, Is.EqualTo(1.0f));
        }

        [UnityTest]
        [ParametrizedIgnore(Element.Fire, Element.Water)]
        [ParametrizedIgnore(Element.Water, Element.Wood)]
        [ParametrizedIgnore(Element.Wood, Element.Fire)]
        public IEnumerator UnityTestでもValues属性は使用可能(
            // Note: UnityTest で ParametrizedIgnore 属性が使用できるのは、Unity Test Framework v1.4.6以降です
            [Values] Element defence,
            [Values] Element attack)
        {
            var actual = defence.GetDamageMultiplier(attack);
            yield return null;

            Assert.That(actual, Is.EqualTo(1.0f));
        }

        [Test]
        [ParametrizedIgnore(Element.Fire, Element.Water)]
        [ParametrizedIgnore(Element.Water, Element.Wood)]
        [ParametrizedIgnore(Element.Wood, Element.Fire)]
        public async Task 非同期テストでもValues属性は使用可能(
            // Note: Async test で ParametrizedIgnore 属性が使用できるのは、Unity Test Framework v1.4.6以降です
            [Values] Element defence,
            [Values] Element attack)
        {
            var actual = defence.GetDamageMultiplier(attack);
            await Task.Yield();

            Assert.That(actual, Is.EqualTo(1.0f));
        }
    }
}
