// Copyright (c) 2021-2025 Koji Hasegawa.
// This software is released under the MIT License.

using NUnit.Framework;
using TestDoubleExample.TestDoubles;

namespace TestDoubleExample.Janken
{
    /// <summary>
    /// テストダブルによるテストの例
    /// </summary>
    public class JankenTest
    {
        [Test]
        public void Pon_PRNGが0を返すとき_ぐーを返す()
        {
            var stub = new StubRandom(0.0f); // 常に0を返すテストスタブを生成
            var sut = new Janken(stub);      // テスト対象にスタブを注入
            var actual = sut.Pon();

            Assert.That(actual, Is.EqualTo(Hand.Rock)); // 結果は常に「ぐー」
        }

        [Test]
        public void Pon_PRNGがdot5を返すとき_ちょきを返す()
        {
            var stub = new StubRandom(0.5f); // 常に0.5を返すテストスタブを生成
            var sut = new Janken(stub);      // テスト対象にスタブを注入
            var actual = sut.Pon();

            Assert.That(actual, Is.EqualTo(Hand.Scissors)); // 結果は常に「ちょき」
        }

        [Test]
        public void Pon_PRNGが1を返すとき_ぱーを返す()
        {
            var stub = new StubRandom(1.0f); // 常に1を返すテストスタブを生成
            var sut = new Janken(stub);      // テスト対象にスタブを注入
            var actual = sut.Pon();

            Assert.That(actual, Is.EqualTo(Hand.Paper)); // 結果は常に「ぱー」
        }

        [Test]
        public void Pon_PRNGのRangeに要求している値域は0から1であること()
        {
            var spy = new SpyRandom(); // テストスパイを生成
            var sut = new Janken(spy); // テスト対象にスパイを注入
            sut.Pon();                 // Note: SUTの戻り値は検証しない

            Assert.That(spy.LastMinInclusive, Is.EqualTo(0.0f));
            Assert.That(spy.LastMaxInclusive, Is.EqualTo(1.0f));
        }
    }
}
