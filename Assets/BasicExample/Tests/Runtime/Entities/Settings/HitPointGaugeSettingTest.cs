// Copyright (c) 2021 Koji Hasegawa.
// This software is released under the MIT License.

using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools.Utils;

namespace BasicExample.Entities
{
    public class HitPointGaugeSettingTest
    {
        private static object[] s_withoutBuffCases =
        {
            new object[] { 0f, Color.red },
            new object[] { 0.01f, Color.red },
            new object[] { 0.19f, Color.red },
            new object[] { 0.2f, Color.yellow },
            new object[] { 0.49f, Color.yellow },
            new object[] { 0.5f, Color.green },
        };

        [TestCaseSource(nameof(s_withoutBuffCases))]
        public void GetHitPointGaugeColor_Buffなし_表示色は正しい(float percentage, Color expected)
        {
            var sut = ScriptableObject.CreateInstance<HitPointGaugeSetting>();
            var actual = sut.GetHitPointGaugeColor(percentage, false);

            Assert.That(actual, Is.EqualTo(expected).Using(ColorEqualityComparer.Instance));
        }

        public static IEnumerable<object[]> RedWithBuffCases()
        {
            Color.RGBToHSV(Color.red, out var hue, out var saturation, out var value);
            var color = Color.HSVToRGB(hue, saturation - 0.3f, value);

            yield return new object[] { 0f, color };
            yield return new object[] { 0.01f, color };
            yield return new object[] { 0.19f, color };
        }

        public static IEnumerable<object[]> YellowWithBuffCases()
        {
            Color.RGBToHSV(Color.yellow, out var hue, out var saturation, out var value);
            var color = Color.HSVToRGB(hue, saturation - 0.3f, value);

            yield return new object[] { 0.2f, color };
            yield return new object[] { 0.49f, color };
        }

        public static IEnumerable<object[]> GreenWithBuffCases()
        {
            Color.RGBToHSV(Color.green, out var hue, out var saturation, out var value);
            var color = Color.HSVToRGB(hue, saturation - 0.3f, value);

            yield return new object[] { 0.5f, color };
        }

        [TestCaseSource(nameof(RedWithBuffCases))]
        [TestCaseSource(nameof(YellowWithBuffCases))]
        [TestCaseSource(nameof(GreenWithBuffCases))]
        public void GetHitPointGaugeColor_Buffあり_表示色は正しい(float percentage, Color expected)
        {
            var sut = ScriptableObject.CreateInstance<HitPointGaugeSetting>();
            var actual = sut.GetHitPointGaugeColor(percentage, true);

            Assert.That(actual, Is.EqualTo(expected).Using(ColorEqualityComparer.Instance));
        }
    }
}
