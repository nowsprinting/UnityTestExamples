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
        private static bool s_isDomainReloadPending;

        private const string CsPath = "Assets/APIExamples/Scripts/Runtime/CreateFileExample.cs";
        private const string RspPath = "Assets/APIExamples/Scripts/Runtime/csc.rsp";
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
            AssetDatabase.DeleteAsset(RspPath);
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

        private static IEnumerator WaitForPendingDomainReload()
        {
            // Note: RecompileScriptsはコンパイル完了までしか待たず、ドメインリロードが遅れて実行されることがあるため待機する。
            // WaitForDomainReloadを使わないのは、ドメインリロードがRecompileScripts中に完了していると例外をスローするため
            while (s_isDomainReloadPending)
            {
                yield return null;
            }
        }

        [UnityTest]
        [Timeout(60000)]
        public IEnumerator RecompileScriptsでリコンパイルを実行する例_リコンパイルされたクラスのメソッドを呼び出せる()
        {
            Assume.That(Path.GetFullPath(CsPath), Does.Not.Exist,
                "Destination file already exists. please remove and re-run this test.");

            using (var file = File.CreateText(Path.GetFullPath(CsPath)))
            {
                file.Write("public class Foo { public bool Bar(string msg) { return true; } }");
            }

            s_isDomainReloadPending = true;

            // ここでAssetDatabase.Refresh()は不要
            yield return new RecompileScripts();
            yield return WaitForPendingDomainReload();

            // リフレクションでメソッド呼び出し
            var assemblyName = new AssemblyName("APIExamples");
            var assembly = Assembly.Load(assemblyName);
            var fooType = assembly.GetType("Foo");
            dynamic foo = Activator.CreateInstance(fooType);
            bool actual = foo.Bar("Baz");

            Assert.That(actual, Is.True);
        }

        [UnityTest]
        [Timeout(60000)]
        public IEnumerator RecompileScriptsでリコンパイルを実行する例_csc_rspで追加したスクリプティングシンボルが有効になる()
        {
            Assume.That(Path.GetFullPath(RspPath), Does.Not.Exist,
                "Destination file already exists. please remove and re-run this test.");
            Assume.That(Path.GetFullPath(CsPath), Does.Not.Exist,
                "Destination file already exists. please remove and re-run this test.");

            File.WriteAllText(Path.GetFullPath(RspPath), "-define:CSC_RSP_EXAMPLE");
            File.WriteAllText(Path.GetFullPath(CsPath),
                "public static class Foo { public static bool IsDefined() {\n" +
                "#if CSC_RSP_EXAMPLE\n" +
                "return true;\n" +
                "#else\n" +
                "return false;\n" +
                "#endif\n" +
                "} }");

            s_isDomainReloadPending = true;

            // ここでAssetDatabase.Refresh()は不要
            yield return new RecompileScripts();
            yield return WaitForPendingDomainReload();

            // リフレクションでメソッド呼び出し
            var assembly = Assembly.Load(new AssemblyName("APIExamples"));
            var fooType = assembly.GetType("Foo");
            var actual = (bool)fooType.GetMethod("IsDefined")!.Invoke(null, null);

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
