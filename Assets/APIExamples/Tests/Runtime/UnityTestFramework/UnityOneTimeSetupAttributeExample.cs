// Copyright (c) 2021-2025 Koji Hasegawa.
// This software is released under the MIT License.

#if ENABLE_UTF_1_5
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using APIExamples.NUnit;
using NUnit.Framework;
using UnityEngine.TestTools;
using Is = APIExamples.NUnit.Is;

namespace APIExamples.UnityTestFramework
{
    /// <summary>
    /// <see cref="UnityOneTimeSetupAttributeExample"/>の使用例
    /// </summary>
    /// <remarks>
    /// Required: Unity Test Framework v1.5 or later
    /// </remarks>
    /// <seealso cref="SetupAttributeExample"/>
    /// <seealso cref="OneTimeSetupAttributeExample"/>
    /// <seealso cref="AsyncSetupAttributeExample"/>
    /// <seealso cref="UnitySetUpAttributeExample"/>
    [TestFixture]
    [SuppressMessage("ReSharper", "AccessToStaticMemberViaDerivedType")]
    public class UnityOneTimeSetupAttributeExample
    {
        private int _oneTimeSetupCount;
        private int _setupCount;

        /// <summary>
        /// テストクラス内の最初のテストの実行前に一度だけ実行されます
        /// </summary>
        [UnityOneTimeSetUp]
        public IEnumerator OneTimeSetUp()
        {
            yield return null;
            _oneTimeSetupCount++;
        }

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
            Assert.That(_oneTimeSetupCount, Is.EqualTo(1), "OneTimeSetUpはTestFixtureごとに一度だけ実行される");
            Assert.That(_setupCount, Is.EqualTo(1), "最初のテストなのでSetUpは1回実行されている");
        }

        [Test, Order(1)]
        public async Task TestMethodAsync()
        {
            await Task.Yield();
            Assert.That(_oneTimeSetupCount, Is.EqualTo(1), "OneTimeSetUpはTestFixtureごとに一度だけ実行される");
            Assert.That(_setupCount, Is.EqualTo(2), "2番目のテストなのでSetUpは2回実行されている");
        }

        [UnityTest, Order(2)]
        public IEnumerator UnityTestMethod()
        {
            yield return null;
            Assert.That(_oneTimeSetupCount, Is.EqualTo(1), "OneTimeSetUpはTestFixtureごとに一度だけ実行される");
            Assert.That(_setupCount, Is.EqualTo(3), "3番目のテストなのでSetUpは3回実行されている");
        }
    }
}
#endif
