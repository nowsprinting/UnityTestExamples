// Copyright (c) 2021-2025 Koji Hasegawa.
// This software is released under the MIT License.

using UnityEngine;

namespace SceneExample.TestDoubles
{
    /// <summary>
    /// テスト内で使用するコンポーネントの例.
    /// テストアセンブリにあってもインスペクターの "Add Component" ピッカーで検索できてしまうが、
    /// <see cref="AddComponentMenu"/> に "" を渡すことで非表示にできる。
    /// </summary>
    /// <remarks>
    /// - Unity 2021以降、"/" ではじまる文字列も有効
    /// - Unity 2023.2.0a17 から6000 系の途中まで、空文字では認識されない不具合
    /// </remarks>
#if UNITY_2021_1_OR_NEWER
    [AddComponentMenu("/")] // Add Componentピッカーから隠す
#else
    [AddComponentMenu("")] // Add Componentピッカーから隠す
#endif
    public class HiddenComponent : MonoBehaviour
    {
    }
}
