// Copyright (c) 2021-2025 Koji Hasegawa.
// This software is released under the MIT License.

using APIExamples.UnityTestFramework;
using NUnit.Framework;
using UnityEditor;
using UnityEditor.TestTools;

namespace APIExamples.Editor.UnityTestFramework
{
    /// <summary>
    /// Unityエディターに指定したプラットフォームモジュール（Build Support）がインストールされているときのみ実行されるテストの例.
    /// </summary>
    /// <seealso cref="UnityPlatformAttributeExample"/>
    [TestFixture]
    public class RequirePlatformSupportAttributeExample
    {
        [Test]
        [RequirePlatformSupport(BuildTarget.StandaloneOSX)]
        public void macOSモジュールがインストールされているときのみ実行されるテスト()
        {
        }

        [Test]
        [RequirePlatformSupport(BuildTarget.StandaloneWindows)]
        public void Windowsモジュールがインストールされているときのみ実行されるテスト()
        {
        }

        [Test]
        [RequirePlatformSupport(BuildTarget.StandaloneLinux64)]
        public void Linuxモジュールがインストールされているときのみ実行されるテスト()
        {
        }

        [Test]
        [RequirePlatformSupport(BuildTarget.StandaloneOSX, BuildTarget.StandaloneWindows,
            BuildTarget.StandaloneLinux64)] // Note: And条件
        public void スタンドアロンモジュールが3種ともインストールされているときのみ実行されるテスト()
        {
        }

        [Test]
        [RequirePlatformSupport(BuildTarget.WSAPlayer)]
        public void WSAPlayerモジュールがインストールされているときのみ実行されるテスト()
        {
        }
    }
}
