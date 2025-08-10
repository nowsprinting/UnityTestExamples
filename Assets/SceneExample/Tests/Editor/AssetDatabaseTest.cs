// Copyright (c) 2021-2025 Koji Hasegawa.
// This software is released under the MIT License.

using NUnit.Framework;
using UnityEditor;
using UnityEngine;

namespace SceneExample.Editor
{
    [TestFixture]
    public class AssetDatabaseTest
    {
        [Test]
        public void LoadAssetAtPath_Prefabをロードして使用する例()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/SceneExample/Prefabs/PrefabExample.prefab");
            var child = prefab.transform.Find("Child");
            var grandchild = child.Find("Grandchild");

            Assert.That(grandchild, Is.Not.Null);
        }

        [Test]
        public void LoadAssetAtPath_テキストファイルをロードして使用する例()
        {
            var text = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/SceneExample/TextAssets/TextExample.txt");

            Assert.That(text.text, Does.Contain("text asset example"));
        }
    }
}
