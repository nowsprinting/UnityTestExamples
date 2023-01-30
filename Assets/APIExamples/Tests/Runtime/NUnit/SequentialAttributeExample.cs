// Copyright (c) 2021-2023 Koji Hasegawa.
// This software is released under the MIT License.

// ReSharper disable AccessToStaticMemberViaDerivedType

using System.Threading.Tasks;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace APIExamples.NUnit
{
    /// <summary>
    /// <see cref="SequentialAttribute"/>によって<see cref="ValuesAttribute"/>および<see cref="ValuesAttribute"/>の組み合わせを固定する例。
    /// </summary>
    /// <remarks>
    /// <see cref="UnityTestAttribute"/>と同時に使用すると正しい組み合わせが得られません（むしろ増える）
    /// </remarks>
    [TestFixture]
    public class SequentialAttributeExample
    {
        [Test]
        [Sequential]
        public void Values属性の組み合わせを固定可能( // 全網羅では4*4*4=64通りのところ、4通りになる例
            [Values(Element.Earth, Element.Earth, Element.Metal, Element.Metal)]
            Element defence,
            [Values(Element.Metal, Element.None, Element.Earth, Element.None)]
            Element attack,
            [Values(2.0f, 0.5f, 2.0f, 0.5f)] float expected)
        {
            var actual = defence.GetDamageMultiplier(attack);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        [Sequential]
        public async Task Values属性の組み合わせを固定可能_AsyncTest( // 全網羅では4*4*4=64通りのところ、4通りになる例
            [Values(Element.Earth, Element.Earth, Element.Metal, Element.Metal)]
            Element defence,
            [Values(Element.Metal, Element.None, Element.Earth, Element.None)]
            Element attack,
            [Values(2.0f, 0.5f, 2.0f, 0.5f)] float expected)
        {
            var actual = defence.GetDamageMultiplier(attack);
            await Task.Delay(0);

            Assert.That(actual, Is.EqualTo(expected));
        }

        private static Element[] s_defenceSeq = { Element.Earth, Element.Earth, Element.Metal, Element.Metal };
        private static Element[] s_attackSeq = { Element.Metal, Element.None, Element.Earth, Element.None };
        private static float[] s_expected = { 2.0f, 0.5f, 2.0f, 0.5f };

        [Test]
        [Sequential]
        public void ValueSource属性の組み合わせも固定可能( // 全網羅では4*4*4=64通りのところ、4通りになる例
            [ValueSource(nameof(s_defenceSeq))] Element defence,
            [ValueSource(nameof(s_attackSeq))] Element attack,
            [ValueSource(nameof(s_expected))] float expected)
        {
            var actual = defence.GetDamageMultiplier(attack);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        [Sequential]
        public async Task ValueSource属性の組み合わせも固定可能_AsyncTest( // 全網羅では4*4*4=64通りのところ、4通りになる例
            [ValueSource(nameof(s_defenceSeq))] Element defence,
            [ValueSource(nameof(s_attackSeq))] Element attack,
            [ValueSource(nameof(s_expected))] float expected)
        {
            var actual = defence.GetDamageMultiplier(attack);
            await Task.Delay(0);

            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
