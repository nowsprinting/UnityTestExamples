// Copyright (c) 2021 Koji Hasegawa.
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
    /// <see href="https://github.com/Cysharp/UniTask">UniTask</see>の使用例を含みます
    /// </summary>
    /// <remarks>
    /// Runtime/UnityTestFramework/UnityTestAttributeExampleをベースに、Edit Modeで動くもの
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
        [Explicit("Edit ModeテストではWaitForEndOfFrameがエラーとなるため実行対象から除外")]
        public IEnumerator YieldInstructionの実装クラスはEditMoteテストでは使用できない()
        {
            var before = Time.frameCount;
            yield return new WaitForEndOfFrame();
            var actual = Time.frameCount;

            Assert.That(actual, Is.EqualTo(before));
        }

        [UnityTest]
        [Explicit("Edit ModeテストではWaitForSecondsRealtimeが動作しないため実行対象から除外")]
        public IEnumerator CustomYieldInstructionの実装クラスはEditMoteテストでは使用できない()
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
            yield return FooMonoBehaviour.BarCoroutine(i =>
            {
                actual = i;
            });

            Assert.That(actual, Is.EqualTo(1));
        }

        [UnityTest]
        [Explicit("Edit Modeテストではフリーズするため実行対象から除外")]
        public IEnumerator UniTaskでコルーチンを起動してコールバックを受け取る例() => UniTask.ToCoroutine(async () =>
        {
            var actual = 0;
            await FooMonoBehaviour.BarCoroutine(i =>
            {
                actual = i;
            }).ToUniTask();

            Assert.That(actual, Is.EqualTo(1));
        });

        // テスト対象コルーチンを含むMonoBehaviour
        private class FooMonoBehaviour : MonoBehaviour
        {
            public static IEnumerator BarCoroutine(Action<int> onSuccess)
            {
                yield return null;
                onSuccess(1);
            }
        }
    }
}
