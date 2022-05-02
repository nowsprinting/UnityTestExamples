// Copyright (c) 2021 Koji Hasegawa.
// This software is released under the MIT License.

using System.Collections;
using NUnit.Framework;
using SceneExample.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace SceneExample
{
    [TestFixture]
    [UnityPlatform(exclude = new[] { RuntimePlatform.WebGLPlayer })]
    public class SceneManagerTest
    {
        [UnityTest]
        [Description("Generate a clean Scene by CreateScene method")]
        public IEnumerator CreateScene_クリーンなSceneを生成してテストを実行する例()
        {
            var scene = SceneManager.CreateScene("New Scene");
            SceneManager.SetActiveScene(scene);
            yield return null;

            var actual = SceneManager.GetActiveScene().name;
            Assert.That(actual, Is.EqualTo("New Scene"));
        }

        [UnityTest]
        [Description("Load scene included in the \"Scenes in Build\" by LoadSceneAsync method")]
        public IEnumerator LoadSceneAsync_ScenesInBuildに含まれるSceneをロードする例()
        {
            yield return SceneManager.LoadSceneAsync("HelloTesting");
            // Scene名称指定でロードできる
            // Can be load by Scene name.

            var cube = GameObject.Find("Cube");
            Assert.That(cube, Is.Not.Null);
        }

        [UnityTest]
        [Description("Load scene not included in the \"Scenes in Build\"")]
        public IEnumerator LoadSceneAsync_ScenesInBuildに含まれていないSceneをロードする例()
        {
            yield return TestSceneLoader.LoadSceneAsync("Assets/SceneExample/Scenes/NotContainScenesInBuild.unity");
            // エディター実行・プレイヤー実行で扱いが異なる（`TestSceneLoader` 参照）
            // いずれのケースでも、Assets/〜.unityまでのパスで指定が必要
            // Different handling for editor and player execution. It is handled in the TestSceneLoader.
            // In both cases, you need to specify the path to Assets/~.unity.

            var sphere = GameObject.Find("Sphere");
            Assert.That(sphere, Is.Not.Null);
        }
    }
}
