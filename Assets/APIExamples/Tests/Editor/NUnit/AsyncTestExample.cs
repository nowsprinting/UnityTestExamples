// Copyright (c) 2021-2025 Koji Hasegawa.
// This software is released under the MIT License.

using System;
using System.Collections;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using UnityEngine;

namespace APIExamples.Editor.NUnit
{
    /// <summary>
    /// 非同期テストの実装例（Edit Modeテスト）
    /// Async test example (in Edit Mode tests)
    /// </summary>
    /// <remarks>
    /// Required: Unity Test Framework v1.3 or later
    /// </remarks>
    [TestFixture]
    public class AsyncTestExample
    {
        [Test]
        [Description("Can await Task")]
        public async Task 非同期テストの例_Taskをawaitできる()
        {
            await Foo(1);
            var actual = await Bar(2);

            Assert.That(actual, Is.EqualTo(3));
        }

        private static async Task Foo(int id)
        {
            await Task.Yield();
            Debug.Log($"Foo({id})");
        }

        private static async Task<int> Bar(int id)
        {
            await Task.Yield();
            Debug.Log($"Bar({id})");
            return id + 1;
        }

        [Test]
        [Description("Can await UniTask")]
        public async Task 非同期テストの例_UniTaskをawaitできる()
        {
            await UniTaskFoo(1);
            var actual = await UniTaskBar(2);

            Assert.That(actual, Is.EqualTo(3));
        }

        private static async UniTask UniTaskFoo(int id)
        {
            await Task.Yield();
            Debug.Log($"UniTaskFoo({id})");
        }

        private static async UniTask<int> UniTaskBar(int id)
        {
            await Task.Yield();
            Debug.Log($"UniTaskBar({id})");
            return id + 1;
        }

        [Ignore("Edit Modeではテストが終了しないため実行対象から除外/ Freeze in the Edit Mode tests")]
        [Test]
        [Description("Can not await coroutine in the Edit Mode tests")]
        public async Task 非同期テストの例_コルーチンをawait_EditModeではテストが終了しない()
        {
            var actual = 0;
            await BarCoroutine(i =>
            {
                actual = i;
            });

            Assert.That(actual, Is.EqualTo(1));
        }

        private static IEnumerator BarCoroutine(Action<int> onSuccess)
        {
            yield return null; // Edit ModeテストではWaitForSecondsなどは使用できない
            onSuccess(1);
        }
    }
}
