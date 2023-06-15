// Copyright (c) 2021-2023 Koji Hasegawa.
// This software is released under the MIT License.

using System.IO;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using TestHelper.Attributes;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace InputSystemExample
{
    /// <summary>
    /// <c>InputEventTrace</c> および <c>ReplayController</c> を使用したシナリオテスト.
    ///
    /// 操作再生のコードは、Input Systemパッケージの *Input Recorder* サンプルを参考にしています。
    /// またレコードデータは *Input Recorder* で記録したものです。
    /// 記録時は *Record Frames* のみonにしています。
    /// </summary>
    /// <remarks>
    /// Linuxでの再生時、以下のエラーが発生するため、実行対象から除外しています。詳細は未調査。
    /// `Unhandled log message: '[Exception] ArgumentException: State format KEYS from event does not match state format MOUS of device Mouse:/Mouse Parameter name: eventPtr'. Use UnityEngine.TestTools.LogAssert.Expect`
    /// </remarks>
    [TestFixture]
    [UnityPlatform(RuntimePlatform.OSXEditor, RuntimePlatform.WindowsEditor)]
    [FocusGameView] // Note: キーボード・マウス操作の再生のため、Gameビューにフォーカスを当てる（バッチモード実行ではGameビューを開く）
    public class ScenarioTest
    {
        private const string InputTracesPath = "Assets/InputSystemExample/Tests/InputTraces";

        [TestCase("Keyboard.inputtrace")]
        public async Task PlaybackTesting_ゴールに到達すること(string path)
        {
            // フレームレートをキャプチャ環境に合わせる（Updateで入力を処理しているとき必要）
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 60;

            await SceneManager.LoadSceneAsync("InputSystemExample");
            // Note: ランダム要素のあるSceneの場合、ここで擬似乱数シード値を固定する

            using (var eventTrace = new InputEventTrace())
            {
                eventTrace.ReadFrom(Path.GetFullPath(Path.Combine(InputTracesPath, path)));

                var isFinished = false;

                using (var replayController = eventTrace.Replay()
                           .OnFinished(() => { isFinished = true; }) // 再生終了したらフラグを立てる
                           .PlayAllEventsAccordingToTimestamps())
                {
                    // Note: スペックの異なるマシンで再生する場合、PlayAllEventsAccordingToTimestampsを使用するほうが安定します。
                    // 同一マシンでキャプチャ・プレイバックするのであれば、フレームに忠実に再生するPlayAllFramesOneByOneでも再生できます。

                    while (!isFinished) // 終了フラグ待ち
                    {
                        await UniTask.NextFrame();
                    }
                }
            }

            var goalPlate = Object.FindObjectOfType<GoalPlate>();
            Assert.That(goalPlate.IsGoal, Is.True);
        }
    }
}
