// Copyright (c) 2021-2025 Koji Hasegawa.
// This software is released under the MIT License.

#if ENABLE_UTF_1_6
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace APIExamples.UnityTestFramework
{
    /// <summary>
    /// ビルド前後に処理を挿むための <see cref="UnityEngine.TestTools.IPrebuildSetupWithTestData"/> および <see cref="UnityEngine.TestTools.IPostbuildCleanupWithTestData"/> の実装例
    /// </summary>
    /// <remarks>
    /// Required: Unity Test Framework v1.6 or later
    /// </remarks>
    /// <seealso cref="PreBuildSetupExample"/>
    public class PreBuildSetupWithTestDataExample : IPrebuildSetupWithTestData, IPostbuildCleanupWithTestData
    {
        ///<inheritdoc/>
        /// <remarks>
        /// Unityエディター実行（Edit ModeテストおよびPlay Modeテスト）では、すべてのテストの実行に先立って実行されます。
        /// プレイヤー実行（Play Modeテスト）では、ビルド前に実行されます。
        /// <p/>
        /// <see cref="TestData"/> には、テストモード、テストプラットフォーム、実行されるテストのリストが含まれます
        /// </remarks>
        public void Setup(TestData testData)
        {
            Debug.Log($"PreBuildSetupWithTestDataExample.Setup; testData: {testData}");
        }

        ///<inheritdoc/>
        /// <remarks>
        /// Unityエディター実行（Edit ModeテストおよびPlay Modeテスト）では、すべてのテストの実行終了後に実行されます。
        /// プレイヤー実行（Play Modeテスト）では、ビルド後に実行されます。
        /// <p/>
        /// <see cref="TestData"/> には、テストモード、テストプラットフォーム、実行されるテストのリストが含まれます
        /// </remarks>
        public void Cleanup(TestData testData)
        {
            Debug.Log($"PreBuildSetupWithTestDataExample.Cleanup; testData: {testData}");
        }

        [Test]
        public void IPrebuildSetupWithTestDataを実装したテストの例()
        {
            Assert.That(true, Is.True);
        }
    }
}
#endif
