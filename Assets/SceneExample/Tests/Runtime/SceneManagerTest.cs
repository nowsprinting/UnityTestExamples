// Copyright (c) 2021 Koji Hasegawa.
// This software is released under the MIT License.

using System.Collections;
using NUnit.Framework;
using TestHelper.Attributes;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace SceneExample
{
    [TestFixture]
    public class SceneManagerTest
    {
        [UnityTest]
        [Description("Generate a clean Scene by CreateScene method")]
        public IEnumerator CreateScene_クリーンなSceneを生成してテストを実行する例()
        {
            var scene = SceneManager.CreateScene("New Scene");
            SceneManager.SetActiveScene(scene);
            yield return null;

            var actual = SceneManager.GetActiveScene().name;
            Assert.That(actual, Is.EqualTo("New Scene"));
        }

        [UnityTest]
        [Description("Load scene included in the \"Scenes in Build\" by LoadSceneAsync method")]
        public IEnumerator LoadSceneAsync_ScenesInBuildに含まれるSceneをロードする例()
        {
            yield return SceneManager.LoadSceneAsync("HelloTesting");
            // Scene名称指定でロードできます。エディター実行でもプレイヤー実行でも、同じコードで動作します。
            // Can be load by Scene name. It works in both in Editor and on Player.

            var cube = GameObject.Find("Cube");
            Assert.That(cube, Is.Not.Null);
        }

        [Test]
        [LoadScene("Assets/SceneExample/Scenes/NotContainScenesInBuild.unity")]
        [Description("Load scene not included in the \"Scenes in Build\"")]
        public void LoadSceneAsync_ScenesInBuildに含まれていないSceneをLoadScene属性を使ってロードする例()
        {
            // Test Helper パッケージ（com.nowsprinting.test-helper）に含まれる LoadScene 属性は、テスト実行前に指定された Scene をロードします。
            // エディター実行でもプレイヤー実行でも、同じコードで動作します。
            // 『Unity Test Framework完全攻略ガイド』10.2.2 および 10.3 で紹介している方法で、エディター実行・プレイヤー実行の差異を吸収しています。

            // LoadScene attribute in Test Helper package (com.nowsprinting.test-helper) loads Scene before test execution.
            // It works in both in Editor and on Player.

            var sphere = GameObject.Find("Sphere");
            Assert.That(sphere, Is.Not.Null);
        }
    }
}
