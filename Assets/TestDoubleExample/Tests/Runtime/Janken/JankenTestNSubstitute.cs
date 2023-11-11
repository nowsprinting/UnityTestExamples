// Copyright (c) 2021-2023 Koji Hasegawa.
// This software is released under the MIT License.

using NSubstitute;
using NUnit.Framework;

namespace TestDoubleExample.Janken
{
    /// <summary>
    /// NSubstituteの使用例
    /// </summary>
    public class JankenTestNSubstitute
    {
        [Test]
        public void Pon_NSubstituteで間接入力を固定してテストする例()
        {
            var stub = Substitute.For<IRandom>();
            stub.Range(0, 3).Returns(0); // Range(0, 3)に対し、常に0を返すスタブを設定

            var sut = new Janken(stub); // テスト対象にスタブを注入
            var actual = sut.Pon();

            Assert.That(actual, Is.EqualTo(Hand.Rock)); // 結果は常に「ぐー」
        }

        [Test]
        public void Pon_NSubstituteで間接入力を固定してテストする例_Anyにより引数を限定しない()
        {
            var stub = Substitute.For<IRandom>();
            stub.Range(Arg.Any<int>(), Arg.Any<int>()).Returns(1); // `Arg.Any<int>()`には任意の整数がマッチする

            var sut = new Janken(stub); // テスト対象にスタブを注入
            var actual = sut.Pon();

            Assert.That(actual, Is.EqualTo(Hand.Scissors)); // 結果は常に「ちょき」
        }

        [Test]
        public void Pon_NSubstituteで間接入力を固定してテストする例_ReturnsForAnyArgsにより引数を限定しない()
        {
            var stub = Substitute.For<IRandom>();
            stub.Range(default, default).ReturnsForAnyArgs(2); // 引数を限定しないで常に2を返す
            // Note: Range()の引数は無視されますが、わかりやすくなるよう`default`を指定しています

            var sut = new Janken(stub); // テスト対象にスタブを注入
            var actual = sut.Pon();

            Assert.That(actual, Is.EqualTo(Hand.Paper)); // 結果は常に「ぱー」
        }

        [Test]
        public void Pon_NSubstituteで間接入力を複数指定してテストする例()
        {
            var stub = Substitute.For<IRandom>();
            stub.Range(0, 3).Returns(0, 1, 2); // 期待値を複数渡すと順に使用されます

            var sut = new Janken(stub);

            Assert.That(sut.Pon(), Is.EqualTo(Hand.Rock)); // 1回目は「ぐー」固定
            Assert.That(sut.Pon(), Is.EqualTo(Hand.Scissors)); // 2回目は「ちょき」固定
            Assert.That(sut.Pon(), Is.EqualTo(Hand.Paper)); // 3回目は「ぱー」固定
        }

        [Test]
        public void Pon_NSubstituteで間接入力を引数から導出する例()
        {
            var stub = Substitute.For<IRandom>();
            stub.Range(Arg.Any<int>(), Arg.Any<int>()).Returns(x => (int)x[1] - 1); // 第二引数の値-1（つまり最大値）を返す

            var sut = new Janken(stub);
            var actual = sut.Pon();

            Assert.That(actual, Is.EqualTo(Hand.Paper)); // 結果は常に「ぱー」
        }

        [Test]
        public void Pon_NSubstituteで間接出力をテストする例()
        {
            var spy = Substitute.For<IRandom>();

            var sut = new Janken(spy); // テスト対象にスパイを注入
            sut.Pon();

            spy.Received().Range(0, 3); // 引数(0, 3)で呼ばれたことを検証

            spy.DidNotReceive().Range(Arg.Is<int>(x => x != 0), Arg.Any<int>()); // 第一引数は0以外では呼ばれていないことを検証
            spy.DidNotReceive().Range(Arg.Any<int>(), Arg.Is<int>(x => x != 3)); // 第二引数は3以外では呼ばれていないことを検証
        }

        [TestFixture]
        public class ClassMockingExamples
        {
            [Test]
            [Explicit("実行時エラーになるため除外")]
            public void Pon_classの非virtualメソッドは置き換えできない()
            {
                var stub = Substitute.For<Random>(); // classを指定
                stub.Range(0, 3).Returns(0); // 実行時にCouldNotSetReturnDueToNoLastCallException発生
                // Note: アナライザー`NSubstitute.Analyzers.CSharp`をプロジェクトに導入すると、このパターンをコンパイル時警告にできます
                // アナライザーが動作するUnityバージョンについては https://www.nowsprinting.com/entry/2021/04/18/200619 を参照してください

                var sut = new Janken(stub);
                var actual = sut.Pon();

                Assert.That(actual, Is.EqualTo(Hand.Rock));
            }

            [Test]
            public void Pon_classでもvirtualメソッドは置き換えできる()
            {
                var stub = Substitute.For<VRandom>(); // classを指定
                stub.Range(0, 3).Returns(1); // classでもvirtualメソッドは置き換えできる

                var sut = new Janken(stub);
                var actual = sut.Pon();

                Assert.That(actual, Is.EqualTo(Hand.Scissors));
            }
        }
    }
}
