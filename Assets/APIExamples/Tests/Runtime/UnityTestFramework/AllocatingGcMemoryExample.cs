// Copyright (c) 2021 Koji Hasegawa.
// This software is released under the MIT License.

using System.Threading.Tasks;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools.Constraints;
using Is = UnityEngine.TestTools.Constraints.Is;

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

// ReSharper disable NotAccessedVariable
// ReSharper disable ConvertToLocalFunction
// ReSharper disable InconsistentNaming
// ReSharper disable AccessToStaticMemberViaDerivedType
// ReSharper disable MoveLocalFunctionAfterJumpStatement

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
            void UsePrimitives()
            {
                var b = true;
                for (var i = 0; i < 10; i++)
                {
                    b = !b;
                }
            }

            Assert.That(UsePrimitives, Is.Not.AllocatingGCMemory());
        }

        [Test]
        public void String型_ヒープアロケーションあり()
        {
            void UseString()
            {
                var s = "string";
                s += s;
            }

            Assert.That(UseString, Is.AllocatingGCMemory());
        }

        [Test]
        public void DebugLogを使用_ヒープアロケーションあり()
        {
            const string Message = "foo bar"; // 文字列はアロケーションを回避

            void UseLogger()
            {
                Debug.Log(Message);
            }

            Assert.That(UseLogger, Is.AllocatingGCMemory());
        }

        [Test]
        public void Color構造体_ヒープアロケーションなし()
        {
            void UseColor()
            {
                var c = new Color(1f, 0f, 0f, 1f);
            }

            Assert.That(UseColor, Is.Not.AllocatingGCMemory());
        }

        [Explicit("Is.Not.AllocatingGCMemory()をasyncメソッドに使用するとテストが終了しない（Unity Test Framework v1.3.9時点）")]
        [Test]
        public async Task プリミティブ型_ヒープアロケーションなしAsync_テストが終了しない()
        {
            async Task UsePrimitives()
            {
                await Task.Yield();
                var b = true;
                for (var i = 0; i < 10; i++)
                {
                    b = !b;
                }
            }

            Assert.That(async () => await UsePrimitives(), Is.Not.AllocatingGCMemory());
        }

        [Explicit("Is.AllocatingGCMemory()をasyncメソッドに使用しても常に成功してしまう（Unity Test Framework v1.3.9時点）")]
        [Test]
        public async Task String型_ヒープアロケーションありAsync_常に成功してしまう()
        {
            async Task UseString()
            {
                await Task.Yield();
                var s = "string";
                s += s;
            }

            Assert.That(async () => await UseString(), Is.AllocatingGCMemory());
        }
    }
}
