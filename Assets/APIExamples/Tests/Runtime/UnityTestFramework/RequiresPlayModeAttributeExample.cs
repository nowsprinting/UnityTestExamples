// Copyright (c) 2022 Koji Hasegawa.
// This software is released under the MIT License.

using NUnit.Framework;
using UnityEditor;
using UnityEngine.TestTools;

namespace APIExamples.UnityTestFramework
{
    /// <summary>
    /// <see cref="RequiresPlayModeAttribute"/> (false)の使用例
    /// 
    /// RequiresPlayMode属性(false)を付与したテストは、Play Modeテストアセンブリ内にあってもEdit Modeテストとして実行される
    /// RequiresPlayMode属性は、Assembly/ Class/ Methodに付与できる
    /// ただし、入れ子にはできない（ClassをPlay Mode指定して、MethodをEdit Mode指定するなど）
    ///
    /// Tests with the RequiresPlayModeAttribute (false) run as Edit Mode tests even within the Play Mode test assembly.
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
            "Tests with the RequiresPlayModeAttribute(false) run as Edit Mode tests even within the Play Mode test assembly")]
        [RequiresPlayMode(false)]
        public void RequiresPlayMode属性にfalseを付与_PLayModeテストアセンブリ内にあってもEditModeテストとして実行される()
        {
            Assert.That(EditorApplication.isPlaying, Is.False);
            // Note: Run Location: On Playerのとき、コンパイルエラーになる
        }
    }
}
