// Copyright (c) 2021-2026 Koji Hasegawa.
// This software is released under the MIT License.

using NUnit.Framework;
using SceneExample.TestDoubles;
using UnityEngine;

namespace SceneExample
{
    [TestFixture]
    public class NestedComponentTest
    {
        /// <summary>
        /// テスト内で使用するコンポーネントの例.
        /// 入れ子のコンポーネントはインスペクターの "Add Component" ピッカーに表示されない
        /// </summary>
        /// <seealso cref="HiddenComponent"/>
        public class NestedComponent : MonoBehaviour
        {
        }

        [Test]
        public void 入れ子のコンポーネントはAddComponentピッカーに表示されないがテストで使用できる()
        {
            var nestedComponent = new GameObject().AddComponent<NestedComponent>();
#if UNITY_2022_3_OR_NEWER
            var actual = Object.FindAnyObjectByType<NestedComponent>();
#else
            var actual = Object.FindObjectOfType<NestedComponent>();
#endif

            Assert.That(actual, Is.EqualTo(nestedComponent));
        }
    }
}
