// Copyright (c) 2021-2022 Koji Hasegawa.
// This software is released under the MIT License.

using System.Collections;
using System.Diagnostics;
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
    [Explicit("Unity Test Framework v1.1.33時点ではMaxTime属性はUnityTest属性のテストに使用できない")]
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

        [UnityTest]
        [MaxTime(2000)]
        public IEnumerator MaxTimeを1秒に設定()
        {
            var waitSeconds = 1f;
            if (Fail)
            {
                waitSeconds = 10f;
            }

            yield return new WaitForSeconds(waitSeconds);
        }

        [UnityTest]
        [MaxTime(2000)]
        public IEnumerator MaxTime値はTimeScaleに影響されない()
        {
            var waitSeconds = 1f;
            if (Fail)
            {
                waitSeconds = 10f;
            }

            Time.timeScale = 5f;
            yield return new WaitForSecondsRealtime(waitSeconds);
        }
    }
}
