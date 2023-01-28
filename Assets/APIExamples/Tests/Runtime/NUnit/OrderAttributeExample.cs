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
    /// <see cref="OrderAttribute"/>で実行順を指定する例
    /// ただし、基本的に実行順に依存して成否が変わるようなユニットテストは改善の余地があります
    /// </summary>
    [TestFixture]
    public class OrderAttributeExample
    {
        private static int s_count = 0;

        [Test]
        [Order(2)]
        public void OrderedTest4()
        {
            Assert.That(++s_count, Is.EqualTo(2));
        }

        [Test]
        [Order(1)]
        public void OrderedTest3()
        {
            Assert.That(++s_count, Is.EqualTo(1));
        }

        [Test]
        [Order(3)]
        public async Task OrderedTest2()
        {
            Assert.That(++s_count, Is.EqualTo(3));
            await Task.Delay(0);
        }

        [UnityTest]
        [Order(4)]
        public IEnumerator OrderedTest1()
        {
            Assert.That(++s_count, Is.EqualTo(4));
            yield return null;
        }

        [Test]
        public void NonOrderedTest()
        {
            Assert.That(++s_count, Is.EqualTo(5));
        }
    }
}
