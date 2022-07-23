// Copyright (c) 2021 Koji Hasegawa.
// This software is released under the MIT License.

using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace APIExamples.UnityTestFramework
{
    /// <summary>
    /// <see cref="UnitySetUpAttribute"/>の使用例
    /// </summary>
    /// <remarks>
    /// WebGLPlayerでUnitySetUp属性がエラーを吐くため、テストクラスごと除外
    /// </remarks>
    [TestFixture]
    [UnityPlatform(exclude = new[] { RuntimePlatform.WebGLPlayer })]
    public class UnitySetUpAttributeExample
    {
        /// <summary>
        /// SetUpをコルーチンで記述できます
        /// <see cref="UnityEngine.TestTools.UnityTestAttribute"/>専用ではなく、通常のTest向けのSetUpとしても使用できます
        /// </summary>
        [UnitySetUp]
        public IEnumerator SetUpCoroutine()
        {
            yield return SceneManager.LoadSceneAsync("HelloTesting");
        }

        [Test]
        public void Test属性テストメソッド()
        {
            var cube = GameObject.Find("Cube");
            Object.DestroyImmediate(cube);

            Assert.That((bool)cube, Is.False);
        }

        [UnityTest, UnityPlatform(exclude = new[] { RuntimePlatform.WebGLPlayer })]
        public IEnumerator UnityTest属性テストメソッド()
        {
            var cube = GameObject.Find("Cube");
            Object.Destroy(cube);
            yield return null;

            Assert.That((bool)cube, Is.False);
        }
    }
}
