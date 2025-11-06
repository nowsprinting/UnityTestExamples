// Copyright (c) 2021-2025 Koji Hasegawa.
// This software is released under the MIT License.

using System.Collections;
using System.Threading.Tasks;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace APIExamples.Editor.UnityTestFramework
{
    /// <summary>
    /// <see cref="UnitySetUpAttribute"/>の使用例
    /// </summary>
    /// <seealso cref="APIExamples.Editor.NUnit.AsyncSetupAttributeExample"/>
    [TestFixture]
    public class UnitySetUpAttributeExample
    {
        private int _setupCount;

        /// <summary>
        /// SetUpをコルーチン書式で記述できます
        /// <see cref="UnityEngine.TestTools.UnityTestAttribute"/>専用ではなく、通常のTest向けのSetUpとしても使用できます
        /// </summary>
        [UnitySetUp]
        public IEnumerator SetUpCoroutine()
        {
            yield return null;
            _setupCount++;
        }

        [Test, Order(0)]
        public void TestMethod()
        {
            Assert.That(_setupCount, Is.EqualTo(1), "最初のテストなのでSetUpは1回実行されている");
        }

        [Test, Order(1)]
        public async Task TestMethodAsync()
        {
            await Task.Yield();
            Assert.That(_setupCount, Is.EqualTo(2), "2番目のテストなのでSetUpは2回実行されている");
        }

        [UnityTest, Order(2)]
        public IEnumerator UnityTestMethod()
        {
            yield return null;
            Assert.That(_setupCount, Is.EqualTo(3), "3番目のテストなのでSetUpは3回実行されている");
        }
    }
}
