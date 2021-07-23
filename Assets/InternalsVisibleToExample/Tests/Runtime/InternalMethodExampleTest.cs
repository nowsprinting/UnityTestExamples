// Copyright (c) 2021 Koji Hasegawa.
// This software is released under the MIT License.

using NUnit.Framework;

namespace InternalsVisibleToExample
{
    public class InternalMethodExampleTest
    {
        [Test]
        public void AlwaysTrue_コールできる()
        {
            var sut = new InternalMethodExample();
            Assert.That(sut.AlwaysTrue(), Is.True);
        }
    }
}
