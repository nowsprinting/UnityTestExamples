// Copyright (c) 2021-2023 Koji Hasegawa.
// This software is released under the MIT License.

using System.Collections;
using System.Threading.Tasks;
using NUnit.Framework;
using UnityEngine.TestTools;

// ReSharper disable AccessToStaticMemberViaDerivedType

namespace APIExamples.NUnit
{
    /// <summary>
    /// 数値型にでたらめな値を与えるRandom属性の使用例
    /// <see cref="ValuesAttributeExample"/>も参照してください
    /// </summary>
    /// <remarks>
    /// Warning: Test Runnerウィンドウでは実行後、再描画で値が変わるため未実行扱いとなります（偶然数値が一致したときは実行とマークされます）
    /// </remarks>
    [TestFixture]
    public class RandomAttributeExample
    {
        [Test]
        public void Random属性ででたらめな値を与える例(
            [Random(0, 5, 3)] int i,
            [Random(5, 10, 3)] int j)
        {
            Assert.That(i + j, Is.InRange((0 + 5), (4 + 9)));
        }

        [Test]
        public async Task Random属性ででたらめな値を与える例_AsyncTest(
            [Random(0, 5, 3)] int i,
            [Random(5, 10, 3)] int j)
        {
            await Task.Yield();

            Assert.That(i + j, Is.InRange((0 + 5), (4 + 9)));
        }

        [UnityTest]
        public IEnumerator Random属性ででたらめな値を与える例_UnityTest(
            [Random(0, 5, 3)] int i,
            [Random(5, 10, 3)] int j)
        {
            yield return null;

            Assert.That(i + j, Is.InRange((0 + 5), (4 + 9)));
        }
    }
}
