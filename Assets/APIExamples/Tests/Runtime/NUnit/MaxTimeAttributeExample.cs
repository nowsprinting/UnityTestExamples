// Copyright (c) 2021-2025 Koji Hasegawa.
// This software is released under the MIT License.

using System;
using System.Collections;
using System.Threading.Tasks;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace APIExamples.NUnit
{
    /// <summary>
    /// <see cref="MaxTimeAttribute"/>の使用例
    /// </summary>
    /// <remarks>
    /// <see cref="UnityTestAttribute"/> および非同期テストと使用できない問題は、Unity Test Framework v1.4.5で修正されました。
    /// <see href="https://issuetracker.unity3d.com/issues/timeout-attribute-is-not-working-when-used-with-the-async-test-in-the-test-runner"/>
    /// </remarks>
    [TestFixture]
    [Explicit("MaxTime属性によって失敗するテストの例")]
    public class MaxTimeAttributeExample
    {
        [Test]
        [MaxTime(100)]
        public void MaxTime属性_指定ミリ秒よりも時間のかかるテストは実行後に失敗()
        {
            var endTime = DateTime.Now.AddMilliseconds(200);
            while (DateTime.Now < endTime)
            {
            }
        }

        [UnityTest]
        [MaxTime(100)]
        public IEnumerator MaxTime属性_指定ミリ秒よりも時間のかかるテストは実行後に失敗_UnityTest属性()
        {
            yield return new WaitForSeconds(0.2f);
        }

        [Test]
        [MaxTime(100)]
        public async Task MaxTime属性_指定ミリ秒よりも時間のかかるテストは実行後に失敗_非同期テスト()
        {
            await Task.Delay(200);
        }
    }
}
