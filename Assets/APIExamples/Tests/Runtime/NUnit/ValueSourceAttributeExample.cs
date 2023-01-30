// Copyright (c) 2021-2023 Koji Hasegawa.
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
    /// BasicExamplesに含まれる<see cref="Element"/>のパラメタライズドテスト記述例
    /// <see cref="ValueSourceAttribute"/>は組み合わせテストになるため、期待値が同じになる要素ごとにメソッドを定義するのが一般的です
    ///
    /// 組み合わせを絞り込むPairwise属性のサンプルは<see cref="PairwiseAttributeExample"/>を参照してください
    /// 組み合わせを固定するSequential属性のサンプルは<see cref="SequentialAttributeExample"/>を参照してください
    /// </summary>
    [TestFixture]
    public class ValueSourceAttributeExample
    {
        private static Element[] s_defence1X = { Element.Wood, Element.Fire, Element.Water };
        private static Element[] s_attack1X = { Element.None, Element.Earth, Element.Metal };

        [Test]
        public void GetDamageMultiplier_相性によるダメージ倍率は正しい_等倍になる組み合わせ(
            [ValueSource(nameof(s_defence1X))] Element defence,
            [ValueSource(nameof(s_attack1X))] Element attack)
        {
            var actual = defence.GetDamageMultiplier(attack);

            Assert.That(actual, Is.EqualTo(1.0f));
        }

        public static IEnumerable<Element> GetDefenceHalved()
        {
            yield return Element.Earth;
            yield return Element.Metal;
        }

        public static IEnumerable<Element> GetAttackHalved()
        {
            yield return Element.None;
            yield return Element.Wood;
            yield return Element.Fire;
            yield return Element.Water;
        }

        [Test]
        public void GetDamageMultiplier_相性によるダメージ倍率は正しい_半減になる組み合わせ(
            [ValueSource(nameof(GetDefenceHalved))]
            Element defence,
            [ValueSource(nameof(GetAttackHalved))] Element attack)
        {
            var actual = defence.GetDamageMultiplier(attack);

            Assert.That(actual, Is.EqualTo(0.5f));
        }

        [UnityTest]
        public IEnumerator UnityTestでもValueSource属性は使用可能(
            [ValueSource(nameof(s_defence1X))] Element defence,
            [ValueSource(nameof(s_attack1X))] Element attack)
        {
            var actual = defence.GetDamageMultiplier(attack);
            yield return null;

            Assert.That(actual, Is.EqualTo(1.0f));
        }

        [Test]
        public async Task 非同期テストでもValueSource属性は使用可能(
            [ValueSource(nameof(GetDefenceHalved))]
            Element defence,
            [ValueSource(nameof(GetAttackHalved))] Element attack)
        {
            var actual = defence.GetDamageMultiplier(attack);
            await Task.Delay(0);

            Assert.That(actual, Is.EqualTo(0.5f));
        }
    }
}
