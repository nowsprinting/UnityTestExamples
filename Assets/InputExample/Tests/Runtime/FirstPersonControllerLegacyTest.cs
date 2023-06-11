// Copyright (c) 2021-2023 Koji Hasegawa.
// This software is released under the MIT License.

using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using InputExample.TestDoubles;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.TestTools.Utils;
using Object = UnityEngine.Object;
#if UNITY_EDITOR
using UnityEditor.SceneManagement;
#endif

// ReSharper disable Unity.InefficientPropertyAccess

namespace InputExample
{
    [TestFixture]
    [UnityPlatform(RuntimePlatform.OSXEditor, RuntimePlatform.WindowsEditor, RuntimePlatform.LinuxEditor)]
    public class FirstPersonControllerLegacyTest
    {
        private const string SandboxScenePath = "Assets/InputExample/Tests/Scenes/InputExampleSandbox.unity";

        private FirstPersonControllerLegacy _controller;

        [SetUp]
        public async Task SetUp()
        {
#if UNITY_EDITOR
            await EditorSceneManager.LoadSceneAsyncInPlayMode(
                SandboxScenePath,
                new LoadSceneParameters(LoadSceneMode.Single));
            // Note: Scenes in Buildに入れていないSceneなので、EditorSceneManagerでロード
#endif
            _controller = Object.FindObjectOfType<FirstPersonControllerLegacy>();
        }

        [Test]
        public async Task Wキーで前方に移動()
        {
            var beforePosition = _controller.transform.position;
            Assume.That(beforePosition, Is.EqualTo(Vector3.zero)
                .Using(new Vector3EqualityComparer(0.1f))); // 初期位置は原点

            var stub = new StubInputKey();
            _controller.Input = stub; // テストスタブを注入

            stub.PushedKeys = new[] { KeyCode.W }; // Wキーを押す
            await Task.Delay(500); // 0.5秒間保持
            stub.PushedKeys = Array.Empty<KeyCode>(); // 離す

            var afterPosition = _controller.transform.position;
            Assert.That(afterPosition, Is.EqualTo(new Vector3(0f, 0f, 4f))
                .Using(new Vector3EqualityComparer(0.3f))); // Z+方向に約4単位移動
        }

        [Test]
        public async Task マウス右移動で右方向を向く()
        {
            var beforeRotation = _controller.transform.rotation;
            Assume.That(beforeRotation, Is.EqualTo(Quaternion.identity)); // 初期向きは正面

            var stub = new StubInputAxis();
            _controller.Input = stub; // テストスタブを注入

            stub.Axes = new[] { new SimulateAxis("Mouse X", 2.0f) }; // 右方向に移動
            await Task.Delay(500); // 0.5秒間保持
            stub.Axes = Array.Empty<SimulateAxis>(); // 止める

            var afterRotation = _controller.transform.rotation;
            Assert.That(afterRotation, Is.EqualTo(Quaternion.Euler(0f, 45f, 0f))
                .Using(new QuaternionEqualityComparer(0.05f))); // Y軸で右に約45度回転
        }
    }
}
