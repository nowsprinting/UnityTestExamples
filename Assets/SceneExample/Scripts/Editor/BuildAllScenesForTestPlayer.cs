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
    /// プレイヤーでのPlay Modeテスト実行前のビルドで "Scenes in Build" にすべてのSceneを一時的に追加します。
    /// これによって、テスト内で <see cref="UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(string)"/> を使用してSceneをロードできるようになります。
    ///
    /// Temporarily add all Scenes to "Scenes in Build" in the build before running the Play Mode tests on the player.
    /// This will allow you to use <see cref="UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(string)"/> to load a Scene in your tests.
    /// </summary>
    /// <remarks>
    /// このスクリプトの利用には、Unity Test Frameworkパッケージ v1.1.13以上が必要です。
    /// 詳細は <see href="https://forum.unity.com/threads/testplayerbuildmodifier-not-working.844447/">フォーラムの報告</see> を参照してください。
    ///
    /// Require Unity Test Framework package v1.1.13 or higher is to use this script.
    /// For details, see the <see href="https://forum.unity.com/threads/testplayerbuildmodifier-not-working.844447/">report in forum</see>.
    /// </remarks>
    public class BuildAllScenesForTestPlayer : ITestPlayerBuildModifier
    {
        public BuildPlayerOptions ModifyOptions(BuildPlayerOptions playerOptions)
        {
            var buildSceneList = new List<string>(playerOptions.scenes);
            foreach (var scene in AssetDatabase.FindAssets("t:SceneAsset", new[] { "Assets" })
                .Select(AssetDatabase.GUIDToAssetPath))
            {
                if (!buildSceneList.Contains(scene))
                {
                    buildSceneList.Add(scene);
                }
            }
            // 引数で渡される `playerOptions.scenes` に入っているSceneの順序を変えると、テストが起動しないことがあります。
            // そのため、不足しているSceneだけを後ろに追加しています。
            // If you change the order of the Scenes in the `playerOptions.scenes` argument, the test may not start.
            // Therefore, only the missing Scenes are added to the back.

            playerOptions.scenes = buildSceneList.ToArray();

            return playerOptions;
        }
    }
}
