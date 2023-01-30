// Copyright (c) 2021 Koji Hasegawa.
// This software is released under the MIT License.

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.TestTools;

// ReSharper disable AccessToStaticMemberViaDerivedType

namespace APIExamples.NUnit
{
    /// <summary>
    /// <see cref="Constraint"/>の記述例
    /// </summary>
    /// <remarks>
    /// ネストしたTestFixtureは、Test Runnerウィンドウの"Run Selected"で個別実行できない場合があります
    /// </remarks>
    [TestFixture]
    public class ConstraintExample
    {
        [TestFixture]
        public class 等価
        {
            [Test]
            public void EqualConstraint_文字列が等しいこと()
            {
                var actual = "Semper Paratus!";

                Assert.That(actual, Is.EqualTo("Semper Paratus!"));
                // 失敗時メッセージ例:
                //  String lengths are both 15. Strings differ at index 7.
                //  Expected: "Semper Paratus!"
                //  But was:  "Semper paratus!"
                //  ------------------^
            }

            [Test]
            public void EqualConstraint_文字列比較にIgnoreCase修飾子を使用()
            {
                var actual = "semper paratus!";

                Assert.That(actual, Is.EqualTo("Semper Paratus!").IgnoreCase);
                // 失敗時メッセージ例:
                //  String lengths are both 15. Strings differ at index 3.
                //  Expected: "Semper Paratus!", ignoring case
                //  But was:  "sember paratus!"
                //  --------------^
            }

            [Test]
            public void EqualConstraint_文字列比較にNoClip修飾子を使用()
            {
                var actual = "Spam, spam, spam, spam, spam, spam, spam, spam, lovely spam! Wonderful spam!";

                Assert.That(actual,
                    Is.EqualTo("Spam, spam, spam, spam, spam, spam, spam, spam, lovely spam! Wonderful spam!").NoClip);
                // 失敗時メッセージ例:
                //  Expected string length 76 but was 82. Strings differ at index 48.
                //  Expected: "Spam, spam, spam, spam, spam, spam, spam, spam, lovely spam! Wonderful spam!"
                //  But was:  "Spam, spam, spam, spam, spam, spam, spam, spam, spam, lovely spam! Wonderful spam!"
                //  -----------------------------------------------------------^

                // 失敗時メッセージ例（NoClipなしの場合）:
                //  Expected string length 76 but was 82. Strings differ at index 48.
                //  Expected: "...m, spam, spam, spam, spam, lovely spam! Wonderful spam!"
                //  But was:  "...m, spam, spam, spam, spam, spam, lovely spam! Wonderful spam!"
                //  -----------------------------------------^
            }

            [Test]
            public void EqualConstraint_数値が等しいこと()
            {
                var actual = 42;

                Assert.That(actual, Is.EqualTo(42));
                // 失敗時メッセージ例:
                //  Expected: 42
                //  But was:  41
            }

            [Test]
            public void EqualConstraint_数値比較にWithin修飾子を使用()
            {
                var actual = 42;

                Assert.That(actual, Is.EqualTo(44.0f).Within(2)); // +/- 2まで許容
                Assert.That(actual, Is.EqualTo(44.0f).Within(5).Percent); // +/- 5%まで許容
                // 失敗時メッセージ例:
                //  Expected: 44.0f +/- 5 Percent
                //  But was:  41
            }

            [Test]
            public void EqualConstraint_浮動小数点比較にWithin修飾子を使用して丸め誤差を許容()
            {
                var actual = 20000000000000004.0d;

                Assert.That(actual, Is.EqualTo(20000000000000000.0d).Within(1).Ulps); // ULPs: Units in the Last Place
                // 失敗時メッセージ例:
                //  Expected: 20000000000000000.0d +/- 1 Ulps
                //  But was:  20000000000000008.0d
            }

