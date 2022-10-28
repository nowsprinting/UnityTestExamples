// Copyright (c) 2022 Koji Hasegawa.
// This software is released under the MIT License.

using System.Threading.Tasks;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace APIExamples.NUnit
{
    /// <summary>
    /// 非同期の<see cref="SetUpAttribute"/>, <see cref="TearDownAttribute"/>の例
    /// Async SetUp and TearDown example
    /// </summary>
    /// <remarks>
    /// Required: Unity Test Framework v1.3 or later
    /// <see cref="OneTimeSetUpAttribute"/>, <see cref="OneTimeTearDownAttribute"/>はasyncサポートされていない
    /// </remarks>
    [UnityPlatform(exclude = new[] { RuntimePlatform.WebGLPlayer })]
    public class AsyncSetupAttributeExample
    {
        /// <summary>
        /// 各テストメソッドの前に実行されます
        /// </summary>
        [SetUp]
        public async Task SetUp()
        {
            Debug.Log($"SetUp, {Time.time}");
            await Task.Delay(200);
        }

        /// <summary>
        /// 各テストメソッドの前に実行されます
        /// </summary>
        [TearDown]
        public async Task TearDown()
        {
            Debug.Log($"TearDown, {Time.time}");
            await Task.Delay(200);
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
