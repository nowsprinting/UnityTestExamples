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
    /// <see cref="ValueSourceAttribute"/> によるパラメタライズドテストの記述例.
    /// <p/>
    /// <see cref="ValueSourceAttribute"/> は組み合わせテストになるため、期待値が同じになる要素ごとにテストメソッドを定義するとわかりやすくなります。
    /// <p/>
    /// 組み合わせ生成ルールのデフォルトは全網羅です。明示的に全網羅であることを示すには <see cref="CombinatorialAttribute"/> を使用できます。
    /// 組み合わせを絞り込む <see cref="PairwiseAttribute"/> のサンプルは <see cref="PairwiseAttributeExample"/> を参照してください。
    /// 組み合わせを固定する <see cref="SequentialAttribute"/> のサンプルは <see cref="SequentialAttributeExample"/> を参照してください。
    /// </summary>
    /// <remarks>
    /// 2xダメージのテストケースは <see cref="TestCaseSourceAttributeExample"/> を参照してください。
    /// </remarks>
    [TestFixture]
    public class ValueSourceAttributeExample
    {
        // テストケースの引数が取る値を静的フィールドで定義
        private static Element[] s_elements = { Element.None, Element.Fire, Element.Water, Element.Wood };

        [Test]
        public void GetDamageMultiplier_無属性からの攻撃はすべて等倍ダメージ(
            [ValueSource(nameof(s_elements))] Element defence) // フィールドを指定する例
        {
            var actual = defence.GetDamageMultiplier(Element.None);

            Assert.That(actual, Is.EqualTo(1.0f));
        }

        // テストケースの引数が取る値を返す静的メソッド
        public static IEnumerable<Element> GetElements()
        {
            yield return Element.None;
            yield return Element.Fire;
            yield return Element.Water;
            yield return Element.Wood;
        }

        [Test]
        public void GetDamageMultiplier_無属性への攻撃はすべて等倍ダメージ(
            [ValueSource(nameof(GetElements))] Element attack) // メソッドを指定する例
        {
            var actual = Element.None.GetDamageMultiplier(attack);

            Assert.That(actual, Is.EqualTo(1.0f));
        }

        [Test]
        [ParametrizedIgnore(Element.Fire, Element.Water)]
        [ParametrizedIgnore(Element.Water, Element.Wood)]
        [ParametrizedIgnore(Element.Wood, Element.Fire)]
        public void GetDamageMultiplier_ParametrizedIgnore属性で等倍ダメージにならない組み合わせを除外_すべて等倍ダメージ(
            // Note: ParametrizedIgnore 属性は NUnit でなく、Unity Test Framework v1.4 で追加された属性です
            [ValueSource(nameof(s_elements))] Element defence,
            [ValueSource(nameof(s_elements))] Element attack)
        {
            var actual = defence.GetDamageMultiplier(attack);

            Assert.That(actual, Is.EqualTo(1.0f));
        }

        [UnityTest]
        [ParametrizedIgnore(Element.Fire, Element.Water)]
        [ParametrizedIgnore(Element.Water, Element.Wood)]
        [ParametrizedIgnore(Element.Wood, Element.Fire)]
        public IEnumerator UnityTestでもValueSource属性は使用可能(
            // Note: UnityTest で ParametrizedIgnore 属性が使用できるのは、Unity Test Framework v1.4.6以降です
            [ValueSource(nameof(s_elements))] Element defence,
            [ValueSource(nameof(s_elements))] Element attack)
        {
            var actual = defence.GetDamageMultiplier(attack);
            yield return null;

            Assert.That(actual, Is.EqualTo(1.0f));
        }

        [Test]
        [ParametrizedIgnore(Element.Fire, Element.Water)]
        [ParametrizedIgnore(Element.Water, Element.Wood)]
        [ParametrizedIgnore(Element.Wood, Element.Fire)]
        public async Task 非同期テストでもValueSource属性は使用可能(
            // Note: Async test で ParametrizedIgnore 属性が使用できるのは、Unity Test Framework v1.4.6以降です
            [ValueSource(nameof(s_elements))] Element defence,
            [ValueSource(nameof(s_elements))] Element attack)
        {
            var actual = defence.GetDamageMultiplier(attack);
            await Task.Yield();

            Assert.That(actual, Is.EqualTo(1.0f));
        }
    }
}
