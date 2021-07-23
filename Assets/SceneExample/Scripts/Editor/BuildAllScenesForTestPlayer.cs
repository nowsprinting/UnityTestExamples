// Copyright (c) 2021 Koji Hasegawa.
// This software is released under the MIT License.

using System.Collections.Generic;
using System.Linq;
using SceneExample.Editor;
using UnityEditor;
using UnityEditor.TestTools;

[assembly: TestPlayerBuildModifier(typeof(BuildAllScenesForTestPlayer))]

namespace SceneExample.Editor
{
    /// <summary>
    /// プレイヤーでのテスト実行前のビルドで、"Scenes in Build"に指定されていないSceneを一時的にビルドに含めます。
    /// これによって、テスト内で <see cref="UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(string)"/> を使用してSceneをロードできるようになります。
    /// </summary>
    /// <remarks>
    /// このスクリプトの利用には、Unity Test Frameworkパッケージ v1.1.13以上が必要です。
    /// 詳細は <see href="https://forum.unity.com/threads/testplayerbuildmodifier-not-working.844447/">フォーラムの報告</see> を参照してください。
    /// </remarks>
    public class BuildAllScenesForTestPlayer : ITestPlayerBuildModifier
    {
        public BuildPlayerOptions ModifyOptions(BuildPlayerOptions playerOptions)
        {
            var buildSceneList = new List<string>(playerOptions.scenes);
            foreach (var scene in AssetDatabase.FindAssets("t:SceneAsset", new [] { "Assets" })
                .Select(AssetDatabase.GUIDToAssetPath))
            {
                if (!buildSceneList.Contains(scene))
                {
                    buildSceneList.Add(scene);
                }
            }
            // 引数で渡される `playerOptions.scenes` に入っているSceneの順序を変えると、テストが起動しないことがあります。
            // そのため、不足しているSceneだけを後ろに追加しています。

            playerOptions.scenes = buildSceneList.ToArray();

            return playerOptions;
        }
    }
}
