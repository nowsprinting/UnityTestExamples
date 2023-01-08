// Copyright (c) 2021-2023 Koji Hasegawa.
// This software is released under the MIT License.

using System;
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
    /// <see cref="TimeoutAttribute"/>およびデフォルトタイムアウト時間の確認
    /// </summary>
    [TestFixture]
    public class TimeoutAttributeExample
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

        [UnityTest]
        [Explicit("時間がかかるのでExplicit")]
        public IEnumerator タイムアウトのデフォルトは3分()
        {
            var waitSeconds = 3 * 60 - 1; // タイムアウトのデフォルトは3[min]
            if (Fail)
            {
                waitSeconds++;
            }

            yield return new WaitForSeconds(waitSeconds);
        }

        [UnityTest]
        [Timeout(2000)]
        public IEnumerator タイムアウトを1秒に設定()
        {
            var waitSeconds = 1f;
            if (Fail)
            {
                waitSeconds = 10f;
            }

            yield return new WaitForSeconds(waitSeconds);
        }

        [UnityTest]
        [Timeout(2000)]
        public IEnumerator タイムアウト値はTimeScaleに影響されない()
        {
            var waitSeconds = 1f;
            if (Fail)
            {
                waitSeconds = 10f;
            }

            Time.timeScale = 5f;
            yield return new WaitForSecondsRealtime(waitSeconds);
        }

        [UnityTest]
        [Timeout(2000)]
        public IEnumerator タイムアウトはWaitForSeconds以外では有効でない_中断されない()
        {
            var waitSeconds = 1f;
            if (Fail)
            {
                waitSeconds = 10f;
            }

            var startTime = DateTime.Now;
            while ((DateTime.Now - startTime).Seconds < waitSeconds)
            {
                yield return null; // Timeout時間を超えてもテストは中断されない。指定時間を超過していれば失敗と判定はされる
            }
        }

        [UnityPlatform(exclude = new[] { RuntimePlatform.WebGLPlayer })]
        // WebGLでTask.Delayが終了しない https://unity3d.atlassian.net/servicedesk/customer/portal/2/IN-28109
        [Test]
        [Timeout(2000)]
        public async Task タイムアウトはasyncテストでは有効でない_中断されず失敗にもならない()
        {
            await Task.Delay(3000);
            // Timeout時間を超えてもテストは中断されず、失敗にもならない https://unity3d.atlassian.net/servicedesk/customer/portal/2/IN-28108
        }
    }
}
