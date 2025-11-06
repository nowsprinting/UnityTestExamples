// Copyright (c) 2021-2023 Koji Hasegawa.
// This software is released under the MIT License.

using System.Collections;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

// ReSharper disable ConditionIsAlwaysTrueOrFalse
#pragma warning disable 162

namespace APIExamples.UnityTestFramework
{
    /// <summary>
    /// <see cref="UnityEngine.TestTools.LogAssert"/>の使用例.
    /// 通常、テスト実行時に<see cref="UnityEngine.Debug.Log(object)"/>および<see cref="UnityEngine.Debug.LogWarning(object)"/>
    /// 以外のメッセージが出力されるとテストは失敗しますが、その条件をコントロールできます。
    /// <c>LogType</c>を省略できるオーバーロードは Unity Test Framework v1.4 で追加されました。
    /// </summary>
    public class LogAssertExample
    {
        private const bool Fail = false; // このフラグをtrueにするとこのクラスのテストはすべて失敗します

        [Test]
        public void Expect_期待するログメッセージが出力されること()
        {
            if (!Fail)
            {
                Debug.Log("expected message");
            }

            LogAssert.Expect(LogType.Log, "expected message");
        }

        [Test]
        public void Expect_期待する警告メッセージが出力されること()
        {
            if (!Fail)
            {
                Debug.LogWarning("expected message");
            }

            LogAssert.Expect(LogType.Warning, "expected message");
        }

        [Test]
        public void Expect_期待するエラーログが出力されること_期待するメッセージであればLogErrorでもテストは失敗しない()
        {
            Debug.LogError("expected message"); // 通常、LogError出力があるとテストは失敗する

            if (!Fail)
            {
                LogAssert.Expect(LogType.Error, "expected message");
            }
        }

        [Test]
        public void Expect_正規表現でマッチング可能()
        {
            Debug.LogError("expected message"); // 通常、LogError出力があるとテストは失敗する

            if (!Fail)
            {
                LogAssert.Expect(LogType.Error, new Regex("ex.+? (message|msg)"));
            }
        }

        [Test]
        public void Expect_同期テストでは記述順序は結果に影響しない()
        {
            if (!Fail)
            {
                LogAssert.Expect(LogType.Error, "expected message"); // 先に記述しても有効
            }

            Debug.LogError("expected message");
        }

        [Test]
        public async Task Expect_非同期テストで先に記述_Yieldを挟んでも同一フレームなら有効_AsyncTest()
        {
            LogAssert.Expect(LogType.Error, "expected message");

            await Task.Yield();
            Debug.LogError("expected message"); // Yieldを挟んでもLogAssert.Expectは有効

            if (Fail)
            {
                await Task.Yield();
                Debug.LogError("expected message"); // フレームが進むとLogAssert.Expectは無効
            }
        }

        [UnityTest]
        public IEnumerator Expect_非同期テストで先に記述_Yieldを挟んでも同一フレームなら有効_UnityTest()
        {
            LogAssert.Expect(LogType.Error, "expected message");

            yield return null;
            Debug.LogError("expected message"); // Yieldを挟んでもLogAssert.Expectは有効

            if (Fail)
            {
                var expectFrame = Time.frameCount;
                yield return new WaitWhile(() => expectFrame == Time.frameCount);
                Debug.LogError("expected message"); // フレームが進むとLogAssert.Expectは無効
            }
        }

        [Test]
        public async Task Expect_非同期テストで後に記述_Yieldを挟まなければ有効_AsyncTest()
        {
            Debug.LogError("expected message");
            if (Fail)
            {
                await Task.Yield(); // Yieldを挟むとこの時点で失敗と判定される
            }

            LogAssert.Expect(LogType.Error, "expected message");
        }

        [UnityTest]
        public IEnumerator Expect_非同期テストで後に記述_Yieldを挟まなければ有効_UnityTest()
        {
            Debug.LogError("expected message");
            if (Fail)
            {
                yield return null; // Yieldを挟むとこの時点で失敗と判定される
            }

            LogAssert.Expect(LogType.Error, "expected message");
        }

        [Test]
        public async Task Expect_非同期テストでもログメッセージは複数フレーム有効_AsyncTest()
        {
            Debug.Log("expected message");

            await Task.Yield();
            LogAssert.Expect(LogType.Log, "expected message");
        }

        [UnityTest]
        public IEnumerator Expect_非同期テストでもログメッセージは複数フレーム有効_UnityTest()
        {
            Debug.Log("expected message");

            var expectFrame = Time.frameCount;
            yield return new WaitWhile(() => expectFrame == Time.frameCount);
            LogAssert.Expect(LogType.Log, "expected message");
        }

        [Test]
        public void ExpectWithoutLogType_期待するログメッセージが出力されること()
        {
            if (!Fail)
            {
                Debug.LogWarning("expected message");
            }

            LogAssert.Expect("expected message");
        }

        [Test]
        public void ExpectWithoutLogType_期待する警告メッセージが出力されること()
        {
            if (!Fail)
            {
                Debug.Log("expected message");
            }

            LogAssert.Expect("expected message");
        }

        [Test]
        public void ExpectWithoutLogType_期待するエラーログが出力されること_期待するメッセージであればLogErrorでもテストは失敗しない()
        {
            Debug.LogError("expected message"); // 通常、LogError出力があるとテストは失敗する

            if (!Fail)
            {
                LogAssert.Expect("expected message");
            }
        }

        [Test]
        public void ExpectWithoutLogType_正規表現でマッチング可能()
        {
            Debug.LogError("expected message"); // 通常、LogError出力があるとテストは失敗する

            if (!Fail)
            {
                LogAssert.Expect(new Regex("ex.+? (message|msg)"));
            }
        }

        [Test]
        public void NoUnexpectedReceived_記述より前に何らかのログメッセージ出力があるとテストを失敗させる()
        {
            if (Fail)
            {
                Debug.Log("not error message"); // 通常、Log/LogWarningはテスト結果に影響しない
            }

            LogAssert.NoUnexpectedReceived();

            Debug.Log("After NoUnexpectedReceived"); // LogAssert.NoUnexpectedReceived()後のログ出力は結果に影響しない
        }

        [Test]
        [TestMustExpectAllLogs]
        public void TestMustExpectAllLogs属性_ログメッセージ出力があるとテストを失敗させる()
        {
            if (Fail)
            {
                Debug.Log("not error message"); // 通常、Log/LogWarningはテスト結果に影響しない
            }
        }
    }
}
