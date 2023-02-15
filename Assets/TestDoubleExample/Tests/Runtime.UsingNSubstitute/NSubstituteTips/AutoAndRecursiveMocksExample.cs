// Copyright (c) 2021-2022 Koji Hasegawa.
// This software is released under the MIT License.

using NSubstitute;
using NUnit.Framework;

namespace TestDoubleExample.NSubstituteTips
{
    /// <summary>
    /// virtualキーワード付きプロパティには自動的にnullでない値を返すスタブが生成されます
    /// stringはstring.Empty、arrayは空のarrayが返ります
    /// interfaceの場合は、再帰的にスタブが生成されます
    /// classの場合は、そのclassのすべてのpublicメソッド・プロパティがvirtualもしくはabstractであれば、再帰的にスタブが生成されます
    /// see https://nsubstitute.github.io/help/auto-and-recursive-mocks/
    /// </summary>
    public class AutoAndRecursiveMocksExample
    {
        [Test]
        public void 通常のインスタンス生成()
        {
            var foo = new Foo();
            Assert.That(foo.i, Is.EqualTo(0));
            Assert.That(foo.s, Is.Null); // stringのデフォルト値はnull
            Assert.That(foo.a, Is.Null); // arrayのデフォルト値はnull
            Assert.That(foo.bar, Is.Null); // classのデフォルトはnull
        }

        [Test]
        public void NSubstituteによる再帰的モック生成の例()
        {
            var foo = Substitute.For<Foo>();

            // Auto values
            Assert.That(foo.i, Is.EqualTo(0));
            Assert.That(foo.s, Is.EqualTo("")); // string.Emptyを返してくれる
            Assert.That(foo.a, Is.EqualTo(new string[] { })); // 空のarrayを返してくれる
            Assert.That(foo.bar, Is.Not.Null); // Barのインスタンスを返してくれる

            // Recursive mocks
            Assert.That(foo.bar.i, Is.EqualTo(0));
            Assert.That(foo.bar.s, Is.EqualTo(""));
            Assert.That(foo.bar.a, Is.EqualTo(new string[] { }));
            Assert.That(foo.bar.baz.i, Is.EqualTo(0));
            Assert.That(foo.bar.baz.s, Is.EqualTo(""));
            Assert.That(foo.bar.baz.a, Is.EqualTo(new string[] { }));
            Assert.That(foo.bar.baz.qux, Is.Null); // Quxはvirtualでないプロパティを含むためインスタンス生成されない
        }

        public class Foo
        {
            public virtual int i { get; }
            public virtual string s { get; }
            public virtual string[] a { get; }
            public virtual Bar bar { get; }
        }

        public interface Bar
        {
            int i { get; }
            string s { get; }
            string[] a { get; }
            Baz baz { get; }
        }

        public abstract class Baz
        {
            public abstract int i { get; }
            public abstract string s { get; }
            public abstract string[] a { get; }
            public abstract Qux qux { get; }
        }

        public class Qux
        {
            public virtual string WithVirtual { get; }
            public string[] WithoutVirtual { get; }
        }
    }
}