            [Test]
            public void EqualConstraint_日時比較にWithin修飾子を使用()
            {
                var actual = new DateTime(2021, 7, 25, 0, 0, 0, 0);

                Assert.That(actual, Is.EqualTo(
                    new DateTime(2021, 7, 25, 0, 5, 0, 0)).Within(TimeSpan.FromSeconds(300))); // +/- 300秒まで許容
                Assert.That(actual, Is.EqualTo(
                    new DateTime(2021, 7, 24, 22, 00, 0, 0)).Within(120).Minutes); // +/- 120分まで許容
                Assert.That(actual, Is.EqualTo(
                    new DateTime(2021, 7, 10, 10, 0, 0, 0)).Within(15).Days); // +/- 15日まで許容
                // 失敗時メッセージ例:
                //  Expected: 2021-07-10 10:00:00.000 +/- 15.00:00:00
                //  But was:  2021-07-25 23:59:59.999
            }

            [Test]
            public void EqualConstraint_配列が等しいこと()
            {
                var actual = new[] { "Katsuro", "Jenny", "Lindsay" };

                Assert.That(actual, Is.EqualTo(new[] { "Katsuro", "Jenny", "Lindsay" }));
                // 失敗時メッセージ例:
                //  Expected and actual are both <System.String[3]>
                //  Values differ at index [1]
                //  Expected string length 5 but was 7. Strings differ at index 0.
                //  Expected: "Jenny"
                //  But was:  "Lindsay"
                //  -----------^
            }

            [Test]
            public void EqualConstraint_要素数の異なるコレクションにAsCollection修飾子を使用()
            {
                var actual = new[,] { { 2, 3 }, { 5, 6 } };
                var expected = new[] { 2, 3, 5, 6 };

                Assert.That(actual, Is.EqualTo(expected).AsCollection);
                // 失敗時メッセージ例:
                //  Expected is <System.Int32[4]>, actual is <System.Int32[3,2]>
                //  Values differ at expected index [2], actual index [1,0]
                //  Expected: 5
                //  But was:  4
            }

            [Test]
            public void SameAsConstraint_オブジェクトが同一であるとみなせること()
            {
                var actual = new ArgumentException();
                var expected = actual;

                Assert.That(actual, Is.SameAs(expected));
                // 失敗時メッセージ例:
                //  Expected: same as <System.ArgumentException: Value does not fall within the expected range.>
                //  But was:  <System.ArgumentException: Value does not fall within the expected range.>
            }
        }

        [TestFixture]
        public class コレクション
        {
            [Test]
            public void AllItemsConstraint_すべての要素を検証()
            {
                var actual = new[] { 2, 3, 5, 6 };

                Assert.That(actual, Is.All.InstanceOf<int>());
                Assert.That(actual, Is.All.GreaterThanOrEqualTo(2));
                Assert.That(actual, Is.All.LessThanOrEqualTo(6));
                Assert.That(actual, Is.All.Not.EqualTo(4));
                // 失敗時メッセージ例:
                //  Expected: all items not equal to 4
                //  But was:  < 2, 3, 4, 5, 6 >
            }

            // AnyOfConstraint は未実装

            [Test]
            public void CollectionContainsConstraint_要素に含まれること()
            {
                var actual = new[] { "Katsuro", "Jenny", "Lindsay" };

                Assert.That(actual, Has.Member("Katsuro"));
                Assert.That(actual, Contains.Item("Jenny"));
                Assert.That(actual, Does.Contain("Lindsay"));
                // 失敗時メッセージ例:
                //  Expected: collection containing "Lindsay"
                //  But was:  < "Katsuro", "Jenny" >

                // SomeItemsConstraint も使用できます
            }

            [Test]
            public void CollectionEquivalentConstraint_要素が順不同で一致すること()
            {
                var actual = new[] { "Katsuro", "Jenny", "Lindsay" };

                Assert.That(actual, Is.EquivalentTo(new[] { "Jenny", "Lindsay", "Katsuro" }));
                // 失敗時メッセージ例:
                //  Expected: equivalent to < "Jenny", "Lindsay", "Katsuro" >
                //  But was:  < "Bill", "Jenny", "Lindsay" >
            }

            [Test]
            public void CollectionOrderedConstraint_要素がソートされていること()
            {
                var ascended = new[] { 2, 3, 5, 6 };
                var descended = new[] { 6, 5, 3, 2 };

                Assert.That(ascended, Is.Ordered);
                Assert.That(descended, Is.Ordered.Descending);
                // 失敗時メッセージ例:
                //  Expected: collection ordered, descending
                //  But was:  < 6, 5, 3, 4 >
            }

