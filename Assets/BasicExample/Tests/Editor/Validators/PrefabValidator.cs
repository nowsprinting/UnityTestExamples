// Copyright (c) 2021-2025 Koji Hasegawa.
// This software is released under the MIT License.

using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

namespace BasicExample.Editor.Validators
{
    /// <summary>
    /// Assets/ 下のすべての Prefab に対して、missing script がないことを検証する.
    /// </summary>
    /// <remarks>
    /// <see cref="TestCaseSourceAttribute"/> の使用例
    /// </remarks>
    [TestFixture]
    public class PrefabValidator
    {
        private static IEnumerable<TestCaseData> Prefabs => AssetDatabase
            .FindAssets("t:Prefab", new string[] { "Assets/" })
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

        [TestCaseSource(nameof(Prefabs))]
        public void MissingScriptがないこと(string path)
        {
            var root = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            AssertMissingScriptRecursive(root);
        }
    }
}
