﻿// Copyright (c) 2022-2023 Koji Hasegawa.
// This software is released under the MIT License.

using System.Threading.Tasks;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace APIExamples.Editor.NUnit
{
    /// <summary>
    /// 非同期の<see cref="SetUpAttribute"/>, <see cref="TearDownAttribute"/>の例（Edit Modeテスト）
    /// <see cref="OneTimeSetUpAttribute"/>, <see cref="OneTimeTearDownAttribute"/>はasyncサポートされていない（UTF v1.3時点）
    /// Async SetUp and TearDown attribute example (in Edit Mode tests)
    /// Async OneTimeSetUp and OneTimeTearDown attribute is not yet supported in UTF v1.3
    /// </summary>
    /// <remarks>
    /// Required: Unity Test Framework v1.3 or later
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
            await Task.Delay(200); // Note: Edit Modeテストでは実際はDelayはされない
        }

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