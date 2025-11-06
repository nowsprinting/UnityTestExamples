// Copyright (c) 2021-2025 Koji Hasegawa.
// This software is released under the MIT License.

using NUnit.Framework;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace EmbeddedPackageSample.Editor
{
    [TestFixture]
    public class EditorSceneManagerTest
    {
        [Test]
        public void OpenScene_Sceneファイルをロードしてテストを実行する例()
        {
            EditorSceneManager.OpenScene(
                "Packages/com.nowsprinting.embedded-package-sample/Scenes/SceneInEmbeddedPackage.unity");
            // パスはProjectウィンドウに表示されるパスで指定。ただしパッケージ名の部分は `displayName` でなく `name` を使用。

            var capsule = GameObject.Find("Capsule");
            Assert.That(capsule, Is.Not.Null);

            Object.DestroyImmediate(capsule); // Edit Modeテスト内でSceneに変更を加えてもファイルには反映されない
        }
    }
}
