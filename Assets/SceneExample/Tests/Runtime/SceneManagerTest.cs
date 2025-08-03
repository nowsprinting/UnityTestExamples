// Copyright (c) 2021-2025 Koji Hasegawa.
// This software is released under the MIT License.

using System.Collections;
using System.Linq;
using NUnit.Framework;
using TestHelper.Attributes;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace SceneExample
{
    [TestFixture]
    public class SceneManagerTest
    {
        [UnityTest]
        [Description("Generate a clean Scene using CreateScene method")]
        public IEnumerator CreateScene_クリーンなSceneを生成する例()
        {
            var scene = SceneManager.CreateScene("New Scene");
            yield return null;
            Assume.That(SceneManager.GetActiveScene().name, Is.Not.EqualTo("New Scene"));

            SceneManager.SetActiveScene(scene);
            yield return null;

            var actual = SceneManager.GetActiveScene().name;
            Assert.That(actual, Is.EqualTo("New Scene"));

            // TearDown
            yield return SceneManager.UnloadSceneAsync(scene);
        }

        [Test]
        [CreateScene(camera: true, light: true)]
        [Description("Generate a clean Scene using CreateSceneAttribute")]
        public void CreateSceneAttribute_クリーンなSceneをCreateScene属性で生成する例()
        {
            var scene = SceneManager.GetActiveScene();
            Assert.That(scene.name, Is.EqualTo(
                $"Scene of SceneExample.SceneManagerTest.CreateSceneAttribute_クリーンなSceneをCreateScene属性で生成する例"));

            var rootGameObjectNames = scene.GetRootGameObjects().Select(x => x.name).ToArray();
            Assert.That(rootGameObjectNames, Is.EquivalentTo(new[] { "Main Camera", "Directional Light" }));
        }

        [UnityTest]
        [Description("Load scene included in the \"Scenes in Build\" using LoadSceneAsync method")]
        public IEnumerator LoadSceneAsync_ScenesInBuildに含まれるSceneをロードする例()
        {
            yield return SceneManager.LoadSceneAsync("ContainScenesInBuild");
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
            loadSceneAsync = EditorSceneManager.LoadSceneAsyncInPlayMode(
                "Assets/SceneExample/Scenes/NotContainScenesInBuild.unity",
                new LoadSceneParameters(LoadSceneMode.Single));
#endif
            yield return loadSceneAsync;

            var sphere = GameObject.Find("Sphere");
            Assert.That(sphere, Is.Not.Null);
        }

        [Test]
        [LoadScene("Assets/SceneExample/Scenes/NotContainScenesInBuild.unity")]
        [Description("Load scene not included in the \"Scenes in Build\" using LoadSceneAttribute")]
        public void LoadSceneAttribute_ScenesInBuildに含まれていないSceneをLoadScene属性を使ってロードする例()
        {
            // Test Helper パッケージ（com.nowsprinting.test-helper）に含まれる LoadScene 属性は、テスト実行前に指定された Scene をロードします。
            // "Scenes in Build" に含まれていない Scene であっても、エディター実行でもプレイヤー実行でも同じコードで動作します。
            // LoadScene attribute in Test Helper package (com.nowsprinting.test-helper) loads Scene before test execution.
            // It works scene not included in "Scenes in Build". It works in both in Editor and on Player, same code.

            var sphere = GameObject.Find("Sphere");
            Assert.That(sphere, Is.Not.Null);
        }
    }
}
