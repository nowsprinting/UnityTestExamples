// Copyright (c) 2021 Koji Hasegawa.
// This software is released under the MIT License.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BasicExample.Entities;
using BasicExample.Entities.ScriptableObjects;
using NUnit.Framework;
using UnityEditor;
using Object = UnityEngine.Object;

namespace BasicExample.Editor.Validators
{
    /// <summary>
    /// ScriptableObjects/Races/ 下のすべての ScriptableObject に対して、フィールドの設定漏れがないことを検証する.
    /// </summary>
    /// <remarks>
    /// <see cref="ValueSourceAttribute"/> の応用例として書いたものの、フィールドは素直にRace型を使うほうがよさそう
    /// </remarks>
    public class RaceValidator
    {
        // Race型のScriptableObjectを列挙
        private static IEnumerable<string> RacePaths => AssetDatabase
            .FindAssets("t:Race", new[] { "Assets/BasicExample/ScriptableObjects/Races" })
            .Select(AssetDatabase.GUIDToAssetPath);

        // Race型のフィールドを列挙
        private static IEnumerable<FieldInfo> RaceFields =>
            typeof(Race).GetFields(BindingFlags.Instance | BindingFlags.NonPublic);

        [Test]
        public void Raceのフィールドに設定漏れがないこと(
            [ValueSource(nameof(RacePaths))] string path,
            [ValueSource(nameof(RaceFields))] FieldInfo field)
        {
            var obj = AssetDatabase.LoadAssetAtPath<Object>(path);
            var value = field.GetValue(obj);

            switch (value)
            {
                case string s:
                    Assert.That(s, Is.Not.Empty);
                    break;
                case int i:
                    Assert.That(i, Is.GreaterThan(0));
                    break;
                case Array a:
                    Assert.That(a.Length, Is.GreaterThan(0));
                    break;
                case CharacterStatus s:
                    Assert.That(s, Is.Not.Null);
                    break;
                default:
                    Assert.Fail($"Unsupported field type: {field.Name}");
                    break;
            }
        }
    }
}
