// Copyright (c) 2021-2025 Koji Hasegawa.
// This software is released under the MIT License.

using System.Collections;
using NUnit.Framework;
using TestHelper.Attributes;
using UnityEngine;
using UnityEngine.TestTools;

namespace EmbeddedPackageSample
{
    [TestFixture]
    public class SceneManagerTest
    {
        [UnityTest]
        [LoadScene("../../Scenes/SceneInEmbeddedPackage.unity")]
        public IEnumerator LoadSceneAsync_ScenesInBuildに含まれていないSceneをロードする例()
        {
            yield return null;

            var capsule = GameObject.Find("Capsule");
            Assert.That(capsule, Is.Not.Null);
        }
    }
}
