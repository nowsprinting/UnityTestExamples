// Copyright (c) 2021 Koji Hasegawa.
// This software is released under the MIT License.

using NUnit.Framework;
using UnityEngine;

namespace APIExamples.NUnit
{
    /// <summary>
    /// <see cref="SetUpAttribute"/>, <see cref="TearDownAttribute"/>, <see cref="OneTimeSetUpAttribute"/>, <see cref="OneTimeTearDownAttribute"/>の例
    /// </summary>
    [TestFixture]
    public class SetupAttributeExample
    {
        /// <summary>
        /// クラス内の最初のテストの実行前に一度だけ実行されます
        /// </summary>
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Debug.Log($"OneTimeSetUp, {Time.time}");
        }

        /// <summary>
        /// クラス内の最後のテストの実行後に一度だけ実行されます
        /// </summary>
        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Debug.Log($"OneTimeTearDown, {Time.time}");
        }

        /// <summary>
        /// 各テストメソッドの前に実行されます
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            Debug.Log($"SetUp, {Time.time}");
        }

        /// <summary>
        /// 各テストメソッドの前に実行されます
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            Debug.Log($"TearDown, {Time.time}");
        }

        [Test]
        public void TestMethod()
        {
            Debug.Log($"TestMethod, {Time.time}");
        }

        [Test]
        public void TestMethod2()
        {
            Debug.Log($"TestMethod2, {Time.time}");
        }
    }
}