            [Test]
            public void CollectionSubsetConstraint_サブセットであること()
            {
                var actual = new[] { 2, 3 };

                Assert.That(actual, Is.SubsetOf(new[] { 2, 3, 5, 6 }));
                // 失敗時メッセージ例:
                //  Expected: subset of < 2, 3, 5, 6 >
                //  But was:  < 2, 3, 4 >
            }

            [Test]
            public void CollectionSupersetConstraint_スーパーセットであること()
            {
                var actual = new[] { 4, 5, 6, 1, 2, 3, 7, 8, 9 };

                Assert.That(actual, Is.SupersetOf(new[] { 2, 3, 5, 6 }));
                // 失敗時メッセージ例:
                //  Expected: superset of < 2, 3, 5, 6 >
                //  But was:  < 4, 5, 6, 7, 8, 9 >
            }

            [Test]
            public void DictionaryContainsKeyConstraint_Dictionaryにキーが含まれること()
            {
                var actual = new Dictionary<int, string>
                {
                    { 1, "Aquatica" }, { 2, "Akheilos" }, { 3, "Little Happy" },
                };

                Assert.That(actual, Contains.Key(3));
                // 失敗時メッセージ例:
                //  Expected: dictionary containing key 3
                //  But was:  < [1, Aquatica], [2, Akheilos] >

                // Does.ContainKey(object) は未実装
                // Does.Not.ContainKey(object) は未実装
            }

            // DictionaryContainsKeyValuePairConstraint は未実装

            [Test]
            public void DictionaryContainsValueConstraint_Dictionaryに値が含まれること()
            {
                var actual = new Dictionary<int, string>
                {
                    { 1, "Aquatica" }, { 2, "Akheilos" }, { 3, "Little Happy" },
                };

                Assert.That(actual, Contains.Value("Little Happy"));
                // 失敗時メッセージ例:
                //  Expected: dictionary containing value "Little Happy"
                //  But was:  < [1, Aquatica], [2, Akheilos] >

                // Does.ContainValue(object) は未実装
                // Does.Not.ContainValue(object) は未実装
            }

            [Test]
            public void EmptyCollectionConstraint_要素が空であること()
            {
                var actual = new int[] { };

                Assert.That(actual, Is.Empty);
                // 失敗時メッセージ例:
                //  Expected: <empty>
                //  But was:  < 0 >
            }

            [Test]
            public void ExactCountConstraint_条件を満たす要素数を検証する()
            {
                var actual = new[] { 2, 3, 5, 6 };

                Assert.That(actual, Has.Exactly(3).GreaterThan(2), "2より大きい要素が3件あること");
                // 失敗時メッセージ例:
                //  Expected: exactly 3 items greater than 2
                //  But was:  < 2, 3, 4, 5, 6 >
            }

            [Test]
            public void NoItemConstraint_条件を満たす要素が含まれないこと()
            {
                var actual = new[] { "Katsuro", "Jenny", "Lindsay" };

                Assert.That(actual, Has.None.Null);
                Assert.That(actual, Has.None.EqualTo("Bill"));
                // 失敗時メッセージ例:
                //  Expected: no item equal to "Bill"
                //  But was:  < "Bill", "Jenny", "Lindsay" >
            }

            [Test]
            public void SomeItemsConstraint_条件を満たす要素が含まれること()
            {
                var actual = new[] { 2, 3, 5, 6 };

                Assert.That(actual, Has.Some.GreaterThan(5));
                // 失敗時メッセージ例:
                //  Expected: some item greater than 5
                //  But was:  < 2, 3, 5 >
            }

            [Test]
            public void UniqueItemsConstraint_重複する要素がないこと()
            {
                var actual = new[] { 2, 3, 5, 6 };

                Assert.That(actual, Is.Unique);
                // 失敗時メッセージ例:
                //  Expected: all items unique
                //  But was:  < 2, 3, 5, 3 >
            }
        }

        [TestFixture]
        public class 比較
        {
            [Test]
            public void GreaterThanConstraint_より大きい()
            {
                var actual = 42;

                Assert.That(actual, Is.GreaterThan(41));
                // 失敗時メッセージ例:
                //  Expected: greater than 41
                //  But was:  41
            }

