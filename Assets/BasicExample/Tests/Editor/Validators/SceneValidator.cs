// Copyright (c) 2021-2025 Koji Hasegawa.
// This software is released under the MIT License.

using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace BasicExample.Editor.Validators
{
    /// <summary>
    /// Assets/ 下のすべての Scene に対して、missing script がないことを検証する.
    /// </summary>
    /// <remarks>
    /// <see cref="TestCaseSourceAttribute"/> の使用例
    /// </remarks>
    [TestFixture]
    public class SceneValidator
    {
        private static IEnumerable<TestCaseData> Scenes => AssetDatabase
            .FindAssets("t:SceneAsset", new string[] { "Assets/" })
            .Select(AssetDatabase.GUIDToAssetPath)
            .Select(path => new TestCaseData(path).SetName(Path.GetFileName(path)));

        private static void AssertMissingScriptRecursive(GameObject gameObject)
        {
            var components = gameObject.GetComponents<Component>();
            foreach (var component in components)
            {
                Assert.That(component, Is.Not.Null, $"Component in {gameObject.name}");
            }

            foreach (Transform child in gameObject.transform)
            {
                AssertMissingScriptRecursive(child.gameObject);
            }
        }

        [TestCaseSource(nameof(Scenes))]
        public void MissingScriptがないこと(string path)
        {
            var scene = EditorSceneManager.OpenScene(path);
            foreach (var root in scene.GetRootGameObjects())
            {
                AssertMissingScriptRecursive(root);
            }
        }
    }
}
