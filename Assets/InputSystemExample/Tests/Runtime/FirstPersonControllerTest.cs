// Copyright (c) 2021-2023 Koji Hasegawa.
// This software is released under the MIT License.

using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.TestTools.Utils;
#if UNITY_EDITOR
using UnityEditor.SceneManagement;
#endif

// ReSharper disable Unity.InefficientPropertyAccess

namespace InputSystemExample
{
    /// <summary>
    /// <c>FirstPersonController</c> の操作・振る舞いのテスト.
    ///
    /// <c>Unity.InputSystem.TestFramework</c> を使用して入力をシミュレートする例。
    /// <see href="https://docs.unity3d.com/Packages/com.unity.inputsystem@1.5/manual/Testing.html"/>
    /// </summary>
    [TestFixture]
    [UnityPlatform(RuntimePlatform.OSXEditor, RuntimePlatform.WindowsEditor, RuntimePlatform.LinuxEditor)]
    public class FirstPersonControllerTest
    {
        private readonly InputTestFixture _input = new InputTestFixture();
        private FirstPersonController _controller;

        [SetUp]
        public async Task SetUp()
        {
            _input.Setup(); // InputTestFixtureを初期化
            // Note: プロダクトコードでInputSystemが初期化されるより前に `InputTestFixture.SetUp` を実行する必要がある
            // Note: `InputTestFixture` を継承する書きかたもあるが、SetUp/TearDownと競合するため選択していない
            // Note: カスタムComposite, Interaction, Processorを使用しているプロジェクトでは、Setupの後に再Registerが必要

#if UNITY_EDITOR
            await EditorSceneManager.LoadSceneAsyncInPlayMode(
                "Assets/InputSystemExample/Tests/Scenes/InputSystemExampleSandbox.unity",
                new LoadSceneParameters(LoadSceneMode.Single));
            // Note: Scenes in Buildに入れていないSceneなので、EditorSceneManagerでロード
#endif
            _controller = Object.FindObjectOfType<FirstPersonController>();
        }

        [TearDown]
        public void TearDown()
        {
            _input.TearDown();
        }

        [Test]
        public async Task Wキーで前方に移動()
        {
            var beforePosition = _controller.transform.position;
            Assume.That(beforePosition, Is.EqualTo(Vector3.zero)
                .Using(new Vector3EqualityComparer(0.1f))); // 初期位置は原点

            var keyboard = InputSystem.AddDevice<Keyboard>(); // デバイス生成・追加
            _input.Press(keyboard.wKey); // Wキーを押す
            await Task.Delay(500); // 0.5秒間保持
            _input.Release(keyboard.wKey); // 離す

            var afterPosition = _controller.transform.position;
            Assert.That(afterPosition, Is.EqualTo(new Vector3(0f, 0f, 4f))
                .Using(new Vector3EqualityComparer(0.3f))); // Z+方向に約4単位移動
        }

        [Test]
        public async Task 左スティックを上に倒すと前方に移動()
        {
            var beforePosition = _controller.transform.position;
            Assume.That(beforePosition, Is.EqualTo(Vector3.zero)
                .Using(new Vector3EqualityComparer(0.1f))); // 初期位置は原点

            var gamepad = InputSystem.AddDevice<Gamepad>();
            _input.Set(gamepad.leftStick, Vector2.up); // 上方向に倒す
            await Task.Delay(500); // 0.5秒間保持
            _input.Set(gamepad.leftStick, Vector2.zero); // 離す

            var afterPosition = _controller.transform.position;
            Assert.That(afterPosition, Is.EqualTo(new Vector3(0f, 0f, 4f))
                .Using(new Vector3EqualityComparer(0.3f))); // Z+方向に約4単位移動
        }

        [Test]
        public async Task 右スティックを右に倒すと右方向を向く()
        {
            var beforeRotation = _controller.transform.rotation;
            Assume.That(beforeRotation, Is.EqualTo(Quaternion.identity)); // 初期向きは正面

            var gamepad = InputSystem.AddDevice<Gamepad>();
            _input.Set(gamepad.rightStick, Vector2.right); // 右方向に倒す
            await Task.Delay(500); // 0.5秒間保持
            _input.Set(gamepad.rightStick, Vector2.zero); // 離す

            var afterRotation = _controller.transform.rotation;
            Assert.That(afterRotation, Is.EqualTo(Quaternion.Euler(0f, 45f, 0f))
                .Using(new QuaternionEqualityComparer(0.05f))); // Y軸で右に約45度回転
        }

        [Test]
        public async Task マウス右移動で右方向を向く()
        {
            var beforeRotation = _controller.transform.rotation;
            Assume.That(beforeRotation, Is.EqualTo(Quaternion.identity)); // 初期向きは正面

            var mouse = InputSystem.AddDevice<Mouse>();
            var endTime = Time.time + 0.5f; // 0.5秒間ループ
            while (Time.time < endTime)
            {
                // Mouse.deltaは毎フレームリセットされるので毎フレームセットし続ける
                _input.Set(mouse.delta, Vector2.right); // 右方向に移動
                await UniTask.NextFrame();
            }

            var afterRotation = _controller.transform.rotation;
            Assert.That(afterRotation, Is.EqualTo(Quaternion.Euler(0f, 45f, 0f))
                .Using(new QuaternionEqualityComparer(0.05f))); // Y軸で右に約45度回転
        }
    }
}
