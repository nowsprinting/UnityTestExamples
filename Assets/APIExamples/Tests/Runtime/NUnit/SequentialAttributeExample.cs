// Copyright (c) 2021-2025 Koji Hasegawa.
// This software is released under the MIT License.

// ReSharper disable AccessToStaticMemberViaDerivedType

using System.Threading.Tasks;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace APIExamples.NUnit
{
    /// <summary>
    /// <see cref="SequentialAttribute"/> によって <see cref="ValuesAttribute"/> および <see cref="ValuesAttribute"/> の組み合わせを固定するパラメタライズドテストの記述例.
    /// <p/>
    /// 全網羅のサンプルは <see cref="ValuesAttributeExample"/> および <see cref="ValueSourceAttributeExample"/> を参照してください。
    /// 組み合わせを絞り込む <see cref="PairwiseAttribute"/> のサンプルは <see cref="PairwiseAttributeExample"/> を参照してください。
    /// </summary>
    /// <remarks>
    /// <see cref="UnityTestAttribute"/> と同時に使用すると正しい組み合わせが得られません（むしろ増える）
    /// </remarks>
    [TestFixture]
    public class SequentialAttributeExample
    {
        [Test]
        [Sequential]
        public void Sequential属性によってValues属性の組み合わせを固定(
                [Values(Element.Fire, Element.Water, Element.Wood)]
                Element defence,
                [Values(Element.Water, Element.Wood, Element.Fire)]
                Element attack)
            // Note: 組み合わせが固定され、3件のテストケースになります
        {
            var actual = defence.GetDamageMultiplier(attack);

            Assert.That(actual, Is.EqualTo(2.0f));
        }

        // テストケースの引数が取る値を静的フィールドで定義
        private static Element[] s_defences = { Element.Fire, Element.Water, Element.Wood };
        private static Element[] s_attacks = { Element.Water, Element.Wood, Element.Fire };

        [Test]
        [Sequential]
        public void Sequential属性によってValueSource属性の組み合わせを固定(
                [ValueSource(nameof(s_defences))] Element defence,
                [ValueSource(nameof(s_attacks))] Element attack)
            // Note: 組み合わせが固定され、3件のテストケースになります
        {
            var actual = defence.GetDamageMultiplier(attack);

            Assert.That(actual, Is.EqualTo(2.0f));
        }

        [Test]
        [Sequential]
        public async Task 非同期テストでもSequential属性は使用可能(
                [Values(Element.Fire, Element.Water, Element.Wood)]
                Element defence,
                [Values(Element.Water, Element.Wood, Element.Fire)]
                Element attack)
            // Note: 組み合わせが固定され、3件のテストケースになります
        {
            var actual = defence.GetDamageMultiplier(attack);
            await Task.Yield();

            Assert.That(actual, Is.EqualTo(2.0f));
        }
    }
}
