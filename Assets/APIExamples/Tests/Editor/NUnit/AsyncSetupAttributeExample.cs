﻿// Copyright (c) 2021-2023 Koji Hasegawa.
// This software is released under the MIT License.

using System.Threading.Tasks;
using NUnit.Framework;
using UnityEngine;

#pragma warning disable CS1998

namespace APIExamples.Editor.NUnit
{
    /// <summary>
    /// 非同期の<see cref="SetUpAttribute"/>の例（Edit Modeテスト）
    /// <see cref="OneTimeSetUpAttribute"/>はasyncサポートされていない（UTF v1.3時点）
    /// Async SetUp attribute example (in Edit Mode tests)
    /// Async OneTimeSetUp attribute is not yet supported in UTF v1.3
    /// </summary>
    /// <remarks>
    /// Required: Unity Test Framework v1.3 or later
    /// </remarks>
    [TestFixture]
    public class AsyncSetupAttributeExample
    {
        /// <summary>
        /// 各テストメソッドの前に実行されます
        /// </summary>
        [SetUp]
        public async Task SetUp()
        {
            await Task.Delay(200);
            Debug.Log($"SetUp, {Time.time}");
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