            [Test]
            public void GreaterThanOrEqualConstraint_以上()
            {
                var actual = 42;

                Assert.That(actual, Is.GreaterThanOrEqualTo(42));
                // 失敗時メッセージ例:
                //  Expected: greater than or equal to 42
                //  But was:  41
            }

            [Test]
            public void LessThanConstraint_より小さい()
            {
                var actual = 42;

                Assert.That(actual, Is.LessThan(43));
                // 失敗時メッセージ例:
                //  Expected: less than 43
                //  But was:  43
            }

            [Test]
            public void LessThanOrEqualConstraint_以下()
            {
                var actual = 42;

                Assert.That(actual, Is.LessThanOrEqualTo(42));
                // 失敗時メッセージ例:
                //  Expected: less than or equal to 42
                //  But was:  43
            }

            [Test]
            public void RangeConstraint_範囲内()
            {
                var actual = 42;

                Assert.That(actual, Is.InRange(40, 44));
                // 失敗時メッセージ例:
                //  Expected: in range (40,44)
                //  But was:  39
            }
        }

        [TestFixture]
        public class 合成
        {
            [Test]
            public void AndConstraint_AND条件()
            {
                var actual = 42;

                Assert.That(actual, Is.GreaterThan(40).And.LessThan(44));
                // 失敗時メッセージ例:
                //  Expected: greater than 40 and less than 44
                //  But was:  40
            }

            [Test]
            public void AndConstraint_OR条件が優先される()
            {
                var actual = 0;

                Assert.That(actual, Is.GreaterThan(40).And.LessThan(44).Or.EqualTo(0));
            }

            [Test]
            public void NotConstraint_NOT条件()
            {
                var actual = 42;

                Assert.That(actual, Is.Not.EqualTo(0));
                // 失敗時メッセージ例:
                //  Expected: not equal to 0
                //  But was:  0
            }

            [Test]
            public void OrConstraint_OR条件()
            {
                var actual = 42;

                Assert.That(actual, Is.GreaterThan(40).Or.LessThan(30));
                // 失敗時メッセージ例:
                //  Expected: greater than 40 or less than 30
                //  But was:  40
            }
        }

        [TestFixture]
        public class 条件
        {
            [Test]
            public void EmptyConstraint_空であること()
            {
                var actual = "";

                Assert.That(actual, Is.Empty);
                // 失敗時メッセージ例:
                //  Expected: <empty>
                //  But was:  "Semper Paratus!"
            }

            [Test]
            public void FalseConstraint_Falseであること()
            {
                var actual = false;

                Assert.That(actual, Is.False);
                // 失敗時メッセージ例:
                //  Expected: False
                //  But was:  True
            }

            [Test]
            public void NaNConstraint_NaNであること()
            {
                var actual = float.NaN;

                Assert.That(actual, Is.NaN);
                // 失敗時メッセージ例:
                //  Expected: NaN
                //  But was:  42.0f
            }

            [Test]
            public void NullConstraint_Nullであること()
            {
                string actual = null;

                Assert.That(actual, Is.Null);
                // 失敗時メッセージ例:
                //  Expected: null
                //  But was:  <string.Empty>
            }

            [Test]
            public void TrueConstraint_Trueであること()
            {
                var actual = true;

                Assert.That(actual, Is.True);
                Assert.That(actual); // Constraintなしのとき、TrueConstraintで評価される
                // 失敗時メッセージ例:
                //  Expected: True
                //  But was:  False
            }
        }

        [TestFixture]
        public class ファイルとディレクトリ
        {
            [Test]
            [UnityPlatform(RuntimePlatform.LinuxEditor, RuntimePlatform.WindowsEditor, RuntimePlatform.OSXEditor)]
            public void EmptyDirectoryConstraint_ディレクトリが空であること()
            {
#if UNITY_EDITOR
                var dir = UnityEditor.FileUtil.GetUniqueTempPathInProject();
                var actual = Directory.CreateDirectory(dir);

                Assert.That(actual, Is.Empty);
                // 失敗時メッセージ例:
                //  Expected: an empty directory
                //  But was:  <UnityTempFile-113d9721b8aa84bb0a7bf0b6e31e2638>

                Directory.Delete(dir, true);
#endif
            }

