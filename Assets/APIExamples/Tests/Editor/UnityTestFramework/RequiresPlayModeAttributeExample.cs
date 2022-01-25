// Copyright (c) 2022 Koji Hasegawa.
// This software is released under the MIT License.

using NUnit.Framework;
using UnityEditor;
using UnityEngine.TestTools;

namespace APIExamples.Editor.UnityTestFramework
{
    /// <summary>
    /// <see cref="RequiresPlayModeAttribute"/>の使用例
    /// 
    /// RequiresPlayMode属性を付与したテストは、Edit Modeテストアセンブリ内にあってもPlay Modeテストとして実行される
    /// RequiresPlayMode属性は、Assembly/ Class/ Methodに付与できる
    /// ただし、入れ子にはできない（ClassをPlay Mode指定して、MethodをEdit Mode指定するなど）
    ///
    /// Tests with the RequiresPlayModeAttribute run as Play Mode tests even within the Edit Mode test assembly.
    /// RequiresPlayModeAttribute can be added to Assembly/ Class/ Method.
    /// However, it cannot be nested (e.g. Class specified as Play Mode, and Method specified as Edit Mode).
    /// </summary>
    /// <remarks>
    /// Required: Unity Test Framework v2.0 or later
    /// </remarks>
    public class RequiresPlayModeAttributeExample
    {
        [Test]
        [Description(
            "Tests with the RequiresPlayModeAttribute run as Play Mode tests even within the Edit Mode test assembly")]
        [RequiresPlayMode]
        public void RequiresPlayMode属性を付与_EditModeテストアセンブリ内にあってもPlayModeテストとして実行される()
        {
            Assert.That(EditorApplication.isPlaying, Is.True);
        }
    }
}
