// Copyright (c) 2021-2023 Koji Hasegawa.
// This software is released under the MIT License.

using System.Collections;
using System.Threading.Tasks;
using NUnit.Framework;
using UnityEngine.TestTools;

// ReSharper disable AccessToStaticMemberViaDerivedType

namespace APIExamples.NUnit
{
    /// <summary>
    /// BasicExamplesに含まれる<see cref="Element"/>のパラメタライズドテスト記述例
    /// <see cref="ValuesAttribute"/>は組み合わせテストになるため、期待値が同じになる要素ごとにメソッドを定義するのが一般的です
    /// 
    /// 数値型にでたらめな値を与えるRandom属性のサンプルは<see cref="RandomAttribute"/>を参照してください
    /// 数値型の値を範囲指定するRange属性のサンプルは<see cref="RangeAttribute"/>を参照してください
    /// 組み合わせを絞り込むPairwise属性のサンプルは<see cref="PairwiseAttributeExample"/>を参照してください
    /// 組み合わせを固定するSequential属性のサンプルは<see cref="SequentialAttributeExample"/>を参照してください
    /// </summary>
    [TestFixture]
    public class ValuesAttributeExample
    {
        [Test]
        public void GetDamageMultiplier_相性によるダメージ倍率は正しい_等倍になる組み合わせ(
            [Values(Element.Wood, Element.Fire, Element.Water)]
            Element defence,
            [Values(Element.None, Element.Earth, Element.Metal)]
            Element attack)
        {
            var actual = defence.GetDamageMultiplier(attack);

            Assert.That(actual, Is.EqualTo(1.0f));
        }

        [Test]
        public void GetDamageMultiplier_相性によるダメージ倍率は正しい_半減になる組み合わせ(
            [Values(Element.Earth, Element.Metal)] Element defence,
            [Values(Element.None, Element.Wood, Element.Fire, Element.Water)]
            Element attack)
        {
            var actual = defence.GetDamageMultiplier(attack);

            Assert.That(actual, Is.EqualTo(0.5f));
        }

        [Test]
        public void GetDamageMultiplier_相性によるダメージ倍率は正しい_無属性(
            [Values(Element.None)] Element defence,
            [Values] Element attack)
        {
            var actual = defence.GetDamageMultiplier(attack);

            Assert.That(actual, Is.EqualTo(1.0f));
        }

        [UnityTest]
        public IEnumerator UnityTestでもValues属性は使用可能(
            [Values(Element.Wood, Element.Fire, Element.Water)]
            Element defence,
            [Values(Element.None, Element.Earth, Element.Metal)]
            Element attack)
        {
            var actual = defence.GetDamageMultiplier(attack);
            yield return null;

            Assert.That(actual, Is.EqualTo(1.0f));
        }

        [Test]
        public async Task 非同期テストでもValues属性は使用可能(
            [Values(Element.Earth, Element.Metal)] Element defence,
            [Values(Element.None, Element.Wood, Element.Fire, Element.Water)]
            Element attack)
        {
            var actual = defence.GetDamageMultiplier(attack);
            await Task.Delay(0);

            Assert.That(actual, Is.EqualTo(0.5f));
        }
    }
}