            [Test]
            [UnityPlatform(RuntimePlatform.LinuxEditor, RuntimePlatform.WindowsEditor, RuntimePlatform.OSXEditor)]
            public void FileOrDirectoryExistsConstraint_ファイルまたはディレクトリが存在すること()
            {
#if UNITY_EDITOR
                var dir = Path.GetFileName(UnityEditor.FileUtil.GetUniqueTempPathInProject());
                var directoryInfo = Directory.CreateDirectory(dir);
                var file = Path.Combine(dir, "test");
                var fileInfo = new FileInfo(file);
                using (var writer = File.CreateText(file))
                {
                    writer.Close();
                }

                Assert.That(directoryInfo, Does.Exist);
                Assert.That(dir, Does.Exist.IgnoreFiles); // ファイルは除外する
                Assert.That(fileInfo, Does.Exist);
                Assert.That(file, Does.Exist.IgnoreDirectories); // ディレクトリは除外する
                // 失敗時メッセージ例:
                //  Expected: file or directory exists
                //  But was:  "Temp/UnityTempFile-ac03ee62865a042748c6f54723c2e51b/test"

                Directory.Delete(dir, true);
#endif
            }

            [Test]
            public void SamePathConstraint_パス文字列が等しいこと()
            {
                Assert.That("\\folder1\\.\\junk\\..\\folder2", Is.SamePath("/folder1/folder2"));
                // 失敗時メッセージ例:
                //  Expected: Path matching "/folder1/folder2"
                //  But was:  "\folder1\.\junk\..\folder2\xxx"
            }

            [Test]
            public void SamePathOrUnderConstraint_パス文字列が期待値と同じかその配下であること()
            {
                Assert.That("\\folder1\\.\\junk\\..\\folder2", Is.SamePathOrUnder("/folder1/folder2"));
                Assert.That("\\folder1\\.\\junk\\..\\folder2\\folder3", Is.SamePathOrUnder("/folder1/folder2"));
                // 失敗時メッセージ例:
                //  Expected: Path under or matching "/folder1/folder2"
                //  But was:  "\folder1\.\junk\..\folder3\folder2"
            }

            [Test]
            [Explicit("SubPathConstraintとは振る舞いが異なるようなので保留")]
            public void SubPathOf_SubPathConstraintとは振る舞いが異なる模様()
            {
                Assert.That("/folder1/./junk/../folder2", Is.SubPathOf("/folder1/folder2"));
                Assert.That("/folder1/junk/folder2", Is.Not.SubPathOf("/folder1/folder2"));
                Assert.That(@"C:\folder1\folder2\folder3", Is.SubPathOf(@"C:\Folder1\Folder2/Folder3").IgnoreCase);
                Assert.That("/folder1/folder2/folder3", Is.Not.SubPathOf("/Folder1/Folder2/Folder3").RespectCase);
            }
        }

        [TestFixture]
        public class 文字列
        {
            [Test]
            public void EmptyStringConstraint_空文字列であること()
            {
                var actual = "";

                Assert.That(actual, Is.Empty);
                // 失敗時メッセージ例:
                //  Expected: <empty>
                //  But was:  "Semper Paratus!"
            }

            [Test]
            public void EndsWithConstraint_末尾が一致すること()
            {
                var actual = "Semper Paratus!";

                Assert.That(actual, Does.EndWith("s!"));
                Assert.That(actual, Does.EndWith("S!").IgnoreCase);
                // 失敗時メッセージ例:
                //  Expected: String ending with "s!"
                //  But was:  "Semper Paratus!!"
            }

            [Test]
            public void RegexConstraint_正規表現に適合すること()
            {
                var actual = "Semper Paratus!";

                Assert.That(actual, Does.Match("^Sem.*s!$"));
                Assert.That(actual, Does.Match("^sem.*s!$").IgnoreCase);
                // 失敗時メッセージ例:
                //  Expected: String matching "^Sem.*s!$"
                //  But was:  "Semper Paratus!!"
            }

