// Copyright (c) 2021-2023 Koji Hasegawa.
// This software is released under the MIT License.

using NUnit.Framework;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace SceneExample.Editor
{
    [TestFixture]
    public class EditorSceneManagerTest
    {
        [Test]
        [Description("Generate a Scene with Light and Camera by NewScene(DefaultGameObjects) method")]
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
        [Description("Generate a clean Scene by NewScene(EmptyScene) method")]
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

            Assert.That(scene1.isLoaded, Is.False);
            Assert.That(scene2.isLoaded, Is.True);

            var behaviours = Object.FindObjectsOfType<Behaviour>();
            Assert.That(behaviours, Is.Empty);
        }

        [Test]
        [Description("Load scene included in the \"Scenes in Build\" by OpenScene method")]
        public void OpenScene_ScenesInBuildに含まれるSceneをロードする例()
        {
            EditorSceneManager.OpenScene("Assets/BasicExample/Scenes/HelloTesting.unity");
            // Scenes in Buildに含まれるSceneであってもパス指定が必要
            // You need to specify the path.

            var cube = GameObject.Find("Cube");
            Assert.That(cube, Is.Not.Null);

            Object.DestroyImmediate(cube); // Edit Modeテスト内でSceneに変更を加えてもファイルには反映されない
            // Not update scene file by changes in Edit Mode tests.
        }

        [Test]
        [Description("Load scene not included in the \"Scenes in Build\" by OpenScene method")]
        public void OpenScene_ScenesInBuildに含まれていないSceneをロードする例()
        {
            EditorSceneManager.OpenScene("Assets/SceneExample/Scenes/NotContainScenesInBuild.unity");

            var sphere = GameObject.Find("Sphere");
            Assert.That(sphere, Is.Not.Null);

            Object.DestroyImmediate(sphere); // Edit Modeテスト内でSceneに変更を加えてもファイルには反映されない
            // Not update scene file by changes in Edit Mode tests.
        }
    }
}
