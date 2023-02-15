// Copyright (c) 2021-2023 Koji Hasegawa.
// This software is released under the MIT License.

using NUnit.Framework;

// ReSharper disable once CheckNamespace (rootNamespace not work in Unity 2019)
namespace LocalPackageSample
{
    public class ExampleForCoverageTest
    {
        [Test]
        public void Foo_Return1()
        {
            Assert.That(ExampleForCoverage.Foo(), Is.EqualTo(1));
        }
    }
}