            [Test]
            public void StartsWithConstraint_先頭が一致すること()
            {
                var actual = "Semper Paratus!";

                Assert.That(actual, Does.StartWith("Se"));
                Assert.That(actual, Does.StartWith("se").IgnoreCase);
                // 失敗時メッセージ例:
                //  Expected: String starting with "Se"
                //  But was:  "Samper Paratus!"
            }

            [Test]
            public void SubstringConstraint_文字列を含む()
            {
                var actual = "Semper Paratus!";

                Assert.That(actual, Does.Contain("per"));
                Assert.That(actual, Does.Contain("para").IgnoreCase);
                // 失敗時メッセージ例:
                //  Expected: String containing "per"
                //  But was:  "Sember Paratus!"
            }
        }

        [TestFixture]
        public class 型
        {
            private interface IFoo
            {
            }

            private class Foo : IFoo
            {
            }

            private class Bar : Foo
            {
            }

            [Test]
            public void AssignableFromConstraint_型が期待値と同じまたはスーパークラスであること()
            {
                var actual = new Foo();

                Assert.That(actual, Is.Not.AssignableFrom<IFoo>());
                Assert.That(actual, Is.AssignableFrom<Foo>());
                Assert.That(actual, Is.AssignableFrom<Bar>());
                // 失敗時メッセージ例:
                //  Expected: assignable from <APIExamples.NUnit.ConstraintExample+型+IFoo>
                //  But was:  <APIExamples.NUnit.ConstraintExample+型+Foo>
            }

            [Test]
            public void AssignableToConstraint_型が期待値と同じまたはサブクラスであること()
            {
                var actual = new Foo();

                Assert.That(actual, Is.AssignableTo<IFoo>());
                Assert.That(actual, Is.AssignableTo<Foo>());
                Assert.That(actual, Is.Not.AssignableTo<Bar>());
                // 失敗時メッセージ例:
                //  Expected: assignable to <APIExamples.NUnit.ConstraintExample+型+Bar>
                //  But was:  <APIExamples.NUnit.ConstraintExample+型+Foo>
            }

            [Test]
            public void ExactTypeConstraint_型が期待値と一致すること()
            {
                var actual = new Foo();

                Assert.That(actual, Is.Not.TypeOf<IFoo>());
                Assert.That(actual, Is.TypeOf<Foo>());
                Assert.That(actual, Is.Not.TypeOf<Bar>());
                // 失敗時メッセージ例:
                //  Expected: <APIExamples.NUnit.ConstraintExample+型+IFoo>
                //  But was:  <APIExamples.NUnit.ConstraintExample+型+Foo>
            }

            [Test]
            public void InstanceOfTypeConstraint_インスタンスの型が期待値と同じまたはサブクラスであること()
            {
                var actual = new Foo();

                Assert.That(actual, Is.InstanceOf<IFoo>());
                Assert.That(actual, Is.InstanceOf<Foo>());
                Assert.That(actual, Is.Not.InstanceOf<Bar>());
                // 失敗時メッセージ例:
                //  Expected: instance of <APIExamples.NUnit.ConstraintExample+型+Bar>
                //  But was:  <APIExamples.NUnit.ConstraintExample+型+Foo>
            }
        }

        [TestFixture]
        public class 例外
        {
            [Test]
            public void ThrowsConstraint_期待する例外がスローされる()
            {
                void GetThrow() => throw new ArgumentException();

                Assert.That(() => GetThrow(), Throws.TypeOf<ArgumentException>());
                // 失敗時メッセージ例:
                //  Expected: <System.ArgumentException>
                //  But was:  <System.NullReferenceException>
            }

            [Test]
            public void ThrowsConstraint_期待するメッセージを持つ例外がスローされる()
            {
                void GetThrowWithMessage() => throw new ArgumentException("Semper Paratus!");

                Assert.That(() => GetThrowWithMessage(),
                    Throws.TypeOf<ArgumentException>().And.Message.EqualTo("Semper Paratus!"));
                // 失敗時メッセージ例:
                //  Expected: <System.ArgumentException> and property Message equal to "Semper Paratus!"
                //  But was:  "sember paratus!"
            }

