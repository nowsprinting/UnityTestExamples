// Copyright (c) 2021-2025 Koji Hasegawa.
// This software is released under the MIT License.

#if ENABLE_UTF_1_6
using NUnit.Framework;
using UnityEngine.TestTools;

namespace APIExamples.UnityTestFramework
{
    /// <summary>
    /// <see cref="UnityEngine.TestTools.PrebuildSetupWithTestDataAttribute"/> で指定した <see cref="UnityEngine.TestTools.IPrebuildSetupWithTestData"/> 実装クラスの <c>Setup</c> メソッドが使用されます
    /// </summary>
    /// <remarks>
    /// Required: Unity Test Framework v1.6 or later
    /// </remarks>
    /// <seealso cref="PreBuildSetupAttributeExample"/>
    [PrebuildSetupWithTestData(typeof(PreBuildSetupWithTestDataExample))]
    [PostBuildCleanupWithTestData(typeof(PreBuildSetupWithTestDataExample))]
    public class PreBuildSetupWithTestDataAttributeExample
    {
        [Test]
        public void PrebuildSetupWithTestDataAttributeを付与したテストの例()
        {
            Assert.That(true, Is.True);
        }
    }
}
#endif
