// Copyright (c) 2021 Koji Hasegawa.
// This software is released under the MIT License.

using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

// ReSharper disable once CheckNamespace (rootNamespace not work in Unity 2019)
namespace LocalPackageSample
{
    [Description("エディター実行のみ")]
    [UnityPlatform(RuntimePlatform.OSXEditor, RuntimePlatform.WindowsEditor, RuntimePlatform.LinuxEditor)]
    public class AssetDatabaseTest
    {
        [Test]
        public void LoadAssetAtPath_Prefabをロードして使用する例()
        {
#if UNITY_EDITOR
            var prefab = UnityEditor.AssetDatabase
                .LoadAssetAtPath<GameObject>("Packages/com.nowsprinting.local-package-sample/Prefabs/PrefabInPackage.prefab");
            // パスはProjectウィンドウに表示されるパスで指定。ただしパッケージ名の部分は `displayName` でなく `name` を使用。

            var child = prefab.transform.Find("Child");
            var grandchild = child.Find("GrandchildInLocalPackage");

            Assert.That(grandchild, Is.Not.Null);
#endif
        }
    }
}
