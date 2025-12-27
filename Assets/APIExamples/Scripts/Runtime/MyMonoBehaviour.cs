// Copyright (c) 2021-2025 Koji Hasegawa.
// This software is released under the MIT License.

using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace APIExamples
{
    /// <summary>
    /// <see cref="APIExamples.UnityTestFramework.MonoBehaviourTestExample"/> のテスト対象.
    /// 生成されて5フレームで破棄されます
    /// </summary>
    [SuppressMessage("ReSharper", "InvalidXmlDocComment")]
    public class MyMonoBehaviour : MonoBehaviour
    {
        protected bool _wasAwake;
        protected bool _wasStart;
        protected bool _wasDestroy;
        private int _frameCount;

        private void Awake()
        {
            _wasAwake = true;
        }

        private void Start()
        {
            _wasStart = true;
        }

        private void Update()
        {
            if (++_frameCount > 4)
            {
                Destroy(this.gameObject);
            }
        }

        private void OnDestroy()
        {
            _wasDestroy = true;
        }
    }
}
