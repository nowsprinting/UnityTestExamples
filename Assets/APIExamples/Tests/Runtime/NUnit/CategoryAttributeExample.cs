// Copyright (c) 2021-2022 Koji Hasegawa.
// This software is released under the MIT License.

using NUnit.Framework;

namespace APIExamples.NUnit
{
    /// <summary>
    /// <see cref="CategoryAttribute"/>の記述例
    /// </summary>
    [TestFixture]
    [Category("CategoryExample")]
    public class CategoryAttributeExample
    {
        [Test]
        [Category("LearningTests")]
        public void CategorizedMethodExample()
        {
        }

        [Test]
        [Category("LearningTests")]
        [Category("IgnoreCI")]
        public void MultiCategorizedMethodExample()
        {
        }
    }
}
