// Copyright (c) 2021 Koji Hasegawa.
// This software is released under the MIT License.

using NUnit.Framework;
using NUnit.Framework.Constraints;

// ReSharper disable AccessToStaticMemberViaDerivedType

namespace APIExamples.NUnit
{
    /// <summary>
    /// カスタム<see cref="Constraint"/>の使用例
    /// </summary>
    [TestFixture]
    public class CustomConstraintExample
    {
        [Test]
        public void CustomEqConstraint_Constraintの実装だけで可能な書きかた()
        {
            var actual = "Foo bar";

            Assert.That(actual, new CustomEqConstraint("Foo bar"));
            // 失敗時メッセージ例:
            //  Expected: "Foo bar"（カスタム制約）
            //  But was:  "Foo bar baz"
        }

        [Test]
        public void CustomEqConstraint_Extensionsの実装も行なうと可能な書きかた()
        {
            var actual = "Foo bar";

            Assert.That(actual, Is.Not.Null.And.CustomEq("Foo bar"));
            // 失敗時メッセージ例:
            //  Expected: not null and "Foo bar"（カスタム制約）
            //  But was:  "Foo bar baz"
        }

        [Test]
        public void CustomEqConstraint_Isの実装も行なうと可能な書きかた()
        {
            var actual = "Foo bar";

            Assert.That(actual, Is.CustomEq("Foo bar"));
            // 失敗時メッセージ例:
            //  Expected: "Foo bar"（カスタム制約）
            //  But was:  "Foo bar baz"
        }
    }

    /// <summary>
    /// カスタム<see cref="Constraint"/>の実装例
    /// </summary>
    /// <remarks>
    /// 内容は文字列が一致するか判定しているだけ
    /// </remarks>
    public class CustomEqConstraint : Constraint
    {
        public CustomEqConstraint(params object[] args) : base(args) { }

        public override ConstraintResult ApplyTo(object actual)
        {
            return new ConstraintResult(this, actual, actual.ToString() == Arguments[0].ToString());
        }

        public override string Description { get { return $"\"{Arguments[0]}\"（カスタム制約）"; } }
    }

    /// <summary>
    /// カスタム<see cref="Constraint"/>向けの<see cref="ConstraintExpression"/>拡張メソッド
    /// </summary>
    public static class CustomConstraintExtensions
    {
        public static CustomEqConstraint CustomEq(this ConstraintExpression expression, object expected)
        {
            var constraint = new CustomEqConstraint(expected);
            expression.Append(constraint);
            return constraint;
        }
    }

    /// <summary>
    /// カスタム<see cref="Constraint"/>向けの<see cref="Is"/>クラス
    /// </summary>
    /// <remarks>
    /// Unity Test Framework には NUnit.Framework.Is を継承した UnityEngine.TestTools.Constraints.Is がすでにあるため、
    /// これを継承して作ります。
    /// UnityEngine.TestTools.Constraints.Is が必要なければ、直接 global::NUnit.Framework.Is を継承しても構いません。
    /// </remarks>
    // ReSharper disable once ClassNeverInstantiated.Global
    public class Is : UnityEngine.TestTools.Constraints.Is
    {
        public static CustomEqConstraint CustomEq(object expected)
        {
            return new CustomEqConstraint(expected);
        }
    }
}
