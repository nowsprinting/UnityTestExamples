// Copyright (c) 2021-2025 Koji Hasegawa.
// This software is released under the MIT License.

using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace APIExamples.NUnit
{
    /// <summary>
    /// <see cref="SetUpAttribute"/>の使用例
    /// </summary>
    /// <seealso cref="OneTimeSetupAttributeExample"/>
    /// <seealso cref="AsyncSetupAttributeExample"/>
    /// <seealso cref="APIExamples.UnityTestFramework.UnitySetUpAttributeExample"/>
    [TestFixture]
    [SuppressMessage("ReSharper", "AccessToStaticMemberViaDerivedType")]
    public class SetupAttributeExample
    {
        private int _setupCount;

        /// <summary>
        /// 各テストメソッドの前に実行されます
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            _setupCount++;
        }

        [Test, Order(0)]
        public void TestMethod()
        {
            Assert.That(_setupCount, Is.EqualTo(1), "最初のテストなのでSetUpは1回実行されている");
        }

        [Test, Order(1)]
        public async Task TestMethodAsync()
        {
            await Task.Yield();
            Assert.That(_setupCount, Is.EqualTo(2), "2番目のテストなのでSetUpは2回実行されている");
        }

        [UnityTest, Order(2)]
        public IEnumerator UnityTestMethod()
        {
            yield return null;
            Assert.That(_setupCount, Is.EqualTo(3), "3番目のテストなのでSetUpは3回実行されている");
        }
    }
}
