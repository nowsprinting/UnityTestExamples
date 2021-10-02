// Copyright (c) 2021 Koji Hasegawa.
// This software is released under the MIT License.

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneExample.Utils
{
    public static class TestSceneLoader
    {
        /// <summary>
        /// Play Modeテスト内でSceneをロードする
        ///
        /// Loading a Scene in a Play Mode tests
        /// </summary>
        /// <remarks>
        /// エディター実行では <see cref="UnityEditor.SceneManagement.EditorSceneManager.LoadSceneAsyncInPlayMode"/> でロード。
        /// プレイヤー実行では <see cref="UnityEditor.TestTools.ITestPlayerBuildModifier"/> を使用して
        /// 一時的に "Scenes in Build" に含めている前提で <see cref="SceneManager.LoadSceneAsync(string)"/> でロードしています。
        ///
        /// In editor execution, load scene with <see cref="UnityEditor.SceneManagement.EditorSceneManager.LoadSceneAsyncInPlayMode"/>.
        /// In player execution, load scene with <see cref="SceneManager.LoadSceneAsync(string)"/> under the assumption
        /// that it is temporarily included in the "Scenes in Build" using the <see cref="UnityEditor.TestTools.ITestPlayerBuildModifier"/>.
        /// </remarks>
        /// <param name="path">Assets/〜.unityまでのパスで指定</param>
        /// <returns></returns>
        public static IEnumerator LoadSceneAsync(string path)
        {
            AsyncOperation loadSceneAsync = null;
#if UNITY_EDITOR
            // Use EditorSceneManager at run on Unity-editor
            loadSceneAsync = UnityEditor.SceneManagement.EditorSceneManager.LoadSceneAsyncInPlayMode(
                path,
                new LoadSceneParameters(LoadSceneMode.Single));
#else
            // Use ITestPlayerBuildModifier to change the "Scenes in Build" list before run on player
            // see: Editor/BuildAllScenesForTestPlayer.cs
            loadSceneAsync = SceneManager.LoadSceneAsync(path);
#endif
            yield return loadSceneAsync;
        }
    }
}
