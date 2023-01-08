// Copyright (c) 2021-2023 Koji Hasegawa.
// This software is released under the MIT License.

using System.Collections;
using System.Threading.Tasks;
using NUnit.Framework;
using UnityEngine.TestTools;

// ReSharper disable AccessToStaticMemberViaDerivedType

namespace APIExamples.NUnit
{
    /// <summary>
    /// <see cref="RetryAttribute"/>の使用例
    /// </summary>
    /// <remarks>
    /// Unity Test Frameworkの<see href="https://docs.unity3d.com/Packages/com.unity.test-framework@1.1/manual/index.html#known-limitations">Known limitations</see>に
    ///     When using the NUnit Retry attribute in PlayMode tests, it throws InvalidCastException.
    /// とありますが、v1.1.27で修正されました。
    /// </remarks>
    [TestFixture]
    public class RetryAttributeExample
    {
        private int _testCount;
        private int _unityTestCount;
        private int _asyncTestCount;

        [Test]
        [Retry(2)] // リトライ回数ではなく総試行回数を指定
        public void 最初は失敗するが2回目で成功するテスト()
        {
            Assert.That(++_testCount, Is.EqualTo(2));
        }

        [UnityTest]
        [Retry(2)] // リトライ回数ではなく総試行回数を指定
        public IEnumerator 最初は失敗するが2回目で成功するテスト_UnityTest属性()
        {
            yield return null;

            Assert.That(++_unityTestCount, Is.EqualTo(2));
        }

        [Explicit("Retry属性はasyncテストに使用できない（Unity Test Framework v1.3.2時点）")]
        // https://unity3d.atlassian.net/servicedesk/customer/portal/2/IN-28107
        [Test]
        [Retry(2)] // リトライ回数ではなく総試行回数を指定
        public async Task 最初は失敗するが2回目で成功するテスト_非同期()
        {
            await Task.Delay(0);

            Assert.That(++_asyncTestCount, Is.EqualTo(2));
        }
    }
}
