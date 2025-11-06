// Copyright (c) 2021-2025 Koji Hasegawa.
// This software is released under the MIT License.

#if ENABLE_UTF_1_5
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using APIExamples.NUnit;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Is = APIExamples.NUnit.Is;

namespace APIExamples.Editor.UnityTestFramework
{
    /// <summary>
    /// <see cref="UnityOneTimeTearDownAttributeExample"/>の使用例
    /// </summary>
    /// <remarks>
    /// Required: Unity Test Framework v1.5 or later
    /// </remarks>
    /// <seealso cref="AsyncTearDownAttributeExample"/>
    /// <seealso cref="UnityTearDownAttributeExample"/>
    [TestFixture]
    [SuppressMessage("ReSharper", "AccessToStaticMemberViaDerivedType")]
    public class UnityOneTimeTearDownAttributeExample
    {
        private int _oneTimeTeardownCount;
        private int _teardownCount;

        /// <summary>
        /// クラス内の最後のテストの実行後に一度だけ実行されます
        /// </summary>
        [UnityOneTimeTearDown]
        public IEnumerator OneTimeTearDown()
        {
            yield return null;
            Debug.Log($"UnityOneTimeTearDown");
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
#endif
