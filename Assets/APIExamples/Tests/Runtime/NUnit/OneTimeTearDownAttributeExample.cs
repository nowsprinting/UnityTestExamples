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
    /// <see cref="OneTimeTearDownAttribute"/>の使用例
    /// </summary>
    /// <seealso cref="TearDownAttributeExample"/>
    /// <seealso cref="AsyncTearDownAttributeExample"/>
    /// <seealso cref="APIExamples.UnityTestFramework.UnityTearDownAttributeExample"/>
    [TestFixture]
    [SuppressMessage("ReSharper", "AccessToStaticMemberViaDerivedType")]
    public class OneTimeTearDownAttributeExample
    {
        private int _oneTimeTeardownCount;
        private int _teardownCount;

        /// <summary>
        /// クラス内の最後のテストの実行後に一度だけ実行されます
        /// </summary>
        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Debug.Log($"OneTimeTearDown");
            _oneTimeTeardownCount++;

            Assert.That(_oneTimeTeardownCount, Is.EqualTo(1), "OneTimeTearDownはTestFixtureごとに一度だけ実行される");
            Assert.That(_teardownCount, Is.EqualTo(3), "3つのテストがあるのでTearDownは3回実行されている");
        }

        /// <summary>
        /// 各テストメソッドの後に実行されます
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            Debug.Log($"TearDown");
            _teardownCount++;
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
