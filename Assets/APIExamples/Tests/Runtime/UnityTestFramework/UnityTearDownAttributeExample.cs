﻿// Copyright (c) 2021 Koji Hasegawa.
// This software is released under the MIT License.

using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.TestTools.Utils;

namespace APIExamples.UnityTestFramework
{
    /// <summary>
    /// <see cref="UnityTearDownAttribute"/>の使用例
    /// </summary>
    [TestFixture]
    public class UnityTearDownAttributeExample
    {
        private Object _cube;

        /// <summary>
        /// TearDownをコルーチンで記述できます
        /// <see cref="UnityEngine.TestTools.UnityTestAttribute"/>専用ではなく、通常のTest向けのTearDownとしても使用できます
        /// </summary>
        [UnityTearDown]
        public IEnumerator TearDownCoroutine()
        {
            Object.Destroy(_cube);
            yield return null;
            // Note: WebGLでも動作しているか確認するためにここで`Assert.Fail()`したらfailしたので動いている模様
        }

        [Test]
        public void Test属性のテストメソッド()
        {
            _cube = Utils.CreatePrimitive(PrimitiveType.Cube);

            Assert.That((bool)_cube, Is.True);
        }

        [UnityTest]
        public IEnumerator UnityTest属性のテストメソッド()
        {
            _cube = Utils.CreatePrimitive(PrimitiveType.Cube);
            yield return null;

            Assert.That((bool)_cube, Is.True);
        }
    }
}
