// Copyright (c) 2021 Koji Hasegawa.
// This software is released under the MIT License.

using NSubstitute;
using NUnit.Framework;

namespace TestDoubleExample.Dices
{
    /// <summary>
    /// NSubstituteの使用例
    /// NSubstituteのインポート前にはコンパイルエラーとなるため、TestDoubleExample.NSubstitute.Tests.asmdefの
    /// `Define Constraints`に`USE_NUGET_PACKAGES`を設定しています。このサンプルを使用するときはこれを削除してください
    /// </summary>
    public class DiceTestUsingNSubstitute
    {
        [Test]
        public void Roll_NSubstituteで間接入力を固定してテストする例()
        {
            var mock = Substitute.For<IRandom>();
            mock.Next(1, 7).Returns(1); // 呼び出しに対する期待値をセット

            var sut = new Dice(mock);
            var actual = sut.Roll();

            Assert.That(actual, Is.EqualTo(1));
        }

        [Test]
        public void Roll_NSubstituteで間接入力を引数から導出する例()
        {
            var mock = Substitute.For<IRandom>();
            mock
                .Next(Arg.Any<int>(), Arg.Any<int>()) // `Arg.Any<int>()`は任意の整数がマッチする
                .Returns(x => (int)x[0] + 1); // 第一引数の値+1を返すよう指定

            var sut = new Dice(mock);
            var actual = sut.Roll();

            Assert.That(actual, Is.EqualTo(2));
        }

        [Test]
        public void Roll_NSubstituteで間接入力を複数指定してテストする例()
        {
            var mock = Substitute.For<IRandom>();
            mock.Next(1, 7).Returns(1, 2, 3); // 期待値を複数渡すと順に使用されます

            var sut = new Dice(mock);

            Assert.That(sut.Roll(), Is.EqualTo(1));
            Assert.That(sut.Roll(), Is.EqualTo(2));
            Assert.That(sut.Roll(), Is.EqualTo(3));
        }

        [Test]
        public void Roll_NSubstituteで間接出力をテストする例()
        {
            var mock = Substitute.For<IRandom>();
            mock.Next(Arg.Any<int>(), Arg.Any<int>()).Returns(1); // 呼び出しに対する期待値をセット

            var sut = new Dice(mock);
            sut.Roll();

            mock.Received().Next(1, 7); // 引数(1, 7)で呼ばれたことを検証
            mock.DidNotReceive().Next(1, 6); // 引数(1, 6)では呼ばれていないことを検証
            // Note: ここでも、`Arg.Any<int>()`や`Arg.Is<int>(x => x > 0)`を指定できます
        }
    }
}
