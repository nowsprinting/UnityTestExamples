// Copyright (c) 2021 Koji Hasegawa.
// This software is released under the MIT License.

using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace LoadAssetExample
{
    [Description("エディター実行のみ")]
    [UnityPlatform(RuntimePlatform.OSXEditor, RuntimePlatform.WindowsEditor, RuntimePlatform.LinuxEditor)]
    public class AssetDatabaseTest
    {
        [Test]
        public void LoadAssetAtPath_Prefabをロードして使用する例()
        {
#if UNITY_EDITOR
            var prefab = UnityEditor.AssetDatabase.LoadAssetAtPath<GameObject>("Assets/LoadAssetExample/Prefabs/PrefabExample.prefab");
            var child = prefab.transform.Find("Child");
            var grandchild = child.Find("Grandchild");

            Assert.That(grandchild, Is.Not.Null);
#endif
        }

        [Test]
        public void LoadAssetAtPath_テキストファイルをロードして使用する例()
        {
#if UNITY_EDITOR
            var text = UnityEditor.AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/LoadAssetExample/TextAssets/TextExample.txt");

            Assert.That(text.text, Does.Contain("text asset example"));
#endif
        }
    }
}
