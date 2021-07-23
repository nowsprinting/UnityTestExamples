// Copyright (c) 2021 Koji Hasegawa.
// This software is released under the MIT License.

using NUnit.Framework;
using UnityEngine.TestTools;

namespace APIExamples.UnityTestFramework
{
    /// <summary>
    /// <see cref="UnityEngine.TestTools.PrebuildSetupAttribute"/>で指定した<see cref="UnityEngine.TestTools.IPrebuildSetup"/>実装クラスの`Setup()`メソッドが使用されます
    /// </summary>
    /// <remarks>
    /// - 複数のテストに属性が付与されていた場合、`Setup()`の実行は1回だけです
    /// - <see cref="UnityEngine.TestTools.IPrebuildSetup"/>を実装していないクラスを渡した場合、何も起きません（エラーにもなりません）
    /// - 属性は、クラスにもメソッドにも付与できます
    /// </remarks>
    [PrebuildSetup(typeof(PreBuildSetupExample))]
    [PostBuildCleanup(typeof(PreBuildSetupExample))]
    public class PreBuildSetupAttributeExample
    {
        [Test]
        public void PrebuildSetupAttributeを付与したテストの例()
        {
            Assert.That(true, Is.True);
        }
    }
}
