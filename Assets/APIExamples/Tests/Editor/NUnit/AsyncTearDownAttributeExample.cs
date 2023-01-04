// Copyright (c) 2021-2023 Koji Hasegawa.
// This software is released under the MIT License.

using System.Threading.Tasks;
using NUnit.Framework;
using UnityEngine;

namespace APIExamples.Editor.NUnit
{
    /// <summary>
    /// 非同期の<see cref="TearDownAttribute"/>の例（Edit Modeテスト）
    /// <see cref="OneTimeTearDownAttribute"/>はasyncサポートされていない（UTF v1.3時点）
    /// Async TearDown attribute example (in Edit Mode tests)
    /// Async OneTimeTearDown attribute is not yet supported in UTF v1.3
    /// </summary>
    /// <remarks>
    /// Required: Unity Test Framework v1.3 or later
    /// </remarks>
    [TestFixture]
    public class AsyncTearDownAttributeExample
    {
        /// <summary>
        /// 各テストメソッドの後に実行されます
        /// </summary>
        [TearDown]
        public async Task TearDown()
        {
            Debug.Log($"TearDown, {Time.time}");
            await Task.Delay(200); // Note: Edit Modeテストでは実際はDelayはされない
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
