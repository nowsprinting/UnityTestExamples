// Copyright (c) 2021-2025 Koji Hasegawa.
// This software is released under the MIT License.

using UnityEditor;
using UnityEditor.TestTools.TestRunner.Api;
using UnityEngine;

namespace APIExamples.Editor.UnityTestFramework
{
    /// <summary>
    /// Test RunnerコールバックAPI使用例.
    /// <see href="https://docs.unity3d.com/Packages/com.unity.test-framework@1.6/api/UnityEditor.TestTools.TestRunner.Api.IErrorCallbacks.html"/>
    /// </summary>
    /// <remarks>
    /// `OnError()`を除いて、<see cref="UnityEditor.TestTools.TestRunner.Api.ICallbacks"/>に定義されている
    /// `OnError()`は、<see cref="UnityEditor.TestTools.TestRunner.Api.IErrorCallbacks"/>にのみ定義されている
    /// </remarks>
    /// <seealso cref="ITestRunCallbackExample"/>
    public class ICallbacksExample : IErrorCallbacks
    {
        [InitializeOnLoadMethod]
        private static void SetupCallbacks()
        {
            var api = ScriptableObject.CreateInstance<TestRunnerApi>();
            api.RegisterCallbacks(new ICallbacksExample());
        }

        /// <inheritdoc/>
        public void RunStarted(ITestAdaptor testsToRun)
        {
            // テスト実行が開始されるときに呼ばれます
        }

        /// <inheritdoc/>
        public void RunFinished(ITestResultAdaptor result)
        {
            // テスト実行が終了したときに呼ばれます
        }

        /// <inheritdoc/>
        public void TestStarted(ITestAdaptor test)
        {
            // 個々の（Test Runnerウィンドウにおける）ツリーノードが開始されるときに呼ばれます
        }

        /// <inheritdoc/>
        public void TestFinished(ITestResultAdaptor result)
        {
            // 個々の（Test Runnerウィンドウにおける）ツリーノードが終了したときに呼ばれます
        }

        /// <inheritdoc/>
        public void OnError(string message)
        {
            // エラー発生時に呼ばれます（テストの失敗では呼ばれません）
        }
    }
}
