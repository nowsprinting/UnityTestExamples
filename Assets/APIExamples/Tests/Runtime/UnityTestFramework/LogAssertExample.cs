// Copyright (c) 2021 Koji Hasegawa.
// This software is released under the MIT License.

using System.Collections;
using System.Text.RegularExpressions;
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
        public void Expect_期待するログメッセージが出力されなければ失敗するテストの例()
        {
            if (Fail)
            {
                LogAssert.Expect(LogType.Log, "expect message");
            }

            Debug.Log("not expected message"); // 通常、テスト結果に影響はない
        }

        [Test]
        public void Expect_期待する警告メッセージが出力されなければ失敗するテストの例()
        {
            if (Fail)
            {
                LogAssert.Expect(LogType.Warning, "expect message");
            }

            Debug.LogWarning("not expected message"); // 通常、テスト結果に影響はない
        }

        [Test]
        public void Expect_期待するメッセージであればLogErrorで出力されてもテストは失敗しない()
        {
            if (!Fail)
            {
                LogAssert.Expect(LogType.Error, "expect message");
            }

            Debug.LogError("expect message"); // 通常、テストは失敗する
        }

        [Test]
        public void Expect_期待するメッセージであればLogErrorで出力されてもテストは失敗しない_正規表現()
        {
            if (!Fail)
            {
                LogAssert.Expect(LogType.Error, new Regex("ex.+? (message|msg)"));
            }

            Debug.LogError("expect message"); // 通常、テストは失敗する
        }

        [UnityTest]
        public IEnumerator Expect_期待するメッセージであればLogErrorで出力されてもテストは失敗しない_Expectは後から呼んでも同フレームであれば有効()
        {
            Debug.LogError("expect message"); // 通常、テストは失敗する

            if (Fail)
            {
                yield return null;
            }

            LogAssert.Expect(LogType.Error, "expect message");
            yield return null;
        }

        [Test]
        public void NoUnexpectedReceived_ログメッセージ出力があるとテストを失敗させる()
        {
            Debug.Log("log message"); // 通常、テスト結果に影響はない

            if (Fail)
            {
                LogAssert.NoUnexpectedReceived(); // TestMustExpectAllLogs属性でも代用できます
            }
        }

        [Test]
        public void NoUnexpectedReceived_警告メッセージ出力があるとテストを失敗させる()
        {
            Debug.LogWarning("warning message"); // 通常、テスト結果に影響はない

            if (Fail)
            {
                LogAssert.NoUnexpectedReceived(); // TestMustExpectAllLogs属性でも代用できます
            }
        }
    }
}
