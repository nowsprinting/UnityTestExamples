// Copyright (c) 2021 Koji Hasegawa.
// This software is released under the MIT License.

using NUnit.Framework;
using UnityEngine.TestTools;

namespace APIExamples.UnityTestFramework
{
    /// <summary>
    /// <see cref="UnityEngine.TestTools.PrebuildSetupAttribute"/> で指定した <see cref="UnityEngine.TestTools.IPrebuildSetup"/> 実装クラスの <c>Setup</c> メソッドが使用されます
    /// <list type="bullet">
    ///     <item>複数のテストに <c>PrebuildSetup</c> 属性が配置されていても、<c>Setup</c> の実行は1回だけです</item>
    ///     <item><see cref="UnityEngine.TestTools.IPrebuildSetup"/> を実装していないクラスを渡した場合、何も起きません（エラーにもなりません）</item>
    ///     <item><c>PrebuildSetup</c> 属性は、クラスにもメソッドにも配置できます</item>
    /// </list>
    /// </summary>
    [PrebuildSetup(typeof(PreBuildSetupExample))]
    [PostBuildCleanup(typeof(PreBuildSetupExample))]
    public class PreBuildSetupAttributeExample
    {
        [Test]
        public void PrebuildSetupAttributeを付与したテストの例()
        {
        }
    }
}
