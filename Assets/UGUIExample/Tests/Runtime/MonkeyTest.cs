// Copyright (c) 2021-2023 Koji Hasegawa.
// This software is released under the MIT License.

using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using TestHelper.Monkey;
using TestHelper.Monkey.Random;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UGUIExample
{
    /// <summary>
    /// Monkey Test Helperパッケージを使用したモンキーテストの実装例.
    /// </summary>
    /// <see href="https://github.com/nowsprinting/test-helper.monkey"/>
    [TestFixture]
    public class MonkeyTest
    {
        [Test]
        public async Task MonkeyTesting()
        {
            var random = new RandomImpl(); // 擬似乱数生成器
            Debug.Log($"Random that monkey uses: {random}"); // シード値をログ出力

            await SceneManager.LoadSceneAsync("MainMenu");
            // Note: ランダム要素のあるSceneの場合、擬似乱数シードにrandom.Next()を使用すると再現に必要なシード値が1つで済んで便利です

            var config = new MonkeyConfig
            {
                Lifetime = TimeSpan.FromSeconds(5), // 5秒間動作（3分以上にする場合はTimeout属性でテスト自体のタイムウトを延ばすこと）
                DelayMillis = 200, // 操作間隔は200ms
                SecondsToErrorForNoInteractiveComponent = 5, // 5秒無操作で失敗扱い
                Random = random, // 擬似乱数生成器を指定
            };

            await Monkey.Run(config);
        }
    }
}
