// Copyright (c) 2021 Koji Hasegawa.
// This software is released under the MIT License.

using NUnit.Framework;

namespace APIExamples.NUnit
{
    /// <summary>
    /// <see cref="IgnoreAttribute"/>の記述例
    /// </summary>
    [Explicit("IgnoreAttributeで実行対象から外しています")]
    [TestFixture]
    public class IgnoreAttributeExample
    {
        [Test]
        [Explicit("IgnoreAttributeで実行対象から外しています")]
        public void IgnoreMethodExample()
        {
            Assert.Fail();
        }
    }
}
