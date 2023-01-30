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
    /// 数値型の値を範囲指定するRange属性の使用例
    /// <see cref="ValuesAttributeExample"/>も参照してください
    /// </summary>
    [TestFixture]
    public class RangeAttributeExample
    {
        [Test]
        public void Range属性で数値型に範囲指定する例( // 2x2x2x3=24通り
            [Range(0, 1)] int i,
            [Range(2L, 5L, 3L)] long l,
            [Range(0.6f, 0.7f, 0.1f)] float f,
            [Range(0.08d, 0.09d, 0.005d)] double d)
        {
            Assert.That(d + f + l + i, Is.InRange(0, 7));
        }

        [Test]
        public async Task Range属性で数値型に範囲指定する例_AsyncTest( // 2x2x2x3=24通り
            [Range(0, 1)] int i,
            [Range(2L, 5L, 3L)] long l,
            [Range(0.6f, 0.7f, 0.1f)] float f,
            [Range(0.08d, 0.09d, 0.005d)] double d)
        {
            await Task.Delay(0);

            Assert.That(d + f + l + i, Is.InRange(0, 7));
        }

        [UnityTest]
        public IEnumerator Range属性で数値型に範囲指定する例_UnityTest( // 2x2x2x3=24通り
            [Range(0, 1)] int i,
            [Range(2L, 5L, 3L)] long l,
            [Range(0.6f, 0.7f, 0.1f)] float f,
            [Range(0.08d, 0.09d, 0.005d)] double d)
        {
            yield return null;

            Assert.That(d + f + l + i, Is.InRange(0, 7));
        }
    }
}