            [Test]
            public void ThrowsConstraint_UnityEngineのAssert失敗を期待する()
            {
                void GetAssert() => UnityEngine.Assertions.Assert.IsTrue(false);

                Assert.That(() => GetAssert(), Throws.TypeOf<UnityEngine.Assertions.AssertionException>());
                // 失敗時メッセージ例:
                //  Expected: <NUnit.Framework.AssertionException>
                //  But was:  <UnityEngine.Assertions.AssertionException>
            }

            [Test]
            public void ThrowsNothingConstraint_例外がスローされないことを期待する()
            {
                void GetNotThrow() { }

                Assert.That(() => GetNotThrow(), Throws.Nothing);
                // 失敗時メッセージ例:
                //  Expected: No Exception to be thrown
                //  But was:  <System.NullReferenceException: Object reference not set to an instance of an object.
            }
        }

        [TestFixture, Description("Attribute and Property")]
        public class 属性とプロパティ
        {
            public string Foo { get; } = "Bar";

            [Test]
            public void AttributeExistsConstraint_属性がつけられていること()
            {
                var actual = typeof(属性とプロパティ);

                Assert.That(actual, Has.Attribute<DescriptionAttribute>());
                // 失敗時メッセージ例:
                //  Expected: type with attribute <NUnit.Framework.DescriptionAttribute>
                //  But was:  <APIExamples.NUnit.ConstraintExample+属性とプロパティ>
            }

            [Test]
            public void PropertyExistsConstraint_プロパティを持っていること()
            {
                var actual = new 属性とプロパティ();

                Assert.That(actual, Has.Property("Foo"));
                // 失敗時メッセージ例:
                //  Expected: property Bar
                //  But was:  <APIExamples.NUnit.ConstraintExample+属性とプロパティ>
            }

            [Test]
            public void PropertyConstraint_プロパティの値が正しい()
            {
                var actual = new 属性とプロパティ();

                Assert.That(actual, Has.Property("Foo").EqualTo("Bar"));
                // 失敗時メッセージ例:
                //  Expected: property Foo equal to "Bar"
                //  But was:  "Baz"
            }
        }

        [TestFixture]
        public class シリアル化
        {
            [Serializable]
            private class BinarySerializableSample
            {
                public string S { get; set; } = "Semper Paratus!";
            }

            [Test]
            public void BinarySerializableConstraint_バイナリシリアル化が可能であること()
            {
                var actual = new BinarySerializableSample();

                Assert.That(actual, Is.BinarySerializable);
                // 失敗時メッセージ例:
                //  Expected: binary serializable
                //  But was:  <APIExamples.NUnit.ConstraintExample+シリアル化+BinarySerializableSample>
            }

            // ReSharper disable once MemberCanBePrivate.Global
            public class XmlSerializableSample
            {
                public string S { get; set; } = "Semper Paratus!";
            }

            [Test]
            public void XmlSerializableConstraint_XMLシリアル化が可能であること()
            {
                var actual = new XmlSerializableSample();

                Assert.That(actual, Is.XmlSerializable);
                // 失敗時メッセージ例:
                //  Expected: xml serializable
                //  But was:  <APIExamples.NUnit.ConstraintExample+シリアル化+XmlSerializableSample>
            }
        }

        [TestFixture]
        public class 遅延
        {
            [Test]
            [Explicit("Test属性のテストでは期待通り動作しないので除外")]
            public void DelayedConstraint_Test属性のテストでは無効()
            {
                var start = Time.time; // The time at the beginning of this frame

                Assert.That(Time.time, Is.GreaterThan(start + 2.0f).After(2500)); // ミリ秒しか指定できない模様
                // 失敗時メッセージ例:
                //  Expected: greater than 2.33228302f after 2500 millisecond delay
                //  But was:  0.33228299f
            }

            [UnityTest]
            [Explicit("UnityTest属性のテストでも期待通り動作しないので除外")]
            public IEnumerator DelayedConstraint_UnityTest属性のテストでも無効()
            {
                var start = Time.time; // The time at the beginning of this frame

                Assert.That(Time.time, Is.GreaterThan(start + 2.0f).After(2500)); // ミリ秒しか指定できない模様
                // 失敗時メッセージ例:
                //  Expected: greater than 2.39063787f after 2500 millisecond delay
                //  But was:  0.390637904f

                yield return null;
            }
        }
    }
}
