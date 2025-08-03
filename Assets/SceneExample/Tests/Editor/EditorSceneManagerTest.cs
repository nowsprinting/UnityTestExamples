// Copyright (c) 2021-2025 Koji Hasegawa.
// This software is released under the MIT License.

using System.Linq;
using NUnit.Framework;
using TestHelper.Attributes;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneExample.Editor
{
    [TestFixture]
    public class EditorSceneManagerTest
    {
        [Test]
        [Description("Generate a Scene with Light and Camera using NewScene(DefaultGameObjects) method")]
        public void NewScene_withDefaultGameObjects_LightとCameraの設置されたSceneが生成される()
        {
            var scene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects);
            Assert.That(scene.rootCount, Is.EqualTo(2));

            var light = Object.FindObjectOfType<Light>();
            Assert.That(light.name, Is.EqualTo("Directional Light"));
            Assert.That(light.type, Is.EqualTo(LightType.Directional));

            var camera = Object.FindObjectOfType<Camera>();
            Assert.That(camera.name, Is.EqualTo("Main Camera"));
        }

        [Test]
        [Description("Generate a clean Scene using NewScene(EmptyScene) method")]
        public void NewScene_withEmptyScene_クリーンなSceneが生成される()
        {
            var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene);
            Assert.That(scene.rootCount, Is.EqualTo(0));
        }

        [Test]
        [Description("Generate Scene after unloading previous one, when executed multiple times.")]
        public void NewScene_複数回実行_先のものがアンロードされて新しいSceneが生成される()
        {
            var scene1 = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects);
            var scene2 = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene);

            Assert.That(SceneManager.sceneCount, Is.EqualTo(1));
            Assert.That(scene1.isLoaded, Is.False);
            Assert.That(scene2.isLoaded, Is.True);
        }

        [Test]
        [CreateScene(camera: true, light: true)]
        [Description("Generate a clean Scene using CreateSceneAttribute")]
        public void CreateSceneAttribute_クリーンなSceneをCreateScene属性で生成する例()
        {
            var scene = SceneManager.GetActiveScene();
            Assert.That(scene.name, Is.EqualTo(
                $"Scene of SceneExample.Editor.EditorSceneManagerTest.CreateSceneAttribute_クリーンなSceneをCreateScene属性で生成する例"));

            var rootGameObjectNames = scene.GetRootGameObjects().Select(x => x.name).ToArray();
            Assert.That(rootGameObjectNames, Is.EquivalentTo(new[] { "Main Camera", "Directional Light" }));
        }

        [Test]
        [Description("Load scene included in the \"Scenes in Build\" by OpenScene method")]
        public void OpenScene_ScenesInBuildに含まれるSceneをロードする例()
        {
            EditorSceneManager.OpenScene("Assets/SceneExample/Scenes/ContainScenesInBuild.unity");
            // Scenes in Buildに含まれるSceneであってもパス指定が必要
            // You need to specify the path.

            var cube = GameObject.Find("Cube");
            Assert.That(cube, Is.Not.Null);

            Object.DestroyImmediate(cube);
            // Edit Modeテスト内でSceneに変更を加えてもファイルには反映されない
            // Not update scene file by changes in Edit Mode tests.
        }

        [Test]
        [Description("Load scene not included in the \"Scenes in Build\" by OpenScene method")]
        public void OpenScene_ScenesInBuildに含まれていないSceneをロードする例()
        {
            EditorSceneManager.OpenScene("Assets/SceneExample/Scenes/NotContainScenesInBuild.unity");

            var sphere = GameObject.Find("Sphere");
            Assert.That(sphere, Is.Not.Null);

            Object.DestroyImmediate(sphere);
            // Edit Modeテスト内でSceneに変更を加えてもファイルには反映されない
            // Not update scene file by changes in Edit Mode tests.
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
