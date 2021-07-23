// Copyright (c) 2021 Koji Hasegawa.
// This software is released under the MIT License.

using System;
using System.Collections.Generic;
using NUnit.Framework;
using NUnit.Framework.Constraints;

// ReSharper disable AccessToStaticMemberViaDerivedType

namespace APIExamples.NUnit
{
    /// <summary>
    /// <see cref="EqualConstraint"/> + Using修飾子と、カスタム<see cref="IComparer{T}"/>の使用例
    /// </summary>
    public class CustomComparerExample
    {
        [Test]
        public void EqualConstraint_Using修飾子で比較()
        {
            var actual = new CompositeKeySUT("Foo", "Bar", 1);

            Assert.That(actual, Is.EqualTo(new CompositeKeySUT("Foo", "Bar", 10)).Using(new CompositeKeySUTComparer()));
            // 失敗時メッセージ例:
            //  Expected: <APIExamples.NUnit.CompositeKeySUT>
            //  But was:  <APIExamples.NUnit.CompositeKeySUT>
        }

        [Test]
        public void EqualConstraint_コレクションの要素をUsing修飾子で比較()
        {
            var actual = new[] { new CompositeKeySUT("Foo", "Bar", 1), new CompositeKeySUT("Bar", "Baz", 2) };

            Assert.That(actual,
                Is.EqualTo(new[] { new CompositeKeySUT("Foo", "Bar", 10), new CompositeKeySUT("Bar", "Baz", 20) })
                    .Using(new CompositeKeySUTComparer()));
            // 失敗時メッセージ例:
            //  Expected and actual are both <APIExamples.NUnit.CompositeKeySUT[2]>
            //  Values differ at index [1]
            //  Expected: <APIExamples.NUnit.CompositeKeySUT>
            //  But was:  <APIExamples.NUnit.CompositeKeySUT>
        }
    }

    /// <summary>
    /// カスタム<see cref="IComparer{T}"/>の実装例
    /// </summary>
    public class CompositeKeySUTComparer : IComparer<CompositeKeySUT>
    {
        /// <summary>
        /// Key1 + Key2 で比較
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare(CompositeKeySUT x, CompositeKeySUT y)
        {
            var key1Comparison = string.Compare(x.Key1, y.Key1, StringComparison.Ordinal);
            if (key1Comparison != 0)
            {
                return key1Comparison;
            }

            return string.Compare(x.Key2, y.Key2, StringComparison.Ordinal);
        }
    }
}
