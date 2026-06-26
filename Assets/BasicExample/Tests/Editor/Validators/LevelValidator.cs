// Copyright (c) 2021-2026 Koji Hasegawa.
// This software is released under the MIT License.

using System.Collections.Generic;
using System.IO;
using System.Linq;
using BasicExample.Level;
using NUnit.Framework;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace BasicExample.Editor.Validators
{
    /// <summary>
    /// Scenes/Levels/ 下のすべての Scene に対して、次のコンポーネントが設置されていることを検証する
    /// <list type="bullet">
    ///     <item><see cref="BasicExample.Level.SpawnPoint"/></item>
    ///     <item><see cref="BasicExample.Level.ExitPoint"/></item>
    /// </list>
    /// </summary>
    /// <remarks>
    /// <see cref="TestCaseSourceAttribute"/> の使用例
    /// </remarks>
    [TestFixture]
    public class LevelValidator
    {
        private static IEnumerable<TestCaseData> Levels => AssetDatabase
            .FindAssets("t:SceneAsset", new[] { "Assets/BasicExample/Scenes/Levels" })
            .Select(AssetDatabase.GUIDToAssetPath)
            .Select(path => new TestCaseData(path).SetName(Path.GetFileName(path)));

        [TestCaseSource(nameof(Levels))]
        public void Levels下のSceneにSpawnPointが1つ設置されていること(string path)
        {
            EditorSceneManager.OpenScene(path);
#if UNITY_6000_4_OR_NEWER
            var spawnPoints = Object.FindObjectsByType<SpawnPoint>();
#elif UNITY_2022_3_OR_NEWER
            var spawnPoints = Object.FindObjectsByType<SpawnPoint>(FindObjectsSortMode.None);
#else
            var spawnPoints = Object.FindObjectsOfType<SpawnPoint>();
#endif

            Assert.That(spawnPoints, Has.Length.EqualTo(1));
        }

        [TestCaseSource(nameof(Levels))]
        public void Levels下のSceneにExitPointが設置されていること(string path)
        {
            EditorSceneManager.OpenScene(path);
#if UNITY_6000_4_OR_NEWER
            var exitPoints = Object.FindObjectsByType<ExitPoint>();
#elif UNITY_2022_3_OR_NEWER
            var exitPoints = Object.FindObjectsByType<ExitPoint>(FindObjectsSortMode.None);
#else
            var exitPoints = Object.FindObjectsOfType<ExitPoint>();
#endif

            Assert.That(exitPoints, Is.Not.Empty, "ExitPointは1つ以上設定されている");

            foreach (var exitPoint in exitPoints)
            {
                var obj = exitPoint.gameObject;
                var colliders = obj.GetComponents<Collider>();
                Assert.That(colliders, Is.Not.Empty, $"{obj.name}にはコライダが設定されている");
            }
        }
    }
}
