// Copyright (c) 2021 Koji Hasegawa.
// This software is released under the MIT License.

using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace APIExamples.NUnit
{
    /// <summary>
    /// <see cref="RepeatAttribute"/>の使用例
    /// </summary>
    /// <remarks>
    /// Unity Test Frameworkの<see href="https://docs.unity3d.com/Packages/com.unity.test-framework@1.1/manual/index.html#known-limitations">Known limitations</see>に
    ///     The UnityTest attribute does not support the NUnit Repeat attribute.
    /// とありますが、v1.1.27時点では使用できるようになっています。
    /// <seealso href="https://docs.unity3d.com/Packages/com.unity.test-framework@1.1/changelog/CHANGELOG.html">Changelog</seealso>
    /// も参照してください
    /// </remarks>
    public class RepeatAttributeExample
    {
        private int _testCount;
        private int _unityTestCount;

        [Test]
        [Repeat(2)]
        public void 繰り返し実行するテスト()
        {
            _testCount++;
            // Debug.Log($"Test {_testCount}回目");
            Assert.That(true);
        }

        [UnityTest, UnityPlatform(exclude = new[] { RuntimePlatform.WebGLPlayer })]
        [Repeat(2)]
        public IEnumerator 繰り返し実行するテスト_UnityTest属性()
        {
            yield return null;

            _unityTestCount++;
            // Debug.Log($"UnityTest {_unityTestCount}回目");
            Assert.That(true);
        }
    }
}
