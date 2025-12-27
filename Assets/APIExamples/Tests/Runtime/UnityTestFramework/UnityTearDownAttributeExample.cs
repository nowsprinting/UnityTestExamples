// Copyright (c) 2021-2025 Koji Hasegawa.
// This software is released under the MIT License.

using System.Collections;
using System.Threading.Tasks;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace APIExamples.UnityTestFramework
{
    /// <summary>
    /// <see cref="UnityTearDownAttribute"/>の使用例
    /// </summary>
    /// <seealso cref="APIExamples.NUnit.TearDownAttributeExample"/>
    /// <seealso cref="APIExamples.NUnit.OneTimeTearDownAttributeExample"/>
    /// <seealso cref="APIExamples.NUnit.AsyncTearDownAttributeExample"/>
    [TestFixture]
    public class UnityTearDownAttributeExample
    {
        /// <summary>
        /// TearDownをコルーチン書式で記述できます
        /// <see cref="UnityEngine.TestTools.UnityTestAttribute"/>専用ではなく、通常のTest向けのTearDownとしても使用できます
        /// </summary>
        [UnityTearDown]
        public IEnumerator TearDownCoroutine()
        {
            yield return null;
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
