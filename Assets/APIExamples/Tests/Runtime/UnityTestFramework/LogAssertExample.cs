// Copyright (c) 2021 Koji Hasegawa.
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
    /// <see cref="UnityEngine.TestTools.LogAssert"/>の使用例
    /// 通常、テスト実行時に<see cref="UnityEngine.Debug.Log(object)"/>および<see cref="UnityEngine.Debug.LogWarning(object)"/>
    /// 以外のメッセージが出力されるとテストは失敗しますが、その条件をコントロールできます。
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
        public async Task Expect_非同期テストではYieldを挟まなければ有効_AsyncTest()
        {
            Debug.LogError("expected message"); // 通常、テストは失敗する

            if (Fail)
            {
                await Task.Yield();
            }

            LogAssert.Expect(LogType.Error, "expected message");
        }

        [UnityTest]
        public IEnumerator Expect_非同期テストではYieldを挟まなければ有効_UnityTest()
        {
            Debug.LogError("expected message"); // 通常、テストは失敗する

            if (Fail)
            {
                yield return null;
            }

            LogAssert.Expect(LogType.Error, "expected message");
        }

        [Test]
        public void NoUnexpectedReceived_ログメッセージ出力があるとテストを失敗させる()
        {
            if (Fail)
            {
                Debug.Log("not error message"); // 通常、Log/LogWarningはテスト結果に影響しない
            }

            LogAssert.NoUnexpectedReceived();
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
