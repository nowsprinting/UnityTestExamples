// Copyright (c) 2021 Koji Hasegawa.
// This software is released under the MIT License.

using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace APIExamples.UnityTestFramework
{
    /// <summary>
    /// <see cref="UnityEngine.TestTools.TestMustExpectAllLogsAttribute"/>の使用例
    /// 通常、テスト実行時に<see cref="UnityEngine.Debug.Log(object)"/>および<see cref="UnityEngine.Debug.LogWarning(object)"/>
    /// ではテストは失敗しませんが、失敗するようにできます。
    /// <see cref="UnityEngine.TestTools.LogAssert.NoUnexpectedReceived"/>でも代用できます。
    /// </summary>
    public class TestMustExpectAllLogsAttributeExample
    {
        private const bool MustExpect = false; // このフラグをtrueにするとこのクラスのテストはすべて失敗します

        [Test]
        [TestMustExpectAllLogs(MustExpect)]
        public void ログメッセージ出力があるときテストを失敗させる()
        {
            Debug.Log("log message"); // 通常、テスト結果に影響はない
        }

        [Test]
        [TestMustExpectAllLogs(MustExpect)]
        public void 警告メッセージ出力があるときテストを失敗させる()
        {
            Debug.LogWarning("warning message"); // 通常、テスト結果に影響はない
        }
    }
}
