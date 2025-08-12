// Copyright (c) 2021-2025 Koji Hasegawa.
// This software is released under the MIT License.

using System.Collections;
using System.Threading.Tasks;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

// ReSharper disable AccessToStaticMemberViaDerivedType

namespace APIExamples.NUnit
{
    /// <summary>
    /// <see cref="RetryAttribute"/>の使用例
    /// </summary>
    /// <remarks>
    /// Unity Test Frameworkの<see href="https://docs.unity3d.com/Packages/com.unity.test-framework@1.1/manual/index.html#known-limitations">Known limitations</see>に
    ///  "When using the NUnit Retry attribute in PlayMode tests, it throws InvalidCastException."
    /// とありますが、v1.1.27で修正されました。
    /// <br/>
    /// <see cref="UnityTestAttribute"/> および非同期テストと使用できない問題は、Unity Test Framework v1.4.5で修正されました。
    /// <see href="https://issuetracker.unity3d.com/issues/timeout-attribute-is-not-working-when-used-with-the-async-test-in-the-test-runner"/>
    /// </remarks>
    [TestFixture]
    public class RetryAttributeExample
    {
        private int _testCount;
        private int _unityTestCount;
        private int _asyncTestCount;

        [Test]
        [Retry(2)] // リトライ回数ではなく総試行回数を指定
        public void Retry属性で最初は失敗するが2回目で成功するテスト()
        {
            Debug.Log($"{TestContext.CurrentContext.Test.Name} {++_testCount}回目");

            Assert.That(_testCount, Is.EqualTo(2));
        }

        [UnityTest]
        [Retry(2)] // リトライ回数ではなく総試行回数を指定
        public IEnumerator Retry属性で最初は失敗するが2回目で成功するテスト_UnityTest属性()
        {
            Debug.Log($"{TestContext.CurrentContext.Test.Name} {++_unityTestCount}回目");
            yield return null;

            Assert.That(_unityTestCount, Is.EqualTo(2));
        }

        [Test]
        [Retry(2)]
        public async Task Retry属性で最初は失敗するが2回目で成功するテスト_非同期テスト()
        {
            Debug.Log($"{TestContext.CurrentContext.Test.Name} {++_asyncTestCount}回目");
            await Task.Yield();

            Assert.That(_asyncTestCount, Is.EqualTo(2));
        }
    }
}
