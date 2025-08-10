// Copyright (c) 2021-2025 Koji Hasegawa.
// This software is released under the MIT License.

using System.Threading.Tasks;
using NUnit.Framework;
using UnityEngine.TestTools;

// ReSharper disable AccessToStaticMemberViaDerivedType

namespace APIExamples.NUnit
{
    /// <summary>
    /// <see cref="PairwiseAttribute"/> によって <see cref="ValuesAttribute"/> および <see cref="ValuesAttribute"/> の組み合わせを絞り込むパラメタライズドテストの記述例.
    /// <p/>
    /// 全網羅のサンプルは <see cref="ValuesAttributeExample"/> および <see cref="ValueSourceAttributeExample"/> を参照してください。
    /// 組み合わせを固定する <see cref="SequentialAttribute"/> のサンプルは <see cref="SequentialAttributeExample"/> を参照してください。
    /// </summary>
    /// <remarks>
    /// <see cref="UnityTestAttribute"/> と同時に使用すると正しい組み合わせが得られません（むしろ増える）
    /// </remarks>
    [TestFixture]
    public class PairwiseAttributeExample
    {
        [Test]
        [Pairwise]
        public void Values属性の組み合わせをペアワイズ法で絞り込む例(
                [Values] Element defence,
                [Values] Element attack,
                [Values(1, 2, 3)] int intArgument,
                [Values] bool boolArgument)
            // Note: 全網羅では 4*4*3*2=96 通りのところ、ペアワイズ法によって 16 通りになります
        {
            var actual = defence.GetDamageMultiplier(attack);

            Assert.That(actual, Is.GreaterThanOrEqualTo(1.0f).And.LessThanOrEqualTo(2.0f));
        }

        // テストケースの引数が取る値を静的フィールドで定義
        private static Element[] s_elements = { Element.None, Element.Fire, Element.Water, Element.Wood };
        private static int[] s_intValues = { 1, 2, 3 };
        private static bool[] s_boolValues = { false, true };

        [Test]
        [Pairwise]
        public void ValueSource属性の組み合わせをペアワイズ法で絞り込む例(
                [ValueSource(nameof(s_elements))] Element defence,
                [ValueSource(nameof(s_elements))] Element attack,
                [ValueSource(nameof(s_intValues))] int intArgument,
                [ValueSource(nameof(s_boolValues))] bool boolArgument)
            // Note: 全網羅では 4*4*3*2=96 通りのところ、ペアワイズ法によって 16 通りになります
        {
            var actual = defence.GetDamageMultiplier(attack);

            Assert.That(actual, Is.GreaterThanOrEqualTo(1.0f).And.LessThanOrEqualTo(2.0f));
        }

        [Test]
        [Pairwise]
        public async Task 非同期テストでもPairwise属性は使用可能(
                [Values] Element defence,
                [Values] Element attack,
                [Values(1, 2, 3)] int intArgument,
                [Values] bool boolArgument)
            // Note: 全網羅では 4*4*3*2=96 通りのところ、ペアワイズ法によって 16 通りになります
        {
            var actual = defence.GetDamageMultiplier(attack);
            await Task.Yield();

            Assert.That(actual, Is.GreaterThanOrEqualTo(1.0f).And.LessThanOrEqualTo(2.0f));
        }
    }
}
