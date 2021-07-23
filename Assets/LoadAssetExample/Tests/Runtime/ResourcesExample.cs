// Copyright (c) 2021 Koji Hasegawa.
// This software is released under the MIT License.

using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace LoadAssetExample
{
    /// <summary>
    /// プレイヤー実行でアセットを使用する例。以下の手順を踏む。
    /// 1. <see cref="UnityEngine.TestTools.IPrebuildSetup"/>でアセットをResourcesフォルダにコピー
    /// 2. テストコードでは<see cref="UnityEngine.Resources.Load(string)"/>でアセット読み込み
    /// 3. <see cref="UnityEngine.TestTools.IPostBuildCleanup"/>でResourcesフォルダをクリア
    /// </summary>
    [Description("プレイヤー実行のみ")]
    [UnityPlatform(RuntimePlatform.LinuxPlayer, RuntimePlatform.WindowsPlayer, RuntimePlatform.OSXPlayer)]
    public class ResourcesExample : IPrebuildSetup, IPostBuildCleanup
    {
        private const string FeaturePath = "Assets/LoadAssetExample";
        private static readonly string s_resourcePath = $"{FeaturePath}/Resources";

        /// <inheritdoc/>
        public void Setup()
        {
#if UNITY_EDITOR
            UnityEditor.AssetDatabase.CreateFolder(FeaturePath, "Resources");
            UnityEditor.AssetDatabase.CreateFolder(s_resourcePath, "LoadAssetExample");
            UnityEditor.FileUtil.CopyFileOrDirectory($"{FeaturePath}/Prefabs", $"{s_resourcePath}/LoadAssetExample/Prefabs");
            UnityEditor.FileUtil.CopyFileOrDirectory($"{FeaturePath}/TextAssets", $"{s_resourcePath}/LoadAssetExample/TextAssets");
            UnityEditor.AssetDatabase.Refresh();
#endif
        }

        /// <inheritdoc/>
        public void Cleanup()
        {
#if UNITY_EDITOR
            UnityEditor.AssetDatabase.DeleteAsset(s_resourcePath);
#endif
        }

        [Test]
        public void Load_プレイヤー実行でPrefabをロードして使用する例()
        {
            var prefab = Resources.Load<GameObject>("LoadAssetExample/Prefabs/PrefabExample");
            // ビルド時に生成するResourcesからのパス、拡張子なし

            var child = prefab.transform.Find("Child");
            var grandchild = child.Find("Grandchild");

            Assert.That(grandchild, Is.Not.Null);
        }

        [Test]
        public void Load_プレイヤー実行でテキストファイルをロードして使用する例()
        {
            var text = Resources.Load<TextAsset>("LoadAssetExample/TextAssets/TextExample");
            // ビルド時に生成するResourcesからのパス、拡張子なし

            Assert.That(text.text, Does.Contain("text asset example"));
        }

    }
}
