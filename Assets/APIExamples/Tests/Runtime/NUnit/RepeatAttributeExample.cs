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
    /// <see cref="RepeatAttribute"/>の使用例
    /// </summary>
    /// <remarks>
    /// Unity Test Frameworkの<see href="https://docs.unity3d.com/Packages/com.unity.test-framework@1.1/manual/index.html#known-limitations">Known limitations</see>に
    /// "The UnityTest attribute does not support the NUnit Repeat attribute."
    /// とありますが、v1.1.27で修正されました。
    /// <br/>
    /// <see cref="UnityTestAttribute"/> および非同期テストと使用できない問題は、Unity Test Framework v1.4.5で修正されました。
    /// <see href="https://issuetracker.unity3d.com/issues/timeout-attribute-is-not-working-when-used-with-the-async-test-in-the-test-runner"/>
    /// </remarks>
    public class RepeatAttributeExample
    {
        private int _testCount;
        private int _unityTestCount;
        private int _asyncTestCount;

        [Test]
        [Repeat(2)]
        public void Repeat属性で繰り返し実行するテスト()
        {
            Debug.Log($"{TestContext.CurrentContext.Test.Name} {++_testCount}回目");
        }

        [UnityTest]
        [Repeat(2)]
        public IEnumerator Repeat属性で繰り返し実行するテスト_UnityTest属性()
        {
            Debug.Log($"{TestContext.CurrentContext.Test.Name} {++_unityTestCount}回目");
            yield return null;
        }

        [Test]
        [Repeat(2)]
        public async Task Repeat属性で繰り返し実行するテスト_非同期テスト()
        {
            Debug.Log($"{TestContext.CurrentContext.Test.Name} {++_asyncTestCount}回目");
            await Task.Yield();
        }
    }
}
