// Copyright (c) 2021-2023 Koji Hasegawa.
// This software is released under the MIT License.

using System.Collections;
using System.Diagnostics;
using System.Threading.Tasks;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

#pragma warning disable CS0162

namespace APIExamples.NUnit
{
    /// <summary>
    /// <see cref="MaxTimeAttribute"/>の使用例
    /// </summary>
    [TestFixture]
    public class MaxTimeAttributeExample
    {
        private readonly Stopwatch _stopwatch = new Stopwatch();
        private const bool Fail = false; // このフラグをtrueにするとこのクラスのテストはすべて失敗します

        [SetUp]
        public void SetUp()
        {
            _stopwatch.Restart();
        }

        [TearDown]
        public void TearDown()
        {
            UnityEngine.Debug.Log($"{_stopwatch.ElapsedMilliseconds} [ms]");
            Time.timeScale = 1f;
        }

        [Test]
        [MaxTime(10)]
        public void MaxTimeを10ミリ秒に設定()
        {
            var loopCount = 1;
            if (Fail)
            {
                loopCount = 10000000;
            }

            var sum = 0f;
            for (var i = 0; i < loopCount; i++)
            {
                sum += Random.value;
            }
        }

        [Explicit("MaxTime属性はUnityTest属性のテストに使用できない（Unity Test Framework v1.3.2時点）")]
        [UnityTest]
        [MaxTime(2000)]
        public IEnumerator MaxTime属性はUnityTest属性のテストに使用できない_実行するとエラー()
        {
            var waitSeconds = 1f;
            if (Fail)
            {
                waitSeconds = 10f;
            }

            yield return new WaitForSeconds(waitSeconds);
        }

        [Explicit("MaxTime属性はasyncテストに使用できない（Unity Test Framework v1.3.2時点）")]
        // https://unity3d.atlassian.net/servicedesk/customer/portal/2/IN-28107
        [UnityPlatform(exclude = new[] { RuntimePlatform.WebGLPlayer })]
        // WebGLでTask.Delayが終了しない https://unity3d.atlassian.net/servicedesk/customer/portal/2/IN-28109
        [Test]
        [MaxTime(2000)]
        public async Task MaxTime属性は非同期テストに使用できない_実行すると無限ループ()
        {
            await Task.Delay(3000);
        }
    }
}
