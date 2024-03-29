﻿// Copyright (c) 2021-2023 Koji Hasegawa.
// This software is released under the MIT License.

using NUnit.Framework;
using UnityEngine;

namespace APIExamples.NUnit
{
    /// <summary>
    /// <see cref="SetUpAttribute"/>, <see cref="OneTimeSetUpAttribute"/>の例
    /// </summary>
    [TestFixture]
    public class SetupAttributeExample
    {
        /// <summary>
        /// クラス内の最初のテストの実行前に一度だけ実行されます
        /// </summary>
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Debug.Log($"OneTimeSetUp, {Time.time}");
        }

        /// <summary>
        /// 各テストメソッドの前に実行されます
        /// </summary>
        [SetUp]
        public void SetUp()
        {
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
