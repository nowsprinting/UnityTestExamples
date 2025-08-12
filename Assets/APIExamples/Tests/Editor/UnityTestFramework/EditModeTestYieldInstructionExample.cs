// Copyright (c) 2021-2025 Koji Hasegawa.
// This software is released under the MIT License.

using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

namespace APIExamples.Editor.UnityTestFramework
{
    /// <summary>
    /// <see cref="IEditModeTestYieldInstruction"/>の使用例
    /// </summary>
    [SuppressMessage("Structure", "NUnit1028:The non-test method is public")]
    public class EditModeTestYieldInstructionExample
    {
        private static bool s_wasInitializeOnEnterPlayModeCalled;
        private static bool s_wasRuntimeInitializeOnLoadMethodCalled;
        private static bool s_wasOneTimeSetUpCalled;
        private static bool s_wasUnitySetUpCalled;
        private static bool s_wasSetUpCalled;
        private static bool s_wasSetUpAsyncCalled;

        private const string CsPath = "Assets/CreateFileExample.cs";
        private const string DLLSrcPath = "TestData/NativePluginExample.dll";
        private const string DLLDstPath = "Assets/NativePluginExample.dll";

        [InitializeOnEnterPlayMode]
        public static void InitializeOnEnterPlayMode()
        {
            s_wasInitializeOnEnterPlayModeCalled = true;
        }

        [RuntimeInitializeOnLoadMethod]
        public static void RuntimeInitializeOnLoadMethod()
        {
            s_wasRuntimeInitializeOnLoadMethodCalled = true;
        }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Assume.That(EditorSettings.enterPlayModeOptionsEnabled, Is.False, "Enter Play Mode Options enabled");
            // Note: Edit | Project Settings... | Editor | Enter Play Mode Settings が変更されていないことが前提

            s_wasOneTimeSetUpCalled = true;
        }

        [UnitySetUp]
        public IEnumerator UnitySetUp()
        {
            s_wasUnitySetUpCalled = true;
            yield break;
        }

        [SetUp]
        public void SetUp()
        {
            s_wasSetUpCalled = true;
        }

        [SetUp]
        public async Task SetUpAsync()
        {
            s_wasSetUpAsyncCalled = true;
            await Task.Yield();
        }

        [TearDown]
        public void TearDown()
        {
            AssetDatabase.DeleteAsset(CsPath);
            AssetDatabase.DeleteAsset(DLLDstPath);
            // Note: ドメインリロードを伴うテストなので、SetUpで初期化は行わない。ドメインリロード後に再度SetUpが呼ばれるため
        }

        [UnityTest]
        public IEnumerator EnterPlayModeで再生モードに切り替える例()
        {
            yield return new EnterPlayMode();

            Assert.That(EditorApplication.isPlaying, Is.True);
            // Note: 特に後処理をしなくてもテスト終了後に編集モードに戻ります
        }

        [UnityTest]
        public IEnumerator EnterPlayModeで再生モードに切り替える例_InitializeOnEnterPlayModeが実行される()
        {
            yield return new EnterPlayMode();
            // Note: staticフィールドの値はドメインリロードで初期化されます

            Assert.That(s_wasInitializeOnEnterPlayModeCalled, Is.True);
        }

        [UnityTest]
        public IEnumerator EnterPlayModeで再生モードに切り替える例_RuntimeInitializeOnLoadMethodが実行される()
        {
            yield return new EnterPlayMode();
            // Note: staticフィールドの値はドメインリロードで初期化されます

            Assert.That(s_wasRuntimeInitializeOnLoadMethodCalled, Is.True);
        }

        [UnityTest]
        public IEnumerator ExitPlayModeで編集モードに切り替える例()
        {
            yield return new EnterPlayMode();
            Assume.That(EditorApplication.isPlaying, Is.True);

            yield return new ExitPlayMode();
            Assert.That(EditorApplication.isPlaying, Is.False);
        }

        [UnityTest]
        public IEnumerator RecompileScriptsでリコンパイルを実行する例_リコンパイルされたクラスのメソッドを呼び出せる()
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
        public IEnumerator WaitForDomainReloadでドメインリロードを待機する例_追加されたアセンブリのメソッドを呼び出せる()
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

        [UnityTest]
        public IEnumerator ドメインリロード発生時にOneTimeSetUpは再実行される()
        {
            yield return new EnterPlayMode();
            // Note: staticフィールドの値はドメインリロードで初期化されます

            Assert.That(s_wasOneTimeSetUpCalled, Is.True);
        }

        [UnityTest]
        public IEnumerator ドメインリロード発生時にUnitySetUpは再実行さない()
        {
            yield return new EnterPlayMode();
            // Note: staticフィールドの値はドメインリロードで初期化されます

            Assert.That(s_wasUnitySetUpCalled, Is.False);
        }

        [UnityTest]
        public IEnumerator ドメインリロード発生時にSetUpは再実行される()
        {
            yield return new EnterPlayMode();
            // Note: staticフィールドの値はドメインリロードで初期化されます

            Assert.That(s_wasSetUpCalled, Is.True);
        }

        [UnityTest]
        public IEnumerator ドメインリロード発生時にSetUpAsyncは再実行される()
        {
            yield return new EnterPlayMode();
            // Note: staticフィールドの値はドメインリロードで初期化されます

            Assert.That(s_wasSetUpAsyncCalled, Is.True);
        }
    }
}
