// Copyright (c) 2021 Koji Hasegawa.
// This software is released under the MIT License.

using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools.Utils;

namespace APIExamples.UnityTestFramework
{
    /// <summary>
    /// Unity固有の型に対応したEqualityComparerで数値を丸めて検証する例
    /// EqualityComparerは、Unity 2018.1で追加された
    /// </summary>
    public class EqualityComparerExample
    {
        [Test]
        public void ColorEqualityComparer使用例_だいたい合ってるのでヨシ()
        {
            var actual = new Color(0.23456f, 0f, 0f, 0.8765f);
            var expected = new Color(0.2f, 0f, 0f, 0.8f);
            var comparer = new ColorEqualityComparer(0.1f);

            Assert.That(actual, Is.EqualTo(expected).Using(comparer));
        }

        [Test]
        public void FloatEqualityComparer使用例_だいたい合ってるのでヨシ()
        {
            var actual = 128.5674329f;
            var expected = 128.5f;
            var comparer = new FloatEqualityComparer(0.1f);

            Assert.That(actual, Is.EqualTo(expected).Using(comparer));
        }

        [Test]
        public void FloatEqualityComparerInstance使用例_だいたい合ってるのでヨシ()
        {
            var actual = 128.5674329f;
            var expected = 128.56f;
            var comparer = FloatEqualityComparer.Instance; // 許容相対誤差 0.0001f

            Assert.That(actual, Is.EqualTo(expected).Using(comparer));
        }

        [Test]
        public void QuaternionEqualityComparer使用例_だいたい合ってるのでヨシ()
        {
            var actual = new Quaternion(10f, 0f, 0f, 0f);
            var expected = new Quaternion(10.7f, 0f, 0f, 0f);
            var comparer = new QuaternionEqualityComparer(0.1f);

            Assert.That(actual, Is.EqualTo(expected).Using(comparer));
        }

        [Test]
        public void Vector2EqualityComparer使用例_だいたい合ってるのでヨシ()
        {
            var actual = new Vector2(10f, 0f);
            var expected = new Vector2(10.7f, 0f);
            var comparer = new Vector2EqualityComparer(0.1f);

            Assert.That(actual, Is.EqualTo(expected).Using(comparer));
        }

        [Test]
        public void Vector2ComparerWithEqualsOperator使用例_等価オペレーターで比較される()
        {
            var actual = new Vector2(10f, 0f);
            var expected = new Vector2(10.0f, 0f);

            Assert.That(actual, Is.EqualTo(expected).Using(Vector2ComparerWithEqualsOperator.Instance));
            // Note: actual == expected で比較
        }

        [Test]
        public void Vector3EqualityComparer使用例_だいたい合ってるのでヨシ()
        {
            var actual = new Vector3(10f, 0f, 0f);
            var expected = new Vector3(10.7f, 0f, 0f);
            var comparer = new Vector3EqualityComparer(0.1f);

            Assert.That(actual, Is.EqualTo(expected).Using(comparer));
        }

        [Test]
        public void Vector3ComparerWithEqualsOperator使用例_等価オペレーターで比較される()
        {
            var actual = new Vector3(10f, 0f, 0f);
            var expected = new Vector3(10.0f, 0f, 0f);

            Assert.That(actual, Is.EqualTo(expected).Using(Vector3ComparerWithEqualsOperator.Instance));
            // Note: actual == expected で比較
        }

        [Test]
        public void Vector4EqualityComparer使用例_だいたい合ってるのでヨシ()
        {
            var actual = new Vector4(10f, 0f, 0f, 0f);
            var expected = new Vector4(10.7f, 0f, 0f, 0f);
            var comparer = new Vector4EqualityComparer(0.1f);

            Assert.That(actual, Is.EqualTo(expected).Using(comparer));
        }

        [Test]
        public void Vector4ComparerWithEqualsOperator使用例_等価オペレーターで比較される()
        {
            var actual = new Vector4(10f, 0f, 0f, 0f);
            var expected = new Vector4(10.0f, 0f, 0f, 0f);

            Assert.That(actual, Is.EqualTo(expected).Using(Vector4ComparerWithEqualsOperator.Instance));
            // Note: actual == expected で比較
        }
    }
}
