// Copyright (c) 2021-2023 Koji Hasegawa.
// This software is released under the MIT License.

using System.Threading.Tasks;
using NUnit.Framework;
using UnityEngine.TestTools;

// ReSharper disable AccessToStaticMemberViaDerivedType

namespace APIExamples.NUnit
{
    /// <summary>
    /// <see cref="PairwiseAttribute"/>によって<see cref="ValuesAttribute"/>および<see cref="ValuesAttribute"/>の組み合わせを絞り込む例。
    /// </summary>
    /// <remarks>
    /// <see cref="UnityTestAttribute"/>と同時に使用すると正しい組み合わせが得られません（むしろ増える）
    /// </remarks>
    [TestFixture]
    public class PairwiseAttributeExample
    {
        [Test]
        [Pairwise]
        public void Values属性の組み合わせを絞り込み可能( // 全網羅では6*6*3*2=216通りのところ、ペアワイズ法によって36通りになる例
            [Values] Element defence,
            [Values] Element attack,
            [Values(1, 2, 3)] int intArgument,
            [Values] bool boolArgument)
        {
            var actual = defence.GetDamageMultiplier(attack);

            Assert.That(actual, Is.GreaterThanOrEqualTo(0.5f).And.LessThanOrEqualTo(2.0f));
        }

        [Test]
        [Pairwise]
        public async Task Values属性の組み合わせを絞り込み可能_AsyncTest( // 全網羅では6*6*3*2=216通りのところ、ペアワイズ法によって36通りになる例
            [Values] Element defence,
            [Values] Element attack,
            [Values(1, 2, 3)] int intArgument,
            [Values] bool boolArgument)
        {
            var actual = defence.GetDamageMultiplier(attack);
            await Task.Delay(0);

            Assert.That(actual, Is.GreaterThanOrEqualTo(0.5f).And.LessThanOrEqualTo(2.0f));
        }

        private static Element[] s_defence1X = { Element.Wood, Element.Fire, Element.Water };
        private static Element[] s_attack1X = { Element.None, Element.Earth, Element.Metal };
        private static int[] s_intValues = { 1, 2, 3 };
        private static bool[] s_boolValues = { false, true };

        [Test]
        [Pairwise]
        public void ValueSource属性の組み合わせも絞り込み可能( // 全網羅では3*3*3*2=54通りのところ、ペアワイズ法によって10通りになる例
            [ValueSource(nameof(s_defence1X))] Element defence,
            [ValueSource(nameof(s_attack1X))] Element attack,
            [ValueSource(nameof(s_intValues))] int intArgument,
            [ValueSource(nameof(s_boolValues))] bool boolArgument)
        {
            var actual = defence.GetDamageMultiplier(attack);

            Assert.That(actual, Is.GreaterThanOrEqualTo(0.5f).And.LessThanOrEqualTo(2.0f));
        }

        [Test]
        [Pairwise]
        public async Task ValueSource属性の組み合わせも絞り込み可能_AsyncTest( // 全網羅では3*3*3*2=54通りのところ、ペアワイズ法によって10通りになる例
            [ValueSource(nameof(s_defence1X))] Element defence,
            [ValueSource(nameof(s_attack1X))] Element attack,
            [ValueSource(nameof(s_intValues))] int intArgument,
            [ValueSource(nameof(s_boolValues))] bool boolArgument)
        {
            var actual = defence.GetDamageMultiplier(attack);
            await Task.Delay(0);

            Assert.That(actual, Is.GreaterThanOrEqualTo(0.5f).And.LessThanOrEqualTo(2.0f));
        }
    }
}
