// Copyright (c) 2021 Koji Hasegawa.
// This software is released under the MIT License.

using System.Collections.Generic;
using System.Linq;
using BasicExample.Level;
using NUnit.Framework;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace BasicExample.Editor
{
    /// <summary>
    /// Scenes/Levels/下のすべてのSceneに対して、次のコンポーネントが設置されていることを検証する
    /// - <see cref="BasicExample.Level.SpawnPoint"/>
    /// - <see cref="BasicExample.Level.ExitPoint"/>
    /// </summary>
    /// <remarks>
    /// <see cref="TestCaseSourceAttribute"/>の応用例
    /// </remarks>
    public class LevelValidator
    {
        private static IEnumerable<string> Levels => AssetDatabase
            .FindAssets("t:SceneAsset", new [] { "Assets/BasicExample/Scenes/Levels" })
            .Select(AssetDatabase.GUIDToAssetPath);

        [TestCaseSource(nameof(Levels))]
        public void Levels下のSceneにSpawnPointが設置されていること(string path)
        {
            EditorSceneManager.OpenScene(path);
            var spawnPoints = Object.FindObjectsOfType<SpawnPoint>();

            Assert.That(spawnPoints.Any, Is.True);
        }

        [TestCaseSource(nameof(Levels))]
        public void Levels下のSceneにExitPointが設置されていること(string path)
        {
            EditorSceneManager.OpenScene(path);
            var exitPoints = Object.FindObjectsOfType<ExitPoint>();

            Assert.That(exitPoints.Length, Is.GreaterThanOrEqualTo(1), "ExitPointは1つ以上設定されている");

            foreach (var exitPoint in exitPoints)
            {
                var obj = exitPoint.gameObject;
                var colliders = obj.GetComponents<Collider>();
                Assert.That(colliders.Length, Is.GreaterThanOrEqualTo(1), $"{obj.name}にはコライダが設定されている");
            }
        }
    }
}
