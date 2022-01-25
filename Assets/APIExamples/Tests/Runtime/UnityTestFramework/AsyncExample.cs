// Copyright (c) 2022 Koji Hasegawa.
// This software is released under the MIT License.

using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using UnityEngine;

namespace APIExamples.UnityTestFramework
{
    public class AsyncExample
    {
        [Test]
        public async Task 非同期テストの例_Taskをawaitできる()
        {
            await Foo(1);
            var actual = await Bar(2);
            Assert.That(actual, Is.EqualTo(3));
        }

        private static async Task Foo(int id)
        {
            Debug.Log($"Start Foo({id})");
            await Task.Delay(200);
            Debug.Log($"Exit Foo({id})");
        }

        private static async Task<int> Bar(int id)
        {
            Debug.Log($"Start Bar({id})");
            await Task.Delay(200);
            Debug.Log($"Exit Bar({id})");
            return id + 1;
        }

        [Test]
        public async Task 非同期テストの例_UniTaskをawaitできる()
        {
            await UniTaskFoo(1);
            var actual = await UniTaskBar(2);
            Assert.That(actual, Is.EqualTo(3));
        }

        private static async UniTask UniTaskFoo(int id)
        {
            Debug.Log($"Start UniTaskFoo({id})");
            await UniTask.Delay(200);
            Debug.Log($"Exit UniTaskFoo({id})");
        }

        private static async UniTask<int> UniTaskBar(int id)
        {
            Debug.Log($"Start UniTaskBar({id})");
            await UniTask.Delay(200);
            Debug.Log($"Exit UniTaskBar({id})");
            return id + 1;
        }
    }
}
