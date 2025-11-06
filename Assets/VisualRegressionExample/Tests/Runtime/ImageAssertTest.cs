// Copyright (c) 2021-2023 Koji Hasegawa.
// This software is released under the MIT License.

#if ENABLE_GRAPHICS_TEST_FRAMEWORK
using System.Collections;
using System.IO;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using TestHelper.Attributes;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.TestTools.Graphics;

namespace VisualRegressionExample
{
    /// <summary>
    /// <see cref="ImageAssert.AreEqual(Texture2D, Camera, ImageComparisonSettings, string)"/>の検証
    /// 内部でTexture2D.ReadPixelsを使用しているので同様の制約がある
    ///
    /// 判明している制限事項:
    /// - WaitForEndOfFrame後に実行しなければならない
    /// - Batchmodeでは動作しない（WaitForEndOfFrameでフリーズするため）
    /// - メインスレッドでないと動作しない（SupportsTextureFormatNative can only be called from the main thread）
    /// </summary>
    [TestFixture]
    public class ImageAssertTest
    {
        private const string SandboxScenePath = "Assets/VisualRegressionExample/Scenes/VisualRegressionExample.unity";
        private const string ExpectedImagesPath = "Assets/VisualRegressionExample/Tests/ExpectedImages";

        [UnityTest]
        [LoadScene(SandboxScenePath)]
        public IEnumerator AreEqual_カメラ画像と比較_一致()
        {
            // テスト対象の実行

            yield return new WaitForEndOfFrame();
            // Note: スクリーンショット撮影の前提。なくても動作するようだが念のため

            var settings = new ImageComparisonSettings()
            {
                TargetWidth = Screen.width, TargetHeight = Screen.height, // 最低限、解像度を指定
            };

            var expected = ExpectedImage();
            ImageAssert.AreEqual(expected, Camera.main, settings);
        }

        [UnityTest]
        [LoadScene(SandboxScenePath)]
        public IEnumerator AreEqual_Texture2Dと比較_一致()
        {
            // テスト対象の実行

            yield return new WaitForEndOfFrame();
            // Note: スクリーンショット撮影の前提。必須

            var actual = CaptureScreenshotAsTexture(TextureFormat.ARGB32);
            // Note: ScreenCapture.CaptureScreenshotAsTexture()からはRGBA32で返るので、ARGB32に変換

            var settings = new ImageComparisonSettings()
            {
                TargetWidth = Screen.width, TargetHeight = Screen.height, // 最低限、解像度を指定
            };

            var expected = ExpectedImage();
            ImageAssert.AreEqual(expected, actual, settings);
        }

        [Test]
        [LoadScene(SandboxScenePath)]
        public async Task AreEqual_Texture2Dと比較_一致Async()
        {
            // テスト対象の実行

            var coroutineRunner = new GameObject().AddComponent<CoroutineRunner>();
            await UniTask.WaitForEndOfFrame(coroutineRunner);
            // Note: `UniTask.WaitForEndOfFrame(MonoBehaviour)` was implemented in UniTask v2.3.1

            var actual = CaptureScreenshotAsTexture(TextureFormat.ARGB32);
            // Note: ScreenCapture.CaptureScreenshotAsTexture()からはRGBA32で返るので、ARGB32に変換

            var settings = new ImageComparisonSettings()
            {
                TargetWidth = Screen.width, TargetHeight = Screen.height, // 最低限、解像度を指定
            };

            var expected = ExpectedImage();
            ImageAssert.AreEqual(expected, actual, settings);
        }

        private static Texture2D ExpectedImage()
        {
            Texture2D expected = null;

            var expectedFile = TestContext.CurrentTestExecutionContext.CurrentTest.Name
                .Replace('(', '_')
                .Replace(')', '_')
                .Replace(',', '-'); // Note: Graphics Tests FrameworkパッケージのActualImages下に作られるファイル名に準拠

            var expectedPath = Path.GetFullPath(Path.Combine(
                ExpectedImagesPath,
                $"{expectedFile}.png"));

            if (File.Exists(expectedPath))
            {
                var bytes = File.ReadAllBytes(Path.GetFullPath(expectedPath));
                expected = new Texture2D(Screen.width, Screen.height);
                expected.LoadImage(bytes);
            }

            return expected; // Note: ファイルがない場合、ImageAssert.AreEqual()にスクリーンショットを出力させるためnullを返す
        }

        private static Texture2D CaptureScreenshotAsTexture(TextureFormat format = TextureFormat.ARGB32)
        {
            var src = ScreenCapture.CaptureScreenshotAsTexture();
            var dst = new Texture2D(src.width, src.height, format, false);
            dst.SetPixels(src.GetPixels());
            return dst;
        }

        private class CoroutineRunner : MonoBehaviour
        {
        }
    }
}
#endif
