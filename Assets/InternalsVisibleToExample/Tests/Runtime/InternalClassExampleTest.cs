// Copyright (c) 2021 Koji Hasegawa.
// This software is released under the MIT License.

using NUnit.Framework;

namespace InternalsVisibleToExample
{
    public class InternalClassExampleTest
    {
        [Test]
        public void AlwaysTrue_コールできる()
        {
            var sut = new InternalClassExample();
            Assert.That(sut.AlwaysTrue(), Is.True);
        }
    }
}
