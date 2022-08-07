// Copyright (c) 2021-2022 Koji Hasegawa.
// This software is released under the MIT License.

using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine.TestTools.Utils;

namespace TestDoubleExample.Janken
{
    /// <summary>
    /// 非決定的なテストしかできないじゃんけんロジックに対するテストの例
    /// </summary>
    /// <remarks>
    /// じゃんけんくらいの試行回数であればテストダブルに頼らないテストという選択肢もあります
    /// </remarks>
    public class NondeterministicJankenTest
    {
        [Test]
        [Repeat(100)]
        public void Pon_OrとRepeatによる非決定的なテストの例()
        {
            var sut = new NondeterministicJanken();
            var actual = sut.Pon();

            Assert.That(actual, Is.EqualTo(Hand.Rock).Or.EqualTo(Hand.Paper).Or.EqualTo(Hand.Scissors));
        }

        [TestCase(Hand.Rock, 0.33f)]
        [TestCase(Hand.Scissors, 0.33f)]
        [TestCase(Hand.Paper, 0.33f)]
        [Retry(2)]
        public void Pon_出力が仕様通り分布していることをテストする例(Hand hand, float expectedRate)
        {
            const int NumOfAttempts = 100;
            var comparer = new FloatEqualityComparer(0.1f);
            var actual = new Dictionary<Hand, int>();
            var sut = new NondeterministicJanken();

            // Exercise
            for (var i = 0; i < NumOfAttempts; i++)
            {
                var h = sut.Pon();
                if (actual.ContainsKey(h))
                {
                    actual[h]++;
                }
                else
                {
                    actual.Add(h, 1);
                }
            }

            // Verify
            var rate = (float)actual[hand] / NumOfAttempts;
            Assert.That(rate, Is.EqualTo(expectedRate).Using(comparer));
        }
    }
}
