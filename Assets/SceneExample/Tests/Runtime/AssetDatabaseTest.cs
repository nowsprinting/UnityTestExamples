// Copyright (c) 2021-2025 Koji Hasegawa.
// This software is released under the MIT License.

using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace SceneExample
{
    [TestFixture]
    [Description("エディター実行のみ")]
    [UnityPlatform(RuntimePlatform.OSXEditor, RuntimePlatform.WindowsEditor, RuntimePlatform.LinuxEditor)]
    public class AssetDatabaseTest
    {
        [Test]
        public void LoadAssetAtPath_Prefabをロードして使用する例()
        {
#if UNITY_EDITOR
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/SceneExample/Prefabs/PrefabExample.prefab");
            var child = prefab.transform.Find("Child");
            var grandchild = child.Find("Grandchild");

            Assert.That(grandchild, Is.Not.Null);
#endif
        }

        [Test]
        public void LoadAssetAtPath_テキストファイルをロードして使用する例()
        {
#if UNITY_EDITOR
            var text = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/SceneExample/TextAssets/TextExample.txt");

            Assert.That(text.text, Does.Contain("text asset example"));
#endif
        }
    }
}
