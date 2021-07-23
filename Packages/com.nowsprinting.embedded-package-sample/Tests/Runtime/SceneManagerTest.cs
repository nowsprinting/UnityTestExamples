// Copyright (c) 2021 Koji Hasegawa.
// This software is released under the MIT License.

using System.Collections;
using NUnit.Framework;
using EmbeddedPackageSample.Utils;
using UnityEngine;
using UnityEngine.TestTools;

namespace EmbeddedPackageSample
{
    [TestFixture]
    public class SceneManagerTest
    {
        [UnityTest]
        public IEnumerator LoadSceneAsync_ScenesInBuildに含まれていないSceneをロードする例()
        {
            yield return TestSceneLoader.LoadSceneAsync("Packages/com.nowsprinting.embedded-package-sample/Scenes/SceneInPackage.unity");
            // パスはProjectウィンドウに表示されるパスで指定。ただしパッケージ名の部分は `displayName` でなく `name` を使用。

            var capsule = GameObject.Find("Capsule");
            Assert.That(capsule, Is.Not.Null);
        }
    }
}
