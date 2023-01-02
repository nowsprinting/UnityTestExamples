// Copyright (c) 2021-2023 Koji Hasegawa.
// This software is released under the MIT License.

using NUnit.Framework;
using UnityEngine;

namespace APIExamples.NUnit
{
    /// <summary>
    /// <see cref="TearDownAttribute"/>, <see cref="OneTimeTearDownAttribute"/>の例
    /// </summary>
    [TestFixture]
    public class TearDownExample
    {
        /// <summary>
        /// クラス内の最後のテストの実行後に一度だけ実行されます
        /// </summary>
        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Debug.Log($"OneTimeTearDown, {Time.time}");
        }

        /// <summary>
        /// 各テストメソッドの後に実行されます
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
