// Copyright (c) 2021 Koji Hasegawa.
// This software is released under the MIT License.

using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace APIExamples.NUnit
{
    /// <summary>
    /// <see cref="SetUpFixtureAttribute"/>によって、名前空間内のすべてのテストの前処理および後処理を定義できる
    /// </summary>
    /// <remarks>
    /// このクラス内に<see cref="SetUpAttribute"/>, <see cref="TearDownAttribute"/>を定義すると実行時エラーになります
    /// このクラス内に<see cref="TestAttribute"/>, <see cref="UnityTestAttribute"/>, <see cref="UnitySetUpAttribute"/>, <see cref="UnityTearDownAttribute"/>を定義しても無視されます
    /// </remarks>
    [SetUpFixture]
    public class SetUpFixtureAttributeExample
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Debug.Log("SetUpFixture.OneTimeSetUp");
        }

        [OneTimeTearDown]
        public void RunAfterAnyTests()
        {
            Debug.Log("SetUpFixture.OneTimeTearDown");
        }
    }
}
