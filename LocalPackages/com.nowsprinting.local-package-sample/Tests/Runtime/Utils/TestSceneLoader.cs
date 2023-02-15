// Copyright (c) 2021 Koji Hasegawa.
// This software is released under the MIT License.

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

// ReSharper disable once CheckNamespace (rootNamespace not work in Unity 2019)
namespace LocalPackageSample.Utils
{
    public static class TestSceneLoader
    {
        /// <summary>
        /// Play Modeテスト内でSceneをロードする
        /// </summary>
        /// <remarks>
        /// エディター実行では <see cref="UnityEditor.SceneManagement.EditorSceneManager.LoadSceneAsyncInPlayMode"/> でロード。
        /// プレイヤー実行では <see cref="UnityEditor.TestTools.ITestPlayerBuildModifier"/> を使用して
        /// 一時的にビルド対象に含めている前提で <see cref="SceneManager.LoadSceneAsync(string)"/> でロードしています。
        /// </remarks>
        /// <param name="path">Packages/〜.unityまでのパスで指定</param>
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
