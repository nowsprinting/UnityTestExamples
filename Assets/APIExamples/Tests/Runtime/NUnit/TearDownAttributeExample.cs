// Copyright (c) 2021-2025 Koji Hasegawa.
// This software is released under the MIT License.

using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace APIExamples.NUnit
{
    /// <summary>
    /// <see cref="TearDownAttribute"/>の使用例
    /// </summary>
    /// <seealso cref="OneTimeTearDownAttributeExample"/>
    /// <seealso cref="AsyncTearDownAttributeExample"/>
    /// <seealso cref="APIExamples.UnityTestFramework.UnityTearDownAttributeExample"/>
    [TestFixture]
    [SuppressMessage("ReSharper", "AccessToStaticMemberViaDerivedType")]
    public class TearDownAttributeExample
    {
        /// <summary>
        /// 各テストメソッドの後に実行されます
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            Debug.Log($"TearDown");
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
