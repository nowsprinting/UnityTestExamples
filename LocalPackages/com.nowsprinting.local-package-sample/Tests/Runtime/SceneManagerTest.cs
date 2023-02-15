// Copyright (c) 2021 Koji Hasegawa.
// This software is released under the MIT License.

using System.Collections;
using NUnit.Framework;
using LocalPackageSample.Utils;
using UnityEngine;
using UnityEngine.TestTools;

// ReSharper disable once CheckNamespace (rootNamespace not work in Unity 2019)
namespace LocalPackageSample
{
    [TestFixture]
    public class SceneManagerTest
    {
        [UnityTest]
        public IEnumerator LoadSceneAsync_ScenesInBuildに含まれていないSceneをロードする例()
        {
            yield return TestSceneLoader.LoadSceneAsync("Packages/com.nowsprinting.local-package-sample/Scenes/SceneInPackage.unity");
            // パスはProjectウィンドウに表示されるパスで指定。ただしパッケージ名の部分は `displayName` でなく `name` を使用。

            var cylinder = GameObject.Find("Cylinder");
            Assert.That(cylinder, Is.Not.Null);
        }
    }
}
