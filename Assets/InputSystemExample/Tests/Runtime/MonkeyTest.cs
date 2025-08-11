// Copyright (c) 2021-2025 Koji Hasegawa.
// This software is released under the MIT License.

using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using TestHelper.Attributes;
using TestHelper.Random;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace InputSystemExample
{
    /// <summary>
    /// <c>Unity.InputSystem.TestFramework</c> を使用したモンキーテスト.
    /// 一定時間でたらめな操作をします。
    /// テスト失敗と判断されるのは次の2パターン
    /// - ログにエラー（プロダクトコードに仕込んだ <c>UnityEngine.Assertions.Assert</c> を含む）が出力されたとき
    /// - 一定時間プレイヤーキャラクターが移動しないとき
    /// </summary>
    [TestFixture]
    [FocusGameView] // Note: 安定化のため、Gameビューにフォーカスを当てる（バッチモード実行ではGameビューを開く）
    public class MonkeyTest
    {
        private readonly InputTestFixture _input = new InputTestFixture();

        [SetUp]
        public void SetUp()
        {
            _input.Setup(); // InputTestFixtureを初期化
            // Note: プロダクトコードでInputSystemが初期化されるより前に `InputTestFixture.SetUp` を実行する必要がある
            // Note: `InputTestFixture` を継承する書きかたもあるが、SetUp/TearDownと競合するため選択していない
            // Note: カスタムComposite, Interaction, Processorを使用しているプロジェクトでは、Setupの後に再Registerが必要
        }

        [TearDown]
        public void TearDown()
        {
            _input.TearDown();
        }

        [Test]
        public async Task MonkeyTesting()
        {
            var random = new RandomWrapper();                // 擬似乱数生成器
            Debug.Log($"Random that monkey uses: {random}"); // シード値をログ出力（再現可能にするため）

            await SceneManager.LoadSceneAsync("InputSystemExample");
            // Note: ランダム要素のあるSceneの場合、擬似乱数シードにrandom.Next()を使用すると再現に必要なシード値が1つで済んで便利です

            var controller = Object.FindObjectOfType<FirstPersonController>();
            var lastLocation = controller.transform.position;
            var dontMoveCount = 0;

            using (var tokenSource = new CancellationTokenSource())
            {
                PressKeys(new RandomWrapper(random.Next()), tokenSource.Token).Forget();
                MoveMouse(new RandomWrapper(random.Next()), tokenSource.Token).Forget();

                var expireTime = Time.time + 10.0f; // 10秒間動作させる（3分以上にする場合はTimeout属性でタイムアウト時間を延長）
                while (Time.time < expireTime)
                {
                    var nowLocation = controller.transform.position;
                    if (nowLocation == lastLocation)
                    {
                        if (++dontMoveCount > 80)
                        {
                            Assert.Fail("一定時間プレイヤーキャラクターが移動していない");
                        }
                    }
                    else
                    {
                        lastLocation = nowLocation;
                        dontMoveCount = 0;
                    }

                    await UniTask.NextFrame();
                }

                tokenSource.Cancel();

                Assert.That(lastLocation.y, Is.GreaterThan(0f)); // 落下していないこと
            }
        }

        [SuppressMessage("ReSharper", "FunctionNeverReturns")]
        private async UniTask PressKeys(IRandom random, CancellationToken token)
        {
            var keyboard = InputSystem.AddDevice<Keyboard>();
            var keys = new[] { keyboard.wKey, keyboard.aKey, keyboard.sKey, keyboard.dKey, keyboard.spaceKey }; // 抽選対象

            while (true)
            {
                var key = keys[random.Next(keys.Length)]; // 操作するキーを抽選
                var pressOrRelease = random.Next(2) == 0; // 押すか離すかを抽選
                if (pressOrRelease)
                {
                    _input.Press(key); // 押す
                }
                else
                {
                    _input.Release(key); // 離す
                }

                await UniTask.DelayFrame(random.Next(10), cancellationToken: token);
            }
        }

        [SuppressMessage("ReSharper", "FunctionNeverReturns")]
        private async UniTask MoveMouse(IRandom random, CancellationToken token)
        {
            var mouse = InputSystem.AddDevice<Mouse>();

            while (true)
            {
                var mouseDelta = random.NextNormalizedVector2(); // マウスの移動量を抽選
                _input.Set(mouse.delta, mouseDelta);             // マウス移動

                await UniTask.NextFrame(cancellationToken: token);
            }
        }
    }
}
