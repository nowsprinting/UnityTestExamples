// Copyright (c) 2021-2025 Koji Hasegawa.
// This software is released under the MIT License.

using System.Collections;
using System.Threading.Tasks;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace APIExamples.NUnit
{
    /// <summary>
    /// 非同期の<see cref="TearDownAttribute"/>の使用例
    /// <see cref="OneTimeTearDownAttribute"/>はasyncサポートされていない（UTF v1.6.0時点）
    /// <p/>
    /// Async TearDown attribute example
    /// Async OneTimeTearDown attribute is not yet supported in UTF v1.6.0
    /// </summary>
    /// <remarks>
    /// Required: Unity Test Framework v1.3 or later
    /// </remarks>
    /// <seealso cref="TearDownAttributeExample"/>
    /// <seealso cref="OneTimeTearDownAttributeExample"/>
    /// <seealso cref="APIExamples.UnityTestFramework.UnityTearDownAttributeExample"/>
    [TestFixture]
    public class AsyncTearDownAttributeExample
    {
        /// <summary>
        /// 各テストメソッドの後に実行されます
        /// </summary>
        [TearDown]
        public async Task TearDownAsync()
        {
            await Task.Yield();
            Debug.Log($"TearDownAsync");
        }

        [Test]
        public void TestMethod()
        {
            Debug.Log($"TestMethod");
        }

        [Test]
        public async Task TestMethodAsync()
        {
            await Task.Yield();
            Debug.Log($"TestMethodAsync");
        }

        [UnityTest]
        public IEnumerator UnityTestMethod()
        {
            yield return null;
            Debug.Log($"UnityTestMethod");
        }
    }
}
