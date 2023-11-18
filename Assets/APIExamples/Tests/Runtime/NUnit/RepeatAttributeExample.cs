// Copyright (c) 2021-2023 Koji Hasegawa.
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
    ///     The UnityTest attribute does not support the NUnit Repeat attribute.
    /// とありますが、v1.1.27で修正されました。
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
            Debug.Log($"{nameof(Repeat属性で繰り返し実行するテスト)} {++_testCount}回目");
            Assert.That(true);
        }

        [UnityTest]
        [Repeat(2)]
        public IEnumerator Repeat属性で繰り返し実行するテスト_UnityTest属性()
        {
            Debug.Log($"{nameof(Repeat属性で繰り返し実行するテスト_UnityTest属性)} {++_unityTestCount}回目");
            yield return null;
            Assert.That(true);
        }

        [Ignore("Repeat属性はasyncテストに使用できない（Unity Test Framework v1.4.0時点）")]
        // See: https://unity3d.atlassian.net/servicedesk/customer/portal/2/IN-28107
        // Note: Unity Test Framework v1.3.5 で追加された `-repeat` コマンドラインオプションは、asyncテストにも有効です
        [Test]
        [Repeat(2)]
        public async Task Repeat属性はasyncテストに使用できない_テストが終了しない()
        {
            Debug.Log($"{nameof(Repeat属性はasyncテストに使用できない_テストが終了しない)} {++_asyncTestCount}回目");
            await Task.Yield();
            Assert.That(true);
        }
    }
}
