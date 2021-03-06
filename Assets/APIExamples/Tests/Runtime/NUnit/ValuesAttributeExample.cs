// Copyright (c) 2021 Koji Hasegawa.
// This software is released under the MIT License.

using System.Collections;
using NUnit.Framework;
using UnityEngine.TestTools;

// ReSharper disable AccessToStaticMemberViaDerivedType

namespace APIExamples.NUnit
{
    /// <summary>
    /// BasicExamplesに含まれる<see cref="Element"/>のパラメタライズドテスト記述例
    /// <see cref="ValuesAttribute"/>は組み合わせテストになるため、期待値が同じになる要素ごとにメソッドを定義するのが一般的です
    /// </summary>
    [TestFixture]
    public class ValuesAttributeExample
    {
        [Test]
        public void GetDamageMultiplier_相性によるダメージ倍率は正しい_等倍になる組み合わせ(
            [Values(Element.Wood, Element.Fire, Element.Water)]
            Element def,
            [Values(Element.None, Element.Earth, Element.Metal)]
            Element atk)
        {
            var actual = def.GetDamageMultiplier(atk);

            Assert.That(actual, Is.EqualTo(1.0f));
        }

        [Test]
        public void GetDamageMultiplier_相性によるダメージ倍率は正しい_半減になる組み合わせ(
            [Values(Element.Earth, Element.Metal)] Element def,
            [Values(Element.None, Element.Wood, Element.Fire, Element.Water)]
            Element atk)
        {
            var actual = def.GetDamageMultiplier(atk);

            Assert.That(actual, Is.EqualTo(0.5f));
        }

        [Test]
        public void GetDamageMultiplier_相性によるダメージ倍率は正しい_無属性(
            [Values(Element.None)] Element def,
            [Values] Element atk)
        {
            var actual = def.GetDamageMultiplier(atk);

            Assert.That(actual, Is.EqualTo(1.0f));
        }

        [Test]
        [Pairwise] // Note: UnityTest属性と使用すると正しい組み合わせが得られません（むしろ増える）
        public void GetDamageMultiplier_Pairwise属性で組み合わせの絞り込みが可能(
            [Values] Element def,
            [Values] Element atk,
            [Values(1, 2, 3)] int thirdArgument,
            [Values] bool fourthArgument)
        {
            var actual = def.GetDamageMultiplier(atk);

            Assert.That(actual, Is.GreaterThanOrEqualTo(0.5f).And.LessThanOrEqualTo(2.0f));
        }

        [Test]
        [Sequential] // Note: UnityTest属性と使用すると正しい組み合わせが得られません（むしろ増える）
        public void GetDamageMultiplier_Sequential属性で組み合わせの固定が可能(
            [Values(Element.Earth, Element.Earth, Element.Metal, Element.Metal)]
            Element def,
            [Values(Element.Metal, Element.None, Element.Earth, Element.None)]
            Element atk,
            [Values(2.0f, 0.5f, 2.0f, 0.5f)] float expected)
        {
            var actual = def.GetDamageMultiplier(atk);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [UnityTest]
        public IEnumerator UnityTestでもValues属性は使用可能(
            [Values(Element.Wood, Element.Fire, Element.Water)]
            Element def,
            [Values(Element.None, Element.Earth, Element.Metal)]
            Element atk)
        {
            yield return null;
            var actual = def.GetDamageMultiplier(atk);

            Assert.That(actual, Is.EqualTo(1.0f));
        }
    }
}
