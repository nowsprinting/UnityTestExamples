// Copyright (c) 2021-2025 Koji Hasegawa.
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
            var actual = Object.FindObjectOfType<NestedComponent>();

            Assert.That(actual, Is.EqualTo(nestedComponent));
        }
    }
}
