// Copyright (c) 2021-2025 Koji Hasegawa.
// This software is released under the MIT License.

using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

#pragma warning disable CS0618 // Type or member is obsolete

namespace APIExamples.UnityTestFramework
{
    /// <summary>
    /// <see cref="MonoBehaviourTest{T}"/> の使用例
    /// </summary>
    [TestFixture]
    public class MonoBehaviourTestExample
    {
        /// <summary>
        /// <see cref="IMonoBehaviourTest"/> の実装例（テストスパイ）
        /// <p/>
        /// <c>Start</c> が完了した時点で終了します
        /// </summary>
        private class SpyMyMonoBehaviour : MyMonoBehaviour, IMonoBehaviourTest
        {
            public bool WasAwake => base._wasAwake;
            public bool WasStart => base._wasStart;
            public bool WasDestroy => base._wasDestroy;

            /// <summary>
            /// テスト終了条件を満たしたらtrueを返します
            /// </summary>
            public bool IsTestFinished => WasStart; // Startが完了したら終了
        }

        [UnityTest]
        public IEnumerator MonoBehaviourTestをテストスパイとして使用したMonoBehaviourのテスト()
        {
            // Exercise
            yield return new MonoBehaviourTest<SpyMyMonoBehaviour>();

            // Verify
            var spy = GameObject.FindObjectOfType<SpyMyMonoBehaviour>();
            Assert.That(spy.WasAwake, Is.True);
            Assert.That(spy.WasStart, Is.True);
            Assert.That(spy.WasDestroy, Is.False);

            // Teardown
            GameObject.DestroyImmediate(spy.gameObject);
        }

        /// <summary>
        /// <see cref="IMonoBehaviourTest"/> の実装例（モックオブジェクト）
        /// <p/>
        /// <c>GameObject</c> が破棄されたら内部情報を検証して終了します
        /// </summary>
        private class MockMyMonoBehaviour : MyMonoBehaviour, IMonoBehaviourTest
        {
            /// <summary>
            /// テスト終了条件を満たしたらtrueを返します。
            /// モックオブジェクトとしての実装例なので、アサーションまで行ないます
            /// </summary>
            public bool IsTestFinished
            {
                get
                {
                    if (!base._wasDestroy)
                    {
                        return false; // 条件を満たすまで（OnDestroyが呼ばれるまで）テスト実行を継続
                    }

                    // Verify
                    Assert.That(base._wasAwake, Is.True);
                    Assert.That(base._wasStart, Is.True);
                    Assert.That(base._wasDestroy, Is.True);

                    return true;
                }
            }
        }

        [UnityTest]
        public IEnumerator MonoBehaviourTestをモックオブジェクトとして使用したMonoBehaviourのテスト()
        {
            // Exercise & Verify
            yield return new MonoBehaviourTest<MockMyMonoBehaviour>();

            // Teardown
            // Note: MockMyMonoBehaviour の実装ではこの時点で破棄済みですが、通常は後始末が必要です
            var mock = GameObject.FindObjectOfType<MockMyMonoBehaviour>();
            if (mock != null)
            {
                GameObject.DestroyImmediate(mock.gameObject);
            }
        }
    }
}
