// Copyright (c) 2021 Koji Hasegawa.
// This software is released under the MIT License.

using NUnit.Framework;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace LocalPackageSample
{
    [TestFixture]
    public class EditorSceneManagerTest
    {
        [Test]
        public void OpenScene_Sceneファイルをロードしてテストを実行する例()
        {
            EditorSceneManager.OpenScene("Packages/com.nowsprinting.local-package-sample/Scenes/SceneInPackage.unity");
            // パスはProjectウィンドウに表示されるパスで指定。ただしパッケージ名の部分は `displayName` でなく `name` を使用。

            var cylinder = GameObject.Find("Cylinder");
            Assert.That(cylinder, Is.Not.Null);

            Object.DestroyImmediate(cylinder); // Edit Modeテスト内でSceneに変更を加えてもファイルには反映されない
        }
    }
}
