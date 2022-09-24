// Copyright (c) 2021-2022 Koji Hasegawa.
// This software is released under the MIT License.

using System;
using System.Collections;
using System.IO;
using System.Reflection;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

namespace APIExamples.Editor.UnityTestFramework
{
    /// <summary>
    /// <see cref="IEditModeTestYieldInstruction"/>の使用例
    /// </summary>
    public class EditModeTestYieldInstructionExample
    {
        private const string CsPath = "Assets/CreateFileExample.cs";
        private const string DLLSrcPath = "TestData/NativePluginExample.dll";
        private const string DLLDstPath = "Assets/NativePluginExample.dll";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Assume.That(EditorSettings.enterPlayModeOptionsEnabled, Is.False, "Enter Play Mode Options enabled");
            // Note: Edit | Project Settings... | Editor | Enter Play Mode Settings が変更されていないことが前提

            Debug.Log("OneTimeSetUpはドメインリロードで再実行されます");
        }

        [UnitySetUp]
        public IEnumerator UnitySetUp()
        {
            Debug.Log("UnitySetUpはドメインリロードで再実行されません");
            yield break;
        }

        [SetUp]
        public void SetUp()
        {
            Debug.Log("SetUpはドメインリロードで再実行されます");
        }

        [TearDown]
        public void TearDown()
        {
            AssetDatabase.DeleteAsset(CsPath);
            AssetDatabase.DeleteAsset(DLLDstPath);
            // Note: ドメインリロードを伴うテストなので、SetUpで初期化は行わない。ドメインリロード後に再度SetUpが呼ばれるため
        }

        [UnityTest]
        public IEnumerator EnterPlayModeの使用例()
        {
            yield return new EnterPlayMode();
            Assert.That(EditorApplication.isPlaying, Is.True);
            // Note: 特に後処理をしなくてもテスト終了後に編集モードに戻ります
        }

        [UnityTest]
        public IEnumerator ExitPlayModeの使用例()
        {
            yield return new EnterPlayMode();
            Assume.That(EditorApplication.isPlaying, Is.True);

            yield return new ExitPlayMode();
            Assert.That(EditorApplication.isPlaying, Is.False);
        }

        [UnityTest]
        public IEnumerator RecompileScriptsでリコンパイルされたクラスのメソッドを呼び出す例()
        {
            Assume.That(Path.GetFullPath(CsPath), Does.Not.Exist,
                "Destination file already exists. please remove and re-run this test.");

            using (var file = File.CreateText(Path.GetFullPath(CsPath)))
            {
                file.Write("public class Foo { public bool Bar(string msg) { return true; } }");
            }

            // ここでAssetDatabase.Refresh()は不要
            yield return new RecompileScripts();

            // リフレクションでメソッド呼び出し
            var assemblyName = new AssemblyName("Assembly-CSharp");
            var assembly = Assembly.Load(assemblyName);
            var fooType = assembly.GetType("Foo");
            dynamic foo = Activator.CreateInstance(fooType);
            bool actual = foo.Bar("Baz");

            Assert.That(actual, Is.True);
        }

        [UnityTest]
        public IEnumerator WaitForDomainReloadで追加されたアセンブリのメソッドを呼び出す例()
        {
            Assume.That(Path.GetFullPath(DLLDstPath), Does.Not.Exist,
                "Destination file already exists. please remove and re-run this test.");

            File.Copy(Path.GetFullPath(DLLSrcPath), Path.GetFullPath(DLLDstPath), true);

            // ここでAssetDatabase.Refresh()は不要
            yield return new WaitForDomainReload();

            // リフレクションでメソッド呼び出し
            var assembly = Assembly.LoadFrom(Path.GetFullPath(DLLDstPath));
            var fooType = assembly.GetType("NativePluginExample.Foo");
            dynamic foo = Activator.CreateInstance(fooType);
            bool actual = foo.Bar("Baz");

            Assert.That(actual, Is.True);
        }
    }
}
