// Copyright (c) 2021-2025 Koji Hasegawa.
// This software is released under the MIT License.

using System.Linq;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using TestHelper.Attributes;
using UnityEditor.PackageManager;

namespace APIExamples.Editor.UnityTestFramework
{
    [TestFixture]
    public class UnityTestFrameworkVersionCheck
    {
        private static async UniTask<string> GetTestFrameworkPackageVersionAsync()
        {
            var request = Client.List(false, false);
            while (!request.IsCompleted)
            {
                await UniTask.Yield();
            }

            Assume.That(request.Status, Is.EqualTo(StatusCode.Success));

            return request.Result.Where(x => x.name == "com.unity.test-framework")
                .Select(x => x.version)
                .FirstOrDefault();
        }

        [Test]
        [UnityVersion(olderThan: "6000.0.44f1")]
        public async Task Unity6000_0_43f1まで_TestFrameworkバージョンは任意()
        {
            var actual = await GetTestFrameworkPackageVersionAsync();
            Assert.That(actual, Is.EqualTo("1.4.6"));
        }

        [Test]
        [UnityVersion(newerThanOrEqual: "6000.0.44f1", olderThan: "6000.0.59f2")]
        public async Task Unity6000_0_44f1から58f2まで_TestFrameworkはv1_5_1固定()
        {
            var actual = await GetTestFrameworkPackageVersionAsync();
            Assert.That(actual, Is.EqualTo("1.5.1"));
        }

        [Test]
        [UnityVersion(newerThanOrEqual: "6000.0.59f2", olderThan: "6000.1.0f1")]
        public async Task Unity6000_0_59f2以降_TestFrameworkはv1_6_0固定()
        {
            var actual = await GetTestFrameworkPackageVersionAsync();
            Assert.That(actual, Is.EqualTo("1.6.0"));
        }

        [Test]
        [UnityVersion(newerThanOrEqual: "6000.1.0f1", olderThan: "6000.2.6f1")]
        public async Task Unity6000_1_0f1から6000_2_5f1まで_TestFrameworkはv1_5_1固定()
        {
            var actual = await GetTestFrameworkPackageVersionAsync();
            Assert.That(actual, Is.EqualTo("1.5.1"));
        }

        [Test]
        [UnityVersion(newerThanOrEqual: "6000.2.6f1")]
        public async Task Unity6000_2_6f1以降_TestFrameworkはv1_6_0固定()
        {
            var actual = await GetTestFrameworkPackageVersionAsync();
            Assert.That(actual, Is.EqualTo("1.6.0"));
        }
    }
}
