// Copyright (c) 2021 Koji Hasegawa.
// This software is released under the MIT License.

using NUnit.Framework;

namespace APIExamples.NUnit
{
    /// <summary>
    /// <see cref="ExplicitAttribute"/>の記述例
    /// </summary>
    [TestFixture]
    [Explicit("ExplicitAttributeで実行対象から外しています")]
    public class ExplicitAttributeExample
    {
        [Test]
        [Explicit("ExplicitAttributeで実行対象から外しています")]
        public void ExplicitMethodExample()
        {
            Assert.Fail();
        }
    }
}
