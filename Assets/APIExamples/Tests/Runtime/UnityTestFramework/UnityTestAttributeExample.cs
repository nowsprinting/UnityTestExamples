// Copyright (c) 2021 Koji Hasegawa.
// This software is released under the MIT License.

using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace APIExamples.UnityTestFramework
{
    /// <summary>
    /// フレームをまたがるテストを<see cref="UnityEngine.TestTools.UnityTestAttribute"/>で実装する例
    /// <see href="https://github.com/Cysharp/UniTask">UniTask</see>の使用例を含みます
    /// </summary>
    [TestFixture]
    [UnityPlatform(exclude = new[] { RuntimePlatform.WebGLPlayer })]
    public class UnityTestAttributeExample
    {
        [UnityTest]
        public IEnumerator YieldReturnNullでフレームを送る例()
        {
            var before = Time.frameCount;
            yield return null;
            var actual = Time.frameCount;

            Assert.That(actual, Is.EqualTo(before + 1));
        }

        [UnityTest]
        public IEnumerator YieldBreakの例_テストはパスする()
        {
            yield break;
        }

        [UnityTest]
        [Category("IgnoreCI")]
        [Description("CIでは（Linux Editorでは？）フレームが進んでしまう（648 to 41767）のでとりあえず除外")]
        public IEnumerator YieldInstructionの実装クラスを使用できる()
        {
            var before = Time.frameCount;
            yield return new WaitForEndOfFrame();
            var actual = Time.frameCount;

            Assert.That(actual, Is.EqualTo(before));
        }

        [UnityTest]
        public IEnumerator CustomYieldInstructionの実装クラスを使用できる()
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
            yield return SceneManager.LoadSceneAsync("HelloTesting");
            var cube = GameObject.Find("Cube");

            Assert.That((bool)cube, Is.True);
        }

        [UnityTest]
        public IEnumerator UniTaskでAsyncメソッドを使用するテストの例() => UniTask.ToCoroutine(async () =>
        {
            await SceneManager.LoadSceneAsync("HelloTesting");
            var cube = GameObject.Find("Cube");

            Assert.That((bool)cube, Is.True);
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
