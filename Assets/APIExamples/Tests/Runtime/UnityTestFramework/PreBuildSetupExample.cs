// Copyright (c) 2021 Koji Hasegawa.
// This software is released under the MIT License.

using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace APIExamples.UnityTestFramework
{
    /// <summary>
    /// ビルド前後に処理を挿むための <see cref="UnityEngine.TestTools.IPrebuildSetup"/> および <see cref="UnityEngine.TestTools.IPostBuildCleanup"/> の実装例
    /// </summary>
    public class PreBuildSetupExample : IPrebuildSetup, IPostBuildCleanup
    {
        ///<inheritdoc/>
        /// <remarks>
        /// Unityエディター実行（Edit ModeテストおよびPlay Modeテスト）では、すべてのテストの実行に先立って実行されます。
        /// プレイヤー実行（Play Modeテスト）では、ビルド前に実行されます。
        /// </remarks>
        public void Setup()
        {
            Debug.Log("PreBuildSetupExample.Setup");
        }

        ///<inheritdoc/>
        /// <remarks>
        /// Unityエディター実行（Edit ModeテストおよびPlay Modeテスト）では、すべてのテストの実行終了後に実行されます。
        /// プレイヤー実行（Play Modeテスト）では、ビルド後に実行されます。
        /// </remarks>
        public void Cleanup()
        {
            Debug.Log("PreBuildSetupExample.Cleanup");
        }

        [Test]
        public void IPrebuildSetupを実装したテストの例()
        {
        }
    }
}
