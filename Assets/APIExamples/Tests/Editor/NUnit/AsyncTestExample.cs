// Copyright (c) 2022-2023 Koji Hasegawa.
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
            Debug.Log($"Start Foo({id})");
            await Task.Delay(200); // Note: Edit Modeテストでは実際はDelayはされない
            Debug.Log($"Exit Foo({id})");
        }

        private static async Task<int> Bar(int id)
        {
            Debug.Log($"Start Bar({id})");
            await Task.Delay(200); // Note: Edit Modeテストでは実際はDelayはされない
            Debug.Log($"Exit Bar({id})");
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
            Debug.Log($"Start UniTaskFoo({id})");
            await UniTask.Delay(200); // Note: Edit Modeテストでは実際はDelayはされない
            Debug.Log($"Exit UniTaskFoo({id})");
        }

        private static async UniTask<int> UniTaskBar(int id)
        {
            Debug.Log($"Start UniTaskBar({id})");
            await UniTask.Delay(200); // Note: Edit Modeテストでは実際はDelayはされない
            Debug.Log($"Exit UniTaskBar({id})");
            return id + 1;
        }

        [Test]
        [Description("Can await Coroutine")]
        [Explicit("Edit Modeテストではフリーズするため実行対象から除外/ Freeze in the Edit Mode tests")]
        public async Task 非同期テストの例_コルーチンをawait_EditModeテストでは動作しない()
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
            yield return null;
            onSuccess(1);
        }
    }
}
