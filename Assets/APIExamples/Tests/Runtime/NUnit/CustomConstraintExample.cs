// Copyright (c) 2021-2023 Koji Hasegawa.
// This software is released under the MIT License.

using NUnit.Framework;
using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.TestTools;

// ReSharper disable AccessToStaticMemberViaDerivedType

namespace APIExamples.NUnit
{
    /// <summary>
    /// カスタム制約の使用例
    /// </summary>
    [TestFixture]
    public class CustomConstraintExample
    {
        private static GameObject CreateDestroyedObject()
        {
            var gameObject = new GameObject("Foo");
            GameObject.DestroyImmediate(gameObject);
            return gameObject;
        }

        [Test]
        public void CustomConstraint_Constraintの実装だけで可能な書きかた()
        {
            var actual = CreateDestroyedObject();

            Assert.That(actual, new DestroyedConstraint());
            // 失敗時メッセージ例:
            //  Expected: destroyed GameObject
            //  But was:  <Foo (UnityEngine.GameObject)>
        }

        [Test]
        [UnityPlatform(RuntimePlatform.OSXEditor, RuntimePlatform.WindowsEditor, RuntimePlatform.LinuxEditor)]
        public void CustomConstraint_Extensionsの実装も行なうと可能な書きかた()
        {
            var actual = CreateDestroyedObject();

            Assert.That(actual, Is.Not.Null.And.Destroyed());
            // 失敗時メッセージ例:
            //  Expected: not null and destroyed GameObject
            //  But was:  <Foo (UnityEngine.GameObject)>
        }

        [Test]
        public void CustomConstraint_Isの実装も行なうと可能な書きかた()
        {
            var actual = CreateDestroyedObject();

            Assert.That(actual, Is.Destroyed);
            // 失敗時メッセージ例:
            //  Expected: destroyed GameObject
            //  But was:  <Foo (UnityEngine.GameObject)>
        }

        [Test]
        public void CustomConstraint_Isの実装も行なうと可能な書きかた_Not()
        {
            var actual = new GameObject("Bar");

            Assert.That(actual, Is.Not.Destroyed());
            // 失敗時メッセージ例:
            //  Expected: not destroyed GameObject
            //  But was:  <null>
        }
    }

    /// <summary>
    /// カスタム制約の実装例.
    /// このコードは、Test Helperパッケージ（com.nowsprinting.test-helper）内のコードに日本語コメントをつけたものです。
    /// </summary>
    public class DestroyedConstraint : Constraint
    {
        public DestroyedConstraint(params object[] args) : base(args)
        {
            base.Description = "destroyed GameObject"; // 失敗時メッセージに出力される
        }

        /// <summary>
        /// Assert.Thatから渡されるactualオブジェクトを検証する、カスタム制約の本体
        /// </summary>
        /// <param name="actual">検証対象オブジェクト</param>
        /// <returns>制約の成否</returns>
        public override ConstraintResult ApplyTo(object actual)
        {
            if (actual is GameObject actualGameObject)
            {
                return new ConstraintResult(this, actual, (bool)actualGameObject == false);
                // GameObjectであれば、破棄されているかを判定して結果として返す
            }

            return new ConstraintResult(this, actual, false);
            // GameObjectでなければ常にfalse
        }
    }

    /// <summary>
    /// カスタム制約のための<see cref="ConstraintExpression"/>拡張クラス.
    /// このコードは、Test Helperパッケージ（com.nowsprinting.test-helper）内のコードに日本語コメントをつけたものです。
    /// </summary>
    public static class ConstraintExtensions
    {
        /// <summary>
        /// カスタム制約のための<see cref="ConstraintExpression"/>拡張メソッド.
        /// 式にDestroyedが指定されたとき、Destroyed制約のインスタンスをexpressionに追加して返します。
        /// </summary>
        /// <param name="expression"></param>
        /// <returns>constraint to destroyed GameObject</returns>
        public static DestroyedConstraint Destroyed(this ConstraintExpression expression)
            // Note: 比較対象がある制約の場合、第2引数で object expected を受け取ります
        {
            var constraint = new DestroyedConstraint();
            expression.Append(constraint);
            return constraint;
        }
    }

    /// <summary>
    /// カスタム制約のための<see cref="Is"/>クラス.
    /// このコードは、Test Helperパッケージ（com.nowsprinting.test-helper）内のコードに日本語コメントをつけたものです。
    /// </summary>
    /// <remarks>
    /// Unity Test Framework には NUnit.Framework.Is を継承した UnityEngine.TestTools.Constraints.Is がすでにあるため、これを継承して作ります。
    /// Test Helperパッケージも使用するのであれば、TestHelper.Constraints.Is を継承します。
    /// UnityEngine.TestTools.Constraints.Is も TestHelper.Constraints.Is も必要なければ、直接 global::NUnit.Framework.Is を継承しても構いません。
    /// </remarks>
    // ReSharper disable once ClassNeverInstantiated.Global
    public class Is : UnityEngine.TestTools.Constraints.Is
    {
        /// <summary>
        /// Create constraint to destroyed GameObject.
        /// </summary>
        public static DestroyedConstraint Destroyed => new DestroyedConstraint();
        // Note: 比較対象がある制約の場合、引数で object expected を受け取ります
    }
}
