// Copyright (c) 2021-2025 Koji Hasegawa.
// This software is released under the MIT License.

using System;
using System.Collections;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using TestHelper.Attributes;
using UnityEngine;
using UnityEngine.TestTools;

namespace APIExamples.NUnit
{
    /// <summary>
    /// <see cref="TimeoutAttribute"/>およびデフォルトタイムアウト時間の確認
    /// </summary>
    /// <remarks>
    /// このクラスに書かれている振る舞いは、Unity Test Framework v1.4.6およびv1.5.1のものです。バージョンによって振る舞いが異なりますので注意してください。
    /// <br/>
    /// 非同期 (async) テストでタイムアウト及びTimeout属性が機能しない問題は、Unity Test Framework v1.3.4で修正されました。
    /// <see href="https://unity3d.atlassian.net/servicedesk/customer/portal/2/IN-28108"/>
    /// </remarks>
    [TestFixture]
    [Explicit("Timeout属性によって失敗するテストの例")]
    public class TimeoutAttributeExample
    {
        [Test]
        [Timeout(200)]
        public void Timeout属性_同期テストではタイムアウトで割り込みは発生しないが終了時に指定時間を超えていたらテスト失敗()
        {
            var endTime = DateTime.Now.AddSeconds(0.5d);
            while (DateTime.Now < endTime)
            {
            }
        }

        [UnityTest]
        [Timeout(200)]
        public IEnumerator Timeout属性_UnityTest属性でWaitForSeconds_割り込みは発生しないが終了時に指定時間を超えていたらテスト失敗()
        {
            yield return new WaitForSeconds(0.5f);
        }

        [UnityTest]
        public IEnumerator タイムアウトのデフォルトは3分_3分でタイムアウト()
        {
            var endTime = DateTime.Now.AddMinutes(3d).AddSeconds(1d);
            while (DateTime.Now < endTime)
            {
                yield return null;
            }
        }

        [UnityTest]
        [Timeout(200)]
        public IEnumerator Timeout属性_UnityTest属性でYieldReturnNull_指定ミリ秒でタイムアウト()
        {
            var endTime = DateTime.Now.AddSeconds(0.5d);
            while (DateTime.Now < endTime)
            {
                yield return null;
            }
        }

        [UnityTest]
        [Timeout(1000)]
        [TimeScale(10.0f)]
        public IEnumerator Timeout属性_タイムアウト値はTimeScaleに影響されない_指定ミリ秒でタイムアウト()
        {
            var endTime = Time.realtimeSinceStartup + 2.0f;
            while (Time.realtimeSinceStartup < endTime)
            {
                yield return null;
            }
        }

        [Test]
        [Timeout(200)]
        public async Task Timeout属性_非同期テストでDelay_指定ミリ秒でタイムアウト()
        {
            await Task.Delay(TimeSpan.FromSeconds(0.5f));
        }

        [Test]
        [Timeout(200)]
        public async Task Timeout属性_非同期テストでYield_指定ミリ秒でタイムアウト()
        {
            var endTime = DateTime.Now.AddSeconds(0.5d);
            while (DateTime.Now < endTime)
            {
                await Task.Yield();
            }
        }

        [Test]
        [Timeout(200)]
        public async Task Timeout属性_非同期テストでNextFrame_指定ミリ秒でタイムアウト()
        {
            var endTime = DateTime.Now.AddSeconds(0.5d);
            while (DateTime.Now < endTime)
            {
                await UniTask.NextFrame();
            }
        }
    }
}
