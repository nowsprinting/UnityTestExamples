// Copyright (c) 2021-2025 Koji Hasegawa.
// This software is released under the MIT License.

using System.Collections.Generic;
using System.Linq;
using BasicExample.Level;
using NUnit.Framework;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace BasicExample.Editor.AssetValidators
{
    /// <summary>
    /// Scenes/Levels/下のすべてのSceneに対して、次のコンポーネントが設置されていることを検証する
    /// - <see cref="BasicExample.Level.SpawnPoint"/>
    /// - <see cref="BasicExample.Level.ExitPoint"/>
    /// </summary>
    /// <remarks>
    /// <see cref="ValueSourceAttribute"/>の応用例
    /// </remarks>
    public class LevelValidator
    {
        private static IEnumerable<string> Levels => AssetDatabase
            .FindAssets("t:SceneAsset", new[] { "Assets/BasicExample/Scenes/Levels" })
            .Select(AssetDatabase.GUIDToAssetPath);

        [Test]
        public void Levels下のSceneにSpawnPointが1つ設置されていること([ValueSource(nameof(Levels))] string path)
        {
            EditorSceneManager.OpenScene(path);
            var spawnPoints = Object.FindObjectsOfType<SpawnPoint>();

            Assert.That(spawnPoints, Has.Length.EqualTo(1));
        }

        [Test]
        public void Levels下のSceneにExitPointが設置されていること([ValueSource(nameof(Levels))] string path)
        {
            EditorSceneManager.OpenScene(path);
            var exitPoints = Object.FindObjectsOfType<ExitPoint>();

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
