// Copyright (c) 2021 Koji Hasegawa.
// This software is released under the MIT License.

using NUnit.Framework;

[assembly: Category("APIExamples")]

namespace APIExamples.NUnit
{
    /// <summary>
    /// <see cref="CategoryAttribute"/>の記述例
    /// </summary>
    [TestFixture]
    [Category("LearningTests")]
    public class CategoryAttributeExample
    {
        [Test]
        [Category("IgnoreCI")]
        public void CategorizedMethodExample()
        {
        }
    }
}
