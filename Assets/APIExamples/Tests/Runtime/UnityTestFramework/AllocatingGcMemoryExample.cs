// Copyright (c) 2021 Koji Hasegawa.
// This software is released under the MIT License.

using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools.Constraints;
using Is = UnityEngine.TestTools.Constraints.Is;

// ReSharper disable NotAccessedVariable
// ReSharper disable ConvertToLocalFunction
// ReSharper disable InconsistentNaming
// ReSharper disable AccessToStaticMemberViaDerivedType

namespace APIExamples.UnityTestFramework
{
    /// <summary>
    /// GCを伴うヒープメモリ確保が行われないかを<see cref="UnityEngine.TestTools.Constraints.ConstraintExtensions.AllocatingGCMemory"/>で検証する例
    /// AllocatingGCMemoryは、Unity 2018.3で追加された
    /// </summary>
    public class AllocatingGcMemoryExample
    {
        [Test]
        public void プリミティブ型_ヒープアロケーションなし()
        {
            TestDelegate SUT = () =>
            {
                var b = true;
                for (var i = 0; i < 10; i++)
                {
                    b = !b;
                }
            };

            Assert.That(SUT, Is.Not.AllocatingGCMemory());
        }

        [Test]
        public void String型_ヒープアロケーションあり()
        {
            TestDelegate SUT = () =>
            {
                var s = "string";
                s += s;
            };

            Assert.That(SUT, Is.AllocatingGCMemory());
        }

        [Test]
        public void DebugLogを使用_ヒープアロケーションあり()
        {
            const string Message = "foo bar";

            TestDelegate SUT = () =>
            {
                Debug.Log(Message);
            };

            Assert.That(SUT, Is.AllocatingGCMemory());
        }

        [Test]
        public void Color構造体_ヒープアロケーションなし()
        {
            TestDelegate SUT = () =>
            {
                var c = new Color(1f, 0f, 0f, 1f);
            };

            Assert.That(SUT, Is.Not.AllocatingGCMemory());
        }
    }
}
