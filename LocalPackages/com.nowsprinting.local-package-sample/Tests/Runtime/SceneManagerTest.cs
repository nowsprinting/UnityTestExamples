// Copyright (c) 2021-2025 Koji Hasegawa.
// This software is released under the MIT License.

using System.Collections;
using NUnit.Framework;
using TestHelper.Attributes;
using UnityEngine;
using UnityEngine.TestTools;

namespace LocalPackageSample
{
    [TestFixture]
    public class SceneManagerTest
    {
        [UnityTest]
        [LoadScene("../../Scenes/SceneInLocalPackage.unity")]
        public IEnumerator LoadSceneAsync_ScenesInBuildに含まれていないSceneをロードする例()
        {
            yield return null;

            var cylinder = GameObject.Find("Cylinder");
            Assert.That(cylinder, Is.Not.Null);
        }
    }
}
