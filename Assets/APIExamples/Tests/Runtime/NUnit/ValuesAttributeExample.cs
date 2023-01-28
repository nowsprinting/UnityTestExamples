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
    /// 組み合わせを絞り込む<see cref="PairwiseAttribute"/>および<see cref="SequentialAttribute"/>の例も含みます
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
        public void Pairwise属性で組み合わせの絞り込みが可能( // 全網羅では6*6*3*2=216通りのところ、ペアワイズ法によって36通りになる例
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
        public void Sequential属性で組み合わせの固定が可能( // 全網羅では4*4*4=64通りのところ、4通りになる例
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
            var actual = def.GetDamageMultiplier(atk);
            yield return null;

            Assert.That(actual, Is.EqualTo(1.0f));
        }

        [Test]
        public async Task 非同期テストでもValues属性は使用可能(
            [Values(Element.Wood, Element.Fire, Element.Water)]
            Element def,
            [Values(Element.None, Element.Earth, Element.Metal)]
            Element atk)
        {
            var actual = def.GetDamageMultiplier(atk);
            await Task.Delay(0);

            Assert.That(actual, Is.EqualTo(1.0f));
        }

        [Test]
        [Pairwise]
        public async Task 非同期テストでもPairwise属性で組み合わせの絞り込みが可能( // 全網羅では6*6*3*2=216通りのところ、ペアワイズ法によって36通りになる例
            [Values] Element def,
            [Values] Element atk,
            [Values(1, 2, 3)] int thirdArgument,
            [Values] bool fourthArgument)
        {
            var actual = def.GetDamageMultiplier(atk);
            await Task.Delay(0);

            Assert.That(actual, Is.GreaterThanOrEqualTo(0.5f).And.LessThanOrEqualTo(2.0f));
        }

        [Test]
        [Sequential]
        public async Task 非同期テストでもSequential属性で組み合わせの固定が可能( // 全網羅では4*4*4=64通りのところ、4通りになる例
            [Values(Element.Earth, Element.Earth, Element.Metal, Element.Metal)]
            Element def,
            [Values(Element.Metal, Element.None, Element.Earth, Element.None)]
            Element atk,
            [Values(2.0f, 0.5f, 2.0f, 0.5f)] float expected)
        {
            var actual = def.GetDamageMultiplier(atk);
            await Task.Delay(0);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void Range属性で数値型の範囲を指定する例( // 2x2x2x3=24通り
            [Range(0, 1)] int i,
            [Range(2L, 5L, 3L)] long l,
            [Range(0.6f, 0.7f, 0.1f)] float f,
            [Range(0.08d, 0.09d, 0.005d)] double d)
        {
            Assert.That(d + f + l + i, Is.InRange(0, 7));
        }

        [Test]
        public async Task Range属性で数値型の範囲を指定する例_AsyncTest( // 2x2x2x3=24通り
            [Range(0, 1)] int i,
            [Range(2L, 5L, 3L)] long l,
            [Range(0.6f, 0.7f, 0.1f)] float f,
            [Range(0.08d, 0.09d, 0.005d)] double d)
        {
            await Task.Delay(0);

            Assert.That(d + f + l + i, Is.InRange(0, 7));
        }

        [UnityTest]
        public IEnumerator Range属性で数値型の範囲を指定する例_UnityTest( // 2x2x2x3=24通り
            [Range(0, 1)] int i,
            [Range(2L, 5L, 3L)] long l,
            [Range(0.6f, 0.7f, 0.1f)] float f,
            [Range(0.08d, 0.09d, 0.005d)] double d)
        {
            yield return null;

            Assert.That(d + f + l + i, Is.InRange(0, 7));
        }
    }
}
