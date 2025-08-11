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
    [TestFixture]
    public class UnitySetUpAttributeExample
    {
        /// <summary>
        /// SetUpをコルーチンで記述できます
        /// <see cref="UnityEngine.TestTools.UnityTestAttribute"/>専用ではなく、通常のTest向けのSetUpとしても使用できます
        /// </summary>
        [UnitySetUp]
        public IEnumerator SetUpCoroutine()
        {
            yield return SceneManager.LoadSceneAsync("ContainScenesInBuild");
        }

        [Test]
        public void Test属性テストメソッド()
        {
            var cube = GameObject.Find("Cube");
            Object.DestroyImmediate(cube);

            Assert.That((bool)cube, Is.False);
        }

        [UnityTest]
        public IEnumerator UnityTest属性テストメソッド()
        {
            var cube = GameObject.Find("Cube");
            Object.Destroy(cube);
            yield return null;

            Assert.That((bool)cube, Is.False);
        }
    }
}
