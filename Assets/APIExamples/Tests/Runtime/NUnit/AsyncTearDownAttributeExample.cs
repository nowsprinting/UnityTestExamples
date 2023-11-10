// Copyright (c) 2021-2023 Koji Hasegawa.
// This software is released under the MIT License.

using System.Threading.Tasks;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

#pragma warning disable CS1998

namespace APIExamples.NUnit
{
    /// <summary>
    /// 非同期の<see cref="TearDownAttribute"/>の例
    /// <see cref="OneTimeTearDownAttribute"/>はasyncサポートされていない（UTF v1.3時点）
    /// Async TearDown attribute example
    /// Async OneTimeTearDown attribute is not yet supported in UTF v1.3
    /// </summary>
    /// <remarks>
    /// Required: Unity Test Framework v1.3 or later
    /// </remarks>
    [TestFixture]
    [UnityPlatform(exclude = new[] { RuntimePlatform.WebGLPlayer })]
    // WebGLではTask.Delayが終了しない（v1.3.9時点） https://unity3d.atlassian.net/servicedesk/customer/portal/2/IN-28109
    public class AsyncTearDownAttributeExample
    {
        /// <summary>
        /// 各テストメソッドの後に実行されます
        /// </summary>
        [TearDown]
        public async Task TearDown()
        {
            await Task.Delay(200);
            Debug.Log($"TearDown, {Time.time}");
        }

        [Test]
        public void TestMethod()
        {
            Debug.Log($"TestMethod, {Time.time}");
        }

        [Test]
        public void TestMethod2()
        {
            Debug.Log($"TestMethod2, {Time.time}");
        }
    }
}
