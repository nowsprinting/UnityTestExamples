﻿// Copyright (c) 2021-2023 Koji Hasegawa.
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
        public void 繰り返し実行するテスト()
        {
            Debug.Log($"{nameof(RepeatAttributeExample)}.{nameof(繰り返し実行するテスト)} {++_testCount}回目");

            Assert.That(true);
        }

        [UnityTest]
        [Repeat(2)]
        public IEnumerator 繰り返し実行するテスト_UnityTest属性()
        {
            Debug.Log($"{nameof(RepeatAttributeExample)}.{nameof(繰り返し実行するテスト_UnityTest属性)} {++_unityTestCount}回目");
            yield return null;

            Assert.That(true);
        }

        [Explicit("Repeat属性はasyncテストに使用できない（Unity Test Framework v1.3時点）")]
        [Test]
        [Repeat(2)]
        public async Task 繰り返し実行するテスト_非同期()
        {
            Debug.Log($"{nameof(RepeatAttributeExample)}.{nameof(繰り返し実行するテスト_非同期)} {++_asyncTestCount}回目");
            await Task.Delay(0);

            Assert.That(true);
        }
    }
}
