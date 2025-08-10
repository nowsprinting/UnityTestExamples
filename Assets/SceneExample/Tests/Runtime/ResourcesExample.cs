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
    /// <summary>
    /// プレイヤー実行でアセットを使用する例。以下の手順を踏む。
    /// 1. <see cref="UnityEngine.TestTools.IPrebuildSetup"/>でアセットをResourcesフォルダにコピー
    /// 2. テストコードでは<see cref="UnityEngine.Resources.Load(string)"/>でアセット読み込み
    /// 3. <see cref="UnityEngine.TestTools.IPostBuildCleanup"/>でResourcesフォルダをクリア
    /// </summary>
    [TestFixture]
    [Description("プレイヤー実行のみ")]
    [UnityPlatform(exclude = new[]
        { RuntimePlatform.OSXEditor, RuntimePlatform.WindowsEditor, RuntimePlatform.LinuxEditor })]
    public class ResourcesExample : IPrebuildSetup, IPostBuildCleanup
    {
        private const string FeaturePath = "Assets/SceneExample";
        private static readonly string s_resourcePath = $"{FeaturePath}/Resources";

        /// <inheritdoc/>
        public void Setup()
        {
#if UNITY_EDITOR
            AssetDatabase.CreateFolder(FeaturePath, "Resources");
            FileUtil.CopyFileOrDirectory($"{FeaturePath}/Prefabs", $"{s_resourcePath}/Prefabs");
            FileUtil.CopyFileOrDirectory($"{FeaturePath}/TextAssets", $"{s_resourcePath}/TextAssets");
            AssetDatabase.Refresh();
#endif
        }

        /// <inheritdoc/>
        public void Cleanup()
        {
#if UNITY_EDITOR
            AssetDatabase.DeleteAsset(s_resourcePath);
#endif
        }

        [Test]
        public void Load_プレイヤー実行でPrefabをロードして使用する例()
        {
            var prefab = Resources.Load<GameObject>("Prefabs/PrefabExample");
            // ビルド時に生成するResourcesからのパス、拡張子なし

            var child = prefab.transform.Find("Child");
            var grandchild = child.Find("Grandchild");

            Assert.That(grandchild, Is.Not.Null);
        }

        [Test]
        public void Load_プレイヤー実行でテキストファイルをロードして使用する例()
        {
            var text = Resources.Load<TextAsset>("TextAssets/TextExample");
            // ビルド時に生成するResourcesからのパス、拡張子なし

            Assert.That(text.text, Does.Contain("text asset example"));
        }
    }
}
