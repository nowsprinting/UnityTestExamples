// Copyright (c) 2021-2022 Koji Hasegawa.
// This software is released under the MIT License.

using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace APIExamples.UnityTestFramework
{
    /// <summary>
    /// テストが実行されるプラットフォームを<see cref="UnityEngine.TestTools.UnityPlatformAttribute"/>で指定する例
    /// </summary>
    public class UnityPlatformAttributeExample
    {
        /// <summary>
        /// Unityエディターでのみ実行されるテスト
        /// </summary>
        [Test]
        [UnityPlatform(RuntimePlatform.OSXEditor, RuntimePlatform.WindowsEditor, RuntimePlatform.LinuxEditor)]
        public void RunOnEditor()
        {
            var platform = Application.platform;
            Assert.That(platform.ToString(), Does.EndWith("Editor"));
            // Note: テストコードで `.ToString()` のような実装依存の値を返すメソッドに依存することは推奨しません
        }

        /// <summary>
        /// PC, Mac & Linuxスタンドアロンプレイヤーでのみ実行されるテスト
        /// </summary>
        [Test]
        [UnityPlatform(RuntimePlatform.OSXPlayer, RuntimePlatform.WindowsPlayer, RuntimePlatform.LinuxPlayer)]
        public void RunOnStandalonePC()
        {
            var platform = Application.platform;
            Assert.That(platform.ToString(), Does.EndWith("Player"));
            // Note: テストコードで `.ToString()` のような実装依存の値を返すメソッドに依存することは推奨しません
        }

        /// <summary>
        /// iOSプレイヤーでのみ実行されるテスト
        /// </summary>
        [Test]
        [UnityPlatform(RuntimePlatform.IPhonePlayer)]
        public void RunOnIPhonePlayer()
        {
            var platform = Application.platform;
            Assert.That(platform, Is.EqualTo(RuntimePlatform.IPhonePlayer));
        }

        /// <summary>
        /// Androidプレイヤーでのみ実行されるテスト
        /// </summary>
        [Test]
        [UnityPlatform(RuntimePlatform.Android)]
        public void RunOnAndroid()
        {
            var platform = Application.platform;
            Assert.That(platform, Is.EqualTo(RuntimePlatform.Android));
        }

        /// <summary>
        /// WebGLプレイヤー以外で実行されるテスト（excludeの例）
        /// </summary>
        [Test]
        [UnityPlatform(exclude = new[] { RuntimePlatform.WebGLPlayer })]
        public void RunOnOtherThanWebGLPlayer()
        {
            var platform = Application.platform;
            Assert.That(platform, Is.Not.EqualTo(RuntimePlatform.WebGLPlayer));
        }
    }
}
