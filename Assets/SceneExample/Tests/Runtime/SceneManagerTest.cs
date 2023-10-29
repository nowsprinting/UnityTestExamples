// Copyright (c) 2021 Koji Hasegawa.
// This software is released under the MIT License.

using System.Collections;
using System.Linq;
using NUnit.Framework;
using TestHelper.Attributes;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace SceneExample
{
    [TestFixture]
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

        [Test]
        [CreateScene(camera: true, light: true)]
        public void CreateSceneAttribute_クリーンなSceneをCreateSceneで生成する例()
        {
            var scene = SceneManager.GetActiveScene();
            Assert.That(scene.name, Is.EqualTo(
                "Scene of SceneExample.SceneManagerTest.CreateSceneAttribute_クリーンなSceneをCreateSceneで生成する例"));

            var rootGameObjectNames = scene.GetRootGameObjects().Select(x => x.name).ToArray();
            Assert.That(rootGameObjectNames, Is.EquivalentTo(new[] { "Main Camera", "Directional Light" }));
        }

        [UnityTest]
        [Description("Load scene included in the \"Scenes in Build\" by LoadSceneAsync method")]
        public IEnumerator LoadSceneAsync_ScenesInBuildに含まれるSceneをロードする例()
        {
            yield return SceneManager.LoadSceneAsync("HelloTesting");
            // Scene名称指定でロードできます。エディター実行でもプレイヤー実行でも、同じコードで動作します。
            // Can be load by Scene name. It works in both in Editor and on Player.

            var cube = GameObject.Find("Cube");
            Assert.That(cube, Is.Not.Null);
        }

        [UnityTest]
        [Description("Load scene not included in the \"Scenes in Build\", Only running in Editor")]
        [UnityPlatform(RuntimePlatform.OSXEditor, RuntimePlatform.WindowsEditor, RuntimePlatform.LinuxEditor)]
        public IEnumerator LoadSceneAsync_ScenesInBuildに含まれていないSceneをEditorSceneManagerを使ってロードする例()
        {
            // ReSharper disable once RedundantAssignment
            AsyncOperation loadSceneAsync = null;
#if UNITY_EDITOR
            loadSceneAsync = UnityEditor.SceneManagement.EditorSceneManager.LoadSceneAsyncInPlayMode(
                "Assets/SceneExample/Scenes/NotContainScenesInBuild.unity",
                new LoadSceneParameters(LoadSceneMode.Single));
#endif
            yield return loadSceneAsync;
        }

        [Test]
        [LoadScene("Assets/SceneExample/Scenes/NotContainScenesInBuild.unity")]
        [Description("Load scene not included in the \"Scenes in Build\"")]
        public void LoadSceneAttribute_ScenesInBuildに含まれていないSceneをLoadScene属性を使ってロードする例()
        {
            // Test Helper パッケージ（com.nowsprinting.test-helper）に含まれる LoadScene 属性は、テスト実行前に指定された Scene をロードします。
            // "Scenes in Build" に含まれていない Scene であっても、エディター実行でもプレイヤー実行でも同じコードで動作します。
            // 『Unity Test Framework完全攻略ガイド』10.2.2 および 10.3 で紹介している方法で、エディター実行・プレイヤー実行の差異を吸収しています。

            // LoadScene attribute in Test Helper package (com.nowsprinting.test-helper) loads Scene before test execution.
            // It works scene not included in "Scenes in Build". It works in both in Editor and on Player, same code.

            var sphere = GameObject.Find("Sphere");
            Assert.That(sphere, Is.Not.Null);
        }
    }
}
