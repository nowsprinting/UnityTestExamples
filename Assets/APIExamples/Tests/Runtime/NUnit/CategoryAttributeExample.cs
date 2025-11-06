// Copyright (c) 2021-2025 Koji Hasegawa.
// This software is released under the MIT License.

using NUnit.Framework;

namespace APIExamples.NUnit
{
    /// <summary>
    /// <see cref="CategoryAttribute"/>の記述例
    /// </summary>
    /// <remarks>
    /// アセンブリへの配置例は、<c>UGUIExample</c> など統合テスト編サンプルの <c>AssemblyInfo.cs</c> を参照してください。
    /// </remarks>
    [TestFixture]
    [Category("CategoryFoo")]
    public class CategoryAttributeExample
    {
        [Test]
        [Category("CategoryBar")]
        public void CategorizedMethod()
        {
        }

        [Test]
        [Category("CategoryBaz")]
        [Category("CategoryQux")]
        public void MultiCategorizedMethodMethod()
        {
        }
    }
}
