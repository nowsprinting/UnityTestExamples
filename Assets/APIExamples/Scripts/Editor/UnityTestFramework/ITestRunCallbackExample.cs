// Copyright (c) 2021-2025 Koji Hasegawa.
// This software is released under the MIT License.

using APIExamples.Editor.UnityTestFramework;
using NUnit.Framework.Interfaces;
using UnityEngine.TestRunner;

[assembly: TestRunCallback(typeof(ITestRunCallbackExample))]

namespace APIExamples.Editor.UnityTestFramework
{
    /// <summary>
    /// Test RunnerコールバックAPI使用例.
    /// <see href="https://docs.unity3d.com/Packages/com.unity.test-framework@1.6/api/UnityEngine.TestRunner.ITestRunCallback.html"/>
    /// </summary>
    /// <seealso cref="ICallbacksExample"/>
    public class ITestRunCallbackExample : ITestRunCallback
    {
        /// <inheritdoc/>
        public void RunStarted(ITest testsToRun)
        {
            // テスト実行が開始されるときに呼ばれます
        }

        /// <inheritdoc/>
        public void RunFinished(ITestResult testResults)
        {
            // テスト実行が終了したときに呼ばれます
        }

        /// <inheritdoc/>
        public void TestStarted(ITest test)
        {
            // 個々の（Test Runnerウィンドウにおける）ツリーノードが開始されるときに呼ばれます
        }

        /// <inheritdoc/>
        public void TestFinished(ITestResult result)
        {
            // 個々の（Test Runnerウィンドウにおける）ツリーノードが終了したときに呼ばれます
        }
    }
}
