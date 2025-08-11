// Copyright (c) 2021-2025 Koji Hasegawa.
// This software is released under the MIT License.

using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace APIExamples.Editor.UnityTestFramework
{
    /// <summary>
    /// フレームをまたがるテストを<see cref="UnityEngine.TestTools.UnityTestAttribute"/>で実装する例
    /// </summary>
    /// <remarks>
    /// <see cref="APIExamples.UnityTestFramework.UnityTestAttributeExample"/> をベースに、Edit Modeテスト向けに修正したもの
    /// </remarks>
    [TestFixture]
    public class UnityTestAttributeExample
    {
        [UnityTest]
        public IEnumerator YieldReturnNullでフレームを送る例()
        {
            var before = Time.frameCount;
            yield return null;

            var actual = Time.frameCount;

            Assert.That(actual, Is.EqualTo(before)); // Note: Edit Modeテストではそもそもフレームカウントされない
        }

        [UnityTest]
        public IEnumerator YieldBreakの例_テストはパスする()
        {
            yield break;
        }

        [UnityTest]
        [Explicit("Edit Modeテストでは動作しない")]
        public IEnumerator YieldInstructionの実装クラス_EditMoteテストでは動作しない()
        {
            var start = Time.time; // The time at the beginning of this frame
            const float WaitSeconds = 0.5f;
            yield return new WaitForSeconds(WaitSeconds);

            var actual = Time.time - start;

            Assert.That(actual, Is.EqualTo(WaitSeconds).Within(0.1f));
        }

        [UnityTest]
        [Explicit("Edit Modeテストでは動作しない")]
        public IEnumerator CustomYieldInstructionの実装クラス_EditMoteテストでは動作しない()
        {
            var start = Time.time; // The time at the beginning of this frame
            const float WaitSeconds = 0.5f;
            yield return new WaitForSecondsRealtime(WaitSeconds);

            var actual = Time.time - start;

            Assert.That(actual, Is.EqualTo(WaitSeconds).Within(0.1f));
        }

        [UnityTest]
        public IEnumerator AsyncOperationの終了を待つテストの例()
        {
            var resourceRequest = Resources.LoadAsync<TextAsset>("TextExample");
            yield return resourceRequest;
            var actual = resourceRequest.asset as TextAsset;

            Assert.That(actual.text, Is.Not.Null.And.Contain("Editor/Resources"));
        }

        [UnityTest]
        public IEnumerator UniTaskでAsyncメソッドを使用するテストの例() => UniTask.ToCoroutine(async () =>
        {
            var resourceRequest = Resources.LoadAsync<TextAsset>("TextExample");
            await resourceRequest;
            var actual = resourceRequest.asset as TextAsset;

            Assert.That(actual.text, Is.Not.Null.And.Contain("Editor/Resources"));
        });

        [UnityTest]
        public IEnumerator コルーチンを起動してコールバックを受け取る例()
        {
            var actual = 0;

            yield return BarCoroutine(i =>
            {
                actual = i;
            });

            Assert.That(actual, Is.EqualTo(1));
        }

        [UnityTest]
        [Ignore("Edit Modeテストではテストが終了しないため実行対象から除外/ Freeze in the Edit Mode tests")]
        public IEnumerator UniTaskでコルーチンを起動してコールバックを受け取る例_EditModeテストではテストが終了しない() => UniTask.ToCoroutine(async () =>
        {
            var actual = 0;

            await BarCoroutine(i =>
            {
                actual = i;
            }).ToUniTask();

            Assert.That(actual, Is.EqualTo(1));
        });

        private static IEnumerator BarCoroutine(Action<int> onSuccess)
        {
            yield return null;
            onSuccess(1);
        }
    }
}
