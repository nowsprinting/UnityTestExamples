// Copyright (c) 2021-2023 Koji Hasegawa.
// This software is released under the MIT License.

using System;
using System.Collections;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using UnityEngine;

#pragma warning disable CS1998
// ReSharper disable AccessToStaticMemberViaDerivedType

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
            await Task.Delay(200);
            Debug.Log($"Foo({id})");
        }

        private static async Task<int> Bar(int id)
        {
            await Task.Delay(200);
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
            await UniTask.Delay(200);
            Debug.Log($"UniTaskFoo({id})");
        }

        private static async UniTask<int> UniTaskBar(int id)
        {
            await UniTask.Delay(200);
            Debug.Log($"UniTaskBar({id})");
            return id + 1;
        }

        [Explicit("Edit Modeではテストが終了しないため実行対象から除外/ Freeze in the Edit Mode tests")]
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

        private static async Task ThrowNewExceptionInMethod()
        {
            throw new ArgumentException("message!");
        }

        [Test]
        public async Task 非同期メソッドの例外捕捉を制約モデルで行なう例()
        {
            Assert.That(async () => await ThrowNewExceptionInMethod(),
                Throws.TypeOf<ArgumentException>().And.Message.EqualTo("message!"));
            // Note: 非同期（async）メソッドに対してThrows制約が使用できない問題は、Unity Test Framework v1.3.4で修正された
            //  See: https://unity3d.atlassian.net/servicedesk/customer/portal/2/IN-28107
        }

        [Test]
        public async Task 非同期メソッドの例外捕捉をクラシックモデルで行なう例()
        {
            Assert.ThrowsAsync<ArgumentException>(async () => await ThrowNewExceptionInMethod());
            // Note: クラシックモデルではMessage文字列の評価はできない
        }

        [Test]
        public async Task 非同期メソッドの例外捕捉をTryCatchで行なう例()
        {
            try
            {
                await ThrowNewExceptionInMethod();
                Assert.Fail("例外が出ることを期待しているのでテスト失敗とする");
            }
            catch (ArgumentException expectedException)
            {
                Assert.That(expectedException.Message, Is.EqualTo("message!"));
            }
        }
    }
}
