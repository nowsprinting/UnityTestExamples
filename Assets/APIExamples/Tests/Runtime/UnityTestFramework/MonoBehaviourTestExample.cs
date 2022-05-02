// Copyright (c) 2021 Koji Hasegawa.
// This software is released under the MIT License.

using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace APIExamples.UnityTestFramework
{
    /// <summary>
    /// <see cref="MonoBehaviourTest{T}"/>の使用例
    /// </summary>
    public class MonoBehaviourTestExample
    {
        [UnityTest, UnityPlatform(exclude = new[] { RuntimePlatform.WebGLPlayer })]
        public IEnumerator MonoBehaviourTestの使用例()
        {
            yield return new MonoBehaviourTest<MyMonoBehaviourTest>();
        }

        /// <summary>
        /// <see cref="IMonoBehaviourTest"/>の実装例
        /// </summary>
        private class MyMonoBehaviourTest : SUTMonoBehaviour, IMonoBehaviourTest
        {
            /// <summary>
            /// テスト終了条件を満たしたらtrueを返します
            /// アサーションもここに書きます
            /// </summary>
            public bool IsTestFinished
            {
                get
                {
                    // Exercise
                    if (!base._doneDestroy)
                    {
                        return false; // テスト実行を継続
                    }

                    // Verify 
                    Assert.That(base._doneAwake, Is.True);
                    Assert.That(base._doneStart, Is.True);
                    Assert.That(base._doneDestroy, Is.True);

                    return true; // テスト終了
                }
            }
        }

        /// <summary>
        /// テスト対象MonoBehaviour
        /// 生成されて5フレームで破棄されます
        /// </summary>
        private class SUTMonoBehaviour : MonoBehaviour
        {
            protected bool _doneAwake;
            protected bool _doneStart;
            private int _frameCount;
            protected bool _doneDestroy;

            private void Awake()
            {
                _doneAwake = true;
            }

            private void Start()
            {
                _doneStart = true;
            }

            private void Update()
            {
                if (++_frameCount > 4)
                {
                    Destroy(this);
                }
            }

            private void OnDestroy()
            {
                _doneDestroy = true;
            }
        }
    }
}
