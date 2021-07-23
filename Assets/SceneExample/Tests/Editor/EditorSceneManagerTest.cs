// Copyright (c) 2021 Koji Hasegawa.
// This software is released under the MIT License.

using NUnit.Framework;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace SceneExample
{
    [TestFixture]
    public class EditorSceneManagerTest
    {
        [Test]
        public void NewScene_クリーンなSceneを生成してテストを実行する例()
        {
            EditorSceneManager.NewScene(NewSceneSetup.EmptyScene);

            var actual = Object.FindObjectsOfType<GameObject>();
            Assert.That(actual, Is.Empty);
        }

        [Test]
        public void OpenScene_ScenesInBuildに含まれるSceneをロードする例()
        {
            EditorSceneManager.OpenScene("Assets/BasicExample/Scenes/HelloTesting.unity");
            // Scenes in Buildに含まれるSceneであってもパス指定が必要

            var cube = GameObject.Find("Cube");
            Assert.That(cube, Is.Not.Null);

            Object.DestroyImmediate(cube); // Edit Modeテスト内でSceneに変更を加えてもファイルには反映されない
        }

        [Test]
        public void OpenScene_ScenesInBuildに含まれていないSceneをロードする例()
        {
            EditorSceneManager.OpenScene("Assets/SceneExample/Scenes/NotContainScenesInBuild.unity");

            var sphere = GameObject.Find("Sphere");
            Assert.That(sphere, Is.Not.Null);

            Object.DestroyImmediate(sphere); // Edit Modeテスト内でSceneに変更を加えてもファイルには反映されない
        }
    }
}
