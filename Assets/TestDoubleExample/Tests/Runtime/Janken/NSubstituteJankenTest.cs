// Copyright (c) 2021-2025 Koji Hasegawa.
// This software is released under the MIT License.

using NSubstitute;
using NUnit.Framework;

namespace TestDoubleExample.Janken
{
    /// <summary>
    /// NSubstitute によるテストの例
    /// </summary>
    public class NSubstituteJankenTest
    {
        [Test]
        public void Pon_PRNGが0を返すとき_ぐーを返す()
        {
            var stub = Substitute.For<IRandom>();
            stub.Range(0.0f, 1.0f).Returns(0.0f); // 常に0を返すテストスタブを生成
            // Note: Rangeメソッドの引数が (0f, 1f) のとき指定された戻り値を返す

            var sut = new Janken(stub); // テスト対象にスタブを注入
            var actual = sut.Pon();

            Assert.That(actual, Is.EqualTo(Hand.Rock)); // 結果は常に「ぐー」
        }

        [Test]
        public void Pon_PRNGがdot5を返すとき_ちょきを返す()
        {
            var stub = Substitute.For<IRandom>();
            stub.Range(Arg.Any<float>(), Arg.Any<float>()).Returns(0.5f); // 常に0.5を返すテストスタブを生成
            // Note: Rangeメソッドの引数2つの値がいくつであっても指定された戻り値を返す（Arg.Any<T>()を使用）

            var sut = new Janken(stub); // テスト対象にスタブを注入
            var actual = sut.Pon();

            Assert.That(actual, Is.EqualTo(Hand.Scissors)); // 結果は常に「ちょき」
        }

        [Test]
        public void Pon_PRNGが1を返すとき_ぱーを返す()
        {
            var stub = Substitute.For<IRandom>();
            stub.Range(0f, 0f).ReturnsForAnyArgs(1.0f); // 常に1を返すテストスタブを生成
            // Note: Rangeメソッドの引数2つの値がいくつであっても指定された戻り値を返す（ReturnsForAnyArgsを使用）

            var sut = new Janken(stub); // テスト対象にスタブを注入
            var actual = sut.Pon();

            Assert.That(actual, Is.EqualTo(Hand.Paper)); // 結果は常に「ぱー」
        }

        [Test]
        public void NSubstituteで常に最大値を返すスタブの例()
        {
            var stub = Substitute.For<IRandom>();
            stub.Range(Arg.Any<float>(), Arg.Any<float>()).Returns(x => (float)x[1]); // 第二引数の値（つまり最大値）を返す

            var sut = new Janken(stub);
            var actual = sut.Pon();

            Assert.That(actual, Is.EqualTo(Hand.Paper)); // 結果は常に「ぱー」
        }

        [Test]
        public void NSubstituteで間接入力を複数指定する例()
        {
            var stub = Substitute.For<IRandom>();
            stub.Range(0f, 0f).ReturnsForAnyArgs(0.0f, 0.5f, 1.0f); // 戻り値を複数指定すると順に使用されます
            var sut = new Janken(stub);

            Assert.That(sut.Pon(), Is.EqualTo(Hand.Rock));     // 1回目は常に「ぐー」
            Assert.That(sut.Pon(), Is.EqualTo(Hand.Scissors)); // 2回目は常に「ちょき」
            Assert.That(sut.Pon(), Is.EqualTo(Hand.Paper));    // 3回目は常に「ぱー」
        }

        [Test]
        public void Pon_PRNGのRangeに要求している値域は0から1であること()
        {
            var spy = Substitute.For<IRandom>(); // テストスパイを生成
            var sut = new Janken(spy);           // テスト対象にスパイを注入
            sut.Pon();

            spy.Received().Range(0.0f, 1.0f);                                           // 引数 (0f, 1f) で呼ばれたことを検証
            spy.DidNotReceive().Range(Arg.Is<float>(x => x != 0.0f), Arg.Any<float>()); // 第一引数は 0f 以外では呼ばれていないことを検証
            spy.DidNotReceive().Range(Arg.Any<float>(), Arg.Is<float>(x => x != 1.0f)); // 第二引数は 1f 以外では呼ばれていないことを検証
        }
    }
}
