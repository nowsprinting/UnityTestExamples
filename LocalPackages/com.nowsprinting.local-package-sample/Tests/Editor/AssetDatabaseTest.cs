// Copyright (c) 2021 Koji Hasegawa.
// This software is released under the MIT License.

using System.IO;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

namespace LocalPackageSample.Editor
{
    public class AssetDatabaseTest
    {
        [Test]
        public void LoadAssetAtPath_prefabをロードして使用する例()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Packages/com.nowsprinting.local-package-sample/Prefabs/PrefabInPackage.prefab");
            // パスはProjectウィンドウに表示されるパスで指定。ただしパッケージ名の部分は `displayName` でなく `name` を使用。

            var child = prefab.transform.Find("Child");
            var grandchild = child.Find("GrandchildInLocalPackage");

            Assert.That(grandchild, Is.Not.Null);
        }

        [Test]
        [UnityPlatform(RuntimePlatform.LinuxEditor, RuntimePlatform.OSXEditor)]
        public void パッケージ内ファイルの絶対パスを取得する例()
        {
            var absolute = Path.GetFullPath("Packages/com.nowsprinting.local-package-sample/Prefabs/PrefabInPackage.prefab");

            Assert.That(absolute, Does.EndWith("/LocalPackages/com.nowsprinting.local-package-sample/Prefabs/PrefabInPackage.prefab"));
            // 絶対パスとして、実際のファイルパスが得られます。`System.IO`のメソッドでファイルを扱うには、こちらのパスを使用します
        }

        [Test]
        [UnityPlatform(RuntimePlatform.WindowsEditor)]
        public void パッケージ内ファイルの絶対パスを取得する例_Windows()
        {
            var absolute = Path.GetFullPath("Packages/com.nowsprinting.local-package-sample/Prefabs/PrefabInPackage.prefab");

            Assert.That(absolute, Does.EndWith("\\LocalPackages\\com.nowsprinting.local-package-sample\\Prefabs\\PrefabInPackage.prefab"));
            // 絶対パスとして、実際のファイルパスが得られます。`System.IO`のメソッドでファイルを扱うには、こちらのパスを使用します
        }
    }
}
