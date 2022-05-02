// Copyright (c) 2021 Koji Hasegawa.
// This software is released under the MIT License.

using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace APIExamples.NUnit
{
    /// <summary>
    /// <see cref="TimeoutAttribute"/>およびデフォルトタイムアウト時間の確認
    /// </summary>
    [TestFixture]
    public class TimeoutAttributeExample
    {
        private const bool Fail = false; // このフラグをtrueにするとこのクラスのテストはすべて失敗します

        [TearDown]
        public void TearDown()
        {
            Time.timeScale = 1f;
        }

        [UnityTest, UnityPlatform(exclude = new[] { RuntimePlatform.WebGLPlayer })]
        [Explicit("時間がかかるのでExplicit")]
        public IEnumerator タイムアウトのデフォルトは3分()
        {
            var waitSeconds = 3 * 60; // タイムアウトのデフォルトは3[min]
            if (!Fail)
            {
                waitSeconds--;
            }

            yield return new WaitForSeconds(waitSeconds);
        }

        [UnityTest, UnityPlatform(exclude = new[] { RuntimePlatform.WebGLPlayer })]
        [Timeout(1000)]
        public IEnumerator タイムアウトを1秒に設定()
        {
            var waitSeconds = 1f;
            if (!Fail)
            {
                waitSeconds -= 0.2f;
            }

            yield return new WaitForSeconds(waitSeconds);
        }

        [UnityTest, UnityPlatform(exclude = new[] { RuntimePlatform.WebGLPlayer })]
        [Timeout(1000)]
        public IEnumerator タイムアウト値はTimeScaleに影響されない()
        {
            var waitSeconds = 2f;
            if (!Fail)
            {
                waitSeconds -= 0.4f;
            }

            Time.timeScale = 2f;
            yield return new WaitForSeconds(waitSeconds);
        }
    }
}
