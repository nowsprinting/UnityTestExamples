// Copyright (c) 2021-2023 Koji Hasegawa.
// This software is released under the MIT License.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using UnityEngine;

// ReSharper disable AccessToStaticMemberViaDerivedType

namespace APIExamples.NUnit
{
    /// <summary>
    /// Using修飾子と、カスタム<see cref="IComparer{T}"/>の使用例
    /// </summary>
    public class CustomComparerExample
    {
        [Test]
        public void EqualConstraint_Using修飾子でカスタムComparerを指定して比較()
        {
            var actual = new GameObject("test object");
            var expected = new GameObject("test object");

            Assert.That(actual, Is.EqualTo(expected).Using(new GameObjectNameComparer()));
            // 失敗時メッセージ例:
            //  Expected: <test object (UnityEngine.GameObject)>
            //  But was:  <test (UnityEngine.GameObject)>
        }

        [Test]
        public void CollectionContainsConstraint_コレクションの要素をUsing修飾子でカスタムComparerを指定して比較()
        {
            var actual = new[] { new GameObject("test1"), new GameObject("test2"), new GameObject("test3"), };
            var expected = new GameObject("test3");

            Assert.That(actual, Does.Contain(expected).Using(new GameObjectNameComparer()));
            // 失敗時メッセージ例:
            //  Expected: collection containing <test4 (UnityEngine.GameObject)>
            //  But was:  < <test1 (UnityEngine.GameObject)>, <test2 (UnityEngine.GameObject)>, <test3 (UnityEngine.GameObject)> >
        }
    }

    /// <summary>
    /// カスタム<see cref="IComparer{T}"/>の実装例.
    /// このコードは、Test Helperパッケージ（com.nowsprinting.test-helper）内のコードに日本語コメントをつけたものです。
    /// </summary>
    public class GameObjectNameComparer : IComparer<GameObject>
    {
        /// <summary>
        /// <c>GameObject</c> 同士を、参照でなく <c>name</c> プロパティで比較するカスタムComparer
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        public int Compare(GameObject x, GameObject y)
        {
            return string.Compare(x.name, y.name, StringComparison.Ordinal);
        }
    }
}
