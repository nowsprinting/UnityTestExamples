// Copyright (c) 2021-2023 Koji Hasegawa.
// This software is released under the MIT License.

using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using UnityEngine.TestTools;

#pragma warning disable CS1998

// ReSharper disable AccessToStaticMemberViaDerivedType

namespace APIExamples.NUnit
{
    /// <summary>
    /// BasicExamplesに含まれる<see cref="Element"/>のパラメタライズドテスト記述例
    /// <see cref="ValueSourceAttribute"/>は組み合わせテストになるため、期待値が同じになる要素ごとにメソッドを定義するのが一般的です
    /// </summary>
    [TestFixture]
    public class ValueSourceAttributeExample
    {
        private static Element[] s_defs1x = { Element.Wood, Element.Fire, Element.Water };
        private static Element[] s_atks1x = { Element.None, Element.Earth, Element.Metal };

        [Test]
        public void GetDamageMultiplier_相性によるダメージ倍率は正しい_等倍になる組み合わせ(
            [ValueSource(nameof(s_defs1x))] Element def,
            [ValueSource(nameof(s_atks1x))] Element atk)
        {
            var actual = def.GetDamageMultiplier(atk);

            Assert.That(actual, Is.EqualTo(1.0f));
        }

        public static IEnumerable<Element> GetDefsHalf()
        {
            yield return Element.Earth;
            yield return Element.Metal;
        }

        public static IEnumerable<Element> GetAtksHalf()
        {
            yield return Element.None;
            yield return Element.Wood;
            yield return Element.Fire;
            yield return Element.Water;
        }

        [Test]
        public void GetDamageMultiplier_相性によるダメージ倍率は正しい_半減になる組み合わせ(
            [ValueSource(nameof(GetDefsHalf))] Element def,
            [ValueSource(nameof(GetAtksHalf))] Element atk)
        {
            var actual = def.GetDamageMultiplier(atk);

            Assert.That(actual, Is.EqualTo(0.5f));
        }

        private static int[] s_third = { 1, 2, 3 };
        private static bool[] s_fourth = { false, true };

        [Test]
        [Pairwise] // Note: UnityTest属性と使用すると正しい組み合わせが得られません（むしろ増える）
        public void Pairwise属性で組み合わせの絞り込みが可能( // 全網羅では3*3*3*2=54通りのところ、ペアワイズ法によって10通りになる例
            [ValueSource(nameof(s_defs1x))] Element def,
            [ValueSource(nameof(s_atks1x))] Element atk,
            [ValueSource(nameof(s_third))] int thirdArgument,
            [ValueSource(nameof(s_fourth))] bool fourthArgument)
        {
            var actual = def.GetDamageMultiplier(atk);

            Assert.That(actual, Is.GreaterThanOrEqualTo(0.5f).And.LessThanOrEqualTo(2.0f));
        }

        private static Element[] s_defsSeq = { Element.Earth, Element.Earth, Element.Metal, Element.Metal };
        private static Element[] s_atksSeq = { Element.Metal, Element.None, Element.Earth, Element.None };
        private static float[] s_expected = { 2.0f, 0.5f, 2.0f, 0.5f };

        [Test]
        [Sequential] // Note: UnityTest属性と使用すると正しい組み合わせが得られません（むしろ増える）
        public void Sequential属性で組み合わせの固定が可能( // 全網羅では4*4*4=64通りのところ、4通りになる例
            [ValueSource(nameof(s_defsSeq))] Element def,
            [ValueSource(nameof(s_atksSeq))] Element atk,
            [ValueSource(nameof(s_expected))] float expected)
        {
            var actual = def.GetDamageMultiplier(atk);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [UnityTest]
        public IEnumerator UnityTestでもValueSource属性は使用可能(
            [ValueSource(nameof(s_defs1x))] Element def,
            [ValueSource(nameof(s_atks1x))] Element atk)
        {
            var actual = def.GetDamageMultiplier(atk);
            yield return null;

            Assert.That(actual, Is.EqualTo(1.0f));
        }

        [Test]
        public async Task 非同期テストでもValueSource属性は使用可能(
            [ValueSource(nameof(s_defs1x))] Element def,
            [ValueSource(nameof(s_atks1x))] Element atk)
        {
            var actual = def.GetDamageMultiplier(atk);

            Assert.That(actual, Is.EqualTo(1.0f));
        }

        [Test]
        [Pairwise]
        public async Task 非同期テストでもPairwise属性で組み合わせの絞り込みが可能( // 全網羅では3*3*3*2=54通りのところ、ペアワイズ法によって10通りになる例
            [ValueSource(nameof(s_defs1x))] Element def,
            [ValueSource(nameof(s_atks1x))] Element atk,
            [ValueSource(nameof(s_third))] int thirdArgument,
            [ValueSource(nameof(s_fourth))] bool fourthArgument)
        {
            var actual = def.GetDamageMultiplier(atk);

            Assert.That(actual, Is.GreaterThanOrEqualTo(0.5f).And.LessThanOrEqualTo(2.0f));
        }

        [Test]
        [Sequential]
        public async Task 非同期テストでもSequential属性で組み合わせの固定が可能( // 全網羅では4*4*4=64通りのところ、4通りになる例
            [ValueSource(nameof(s_defsSeq))] Element def,
            [ValueSource(nameof(s_atksSeq))] Element atk,
            [ValueSource(nameof(s_expected))] float expected)
        {
            var actual = def.GetDamageMultiplier(atk);

            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
