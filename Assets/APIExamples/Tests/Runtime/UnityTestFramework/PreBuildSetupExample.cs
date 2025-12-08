// Copyright (c) 2021 Koji Hasegawa.
// This software is released under the MIT License.

using NUnit.Framework;
using UnityEngine.TestTools;

namespace APIExamples.UnityTestFramework
{
    public class PreBuildSetupExample : IPrebuildSetup, IPostBuildCleanup
    {
        ///<inheritdoc/>
        public void Setup()
        {
            // Edit ModeテストおよびPlay Modeテスト（Unityエディター実行）では、すべてのテストの実行に先立って実行される
            // Play Modeテスト（プレイヤー実行）では、ビルド前に実行される
            // テスト用のビルドにのみResourcesに含めたいアセットをコピーする使用例は `LoadAssetExample` を参照してください
        }

        ///<inheritdoc/>
        public void Cleanup()
        {
            // Edit ModeテストおよびPlay Modeテスト（Unityエディター実行）では、すべてのテストの実行終了後に実行される
            // Play Modeテスト（プレイヤー実行）では、ビルド後に実行される
        }

        [Test]
        public void IPrebuildSetupを実装したテストの例()
        {
        }
    }
}
