// Copyright (c) 2021-2022 Koji Hasegawa.
// This software is released under the MIT License.

using NSubstitute;
using NUnit.Framework;

namespace TestDoubleExample.Janken
{
    /// <summary>
    /// NSubstituteの使用例
    /// NSubstituteのインポート前にはコンパイルエラーとなるため、TestDoubleExample.NSubstitute.Tests.asmdefの
    /// `Define Constraints`に`USE_NUGET_PACKAGES`を設定しています。このサンプルを使用するときはこれを削除してください
    /// </summary>
    public class JankenTest
    {
        [Test]
        public void Pon_NSubstituteで間接入力を固定してテストする例()
        {
            var stub = Substitute.For<IRandom>();
            stub.Range(0, 3).Returns(0); // Range(0, 3)に対し、常に0を返すスタブを設定

            var sut = new Janken(stub); // テスト対象にスタブをセット
            var actual = sut.Pon();

            Assert.That(actual, Is.EqualTo(Hand.Rock)); // 結果は常に「ぐー」
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
            stub.Range(Arg.Any<int>(), Arg.Any<int>()) // `Arg.Any<int>()`は任意の整数がマッチする
                .Returns(x => (int)x[1] - 1); // 第二引数の値-1（つまり最大値）を返すよう指定

            var sut = new Janken(stub);
            var actual = sut.Pon();

            Assert.That(actual, Is.EqualTo(Hand.Paper)); // 結果は常に「ぱー」
        }

        [Test]
        public void Pon_NSubstituteで間接出力をテストする例()
        {
            var spy = Substitute.For<IRandom>();
            var sut = new Janken(spy);
            sut.Pon();

            spy.Received().Range(0, 3); // 引数(0, 3)で呼ばれたことを検証
            spy.DidNotReceive().Range(Arg.Is<int>(x => x != 0), Arg.Any<int>()); // 第一引数は0以外では呼ばれていないことを検証
            spy.DidNotReceive().Range(Arg.Any<int>(), Arg.Is<int>(x => x != 3)); // 第二引数は3以外では呼ばれていないことを検証
        }
    }
}
