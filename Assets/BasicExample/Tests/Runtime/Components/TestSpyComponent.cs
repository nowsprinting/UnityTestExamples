// Copyright (c) 2021-2023 Koji Hasegawa.
// This software is released under the MIT License.

using UnityEngine;

namespace BasicExample.Components
{
    /// <summary>
    /// テスト内で使用するコンポーネントの例.
    /// テストアセンブリにあってもインスペクターの "Add Component" ピッカーで検索できてしまうが、
    /// <c>AddComponentMenuAttribute</c> に "" を渡すことで非表示にできる。
    /// </summary>
    [AddComponentMenu("")] // Hide from "Add Component" picker
    public class TestSpyComponent : MonoBehaviour
    {
        /// <summary>
        /// <c>Awake</c> が呼ばれた
        /// </summary>
        public bool IsAwakeCalled { get; private set; } = false;

        /// <summary>
        /// <c>Start</c> が呼ばれた
        /// </summary>
        public bool IsStartCalled { get; private set; } = false;

        private void Awake()
        {
            IsAwakeCalled = true;
        }

        private void Start()
        {
            IsStartCalled = true;
        }
    }
}
