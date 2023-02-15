// Copyright (c) 2021-2022 Koji Hasegawa.
// This software is released under the MIT License.

using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools.Utils;

namespace BasicExample.Entities.Settings
{
    [TestFixture]
    public class HitPointGaugeSettingTest
    {
        [TestFixture]
        public class 同値分割法に基づいたテスト
        {
            private static object[] s_equivalencePartitions =
            {
                new object[] { 0f, Color.black },
                new object[] { 0.1f, Color.red },
                new object[] { 0.35f, Color.yellow },
                new object[] { 0.75f, Color.green },
            };

            [TestCaseSource(nameof(s_equivalencePartitions))]
            public void GetHitPointGaugeColor_HP残量に対する表示色は正しい(float percentage, Color expected)
            {
                var sut = ScriptableObject.CreateInstance<HitPointGaugeSetting>();
                var actual = sut.GetHitPointGaugeColor(percentage, false);

                Assert.That(actual, Is.EqualTo(expected).Using(ColorEqualityComparer.Instance));
            }
        }

        [TestFixture]
        public class 境界値分析に基づいたテスト
        {
            private static object[] s_boundaryValues =
            {
                new object[] { 0f, Color.black },
                new object[] { 0.01f, Color.red },
                new object[] { 0.19f, Color.red },
                new object[] { 0.2f, Color.yellow },
                new object[] { 0.49f, Color.yellow },
                new object[] { 0.5f, Color.green },
                new object[] { 1f, Color.green },
            };

            [TestCaseSource(nameof(s_boundaryValues))]
            public void GetHitPointGaugeColor_HP残量に対する表示色は正しい(float percentage, Color expected)
            {
                var sut = ScriptableObject.CreateInstance<HitPointGaugeSetting>();
                var actual = sut.GetHitPointGaugeColor(percentage, false);

                Assert.That(actual, Is.EqualTo(expected).Using(ColorEqualityComparer.Instance));
            }
        }

        [TestFixture]
        public class デシジョンテーブルに基づいたテスト
        {
            private static object[] s_decisionTableCases =
            {
                new object[] { 0f, false, Color.black },
                new object[] { 0.1f, false, Color.red },
                new object[] { 0.1f, true, new Color(1f, 0.3f, 0.3f) }, // 明るい赤
                new object[] { 0.35f, false, Color.yellow },
                new object[] { 0.35f, true, new Color(1f, 0.945f, 0.316f) }, // 明るい黄
                new object[] { 0.75f, false, Color.green },
                new object[] { 0.75f, true, new Color(0.3f, 1f, 0.3f) }, // 明るい緑
            };

            [TestCaseSource(nameof(s_decisionTableCases))]
            public void GetHitPointGaugeColor_HP残量とバフ有無に対する表示色は正しい(float percentage, bool buff, Color expected)
            {
                var sut = ScriptableObject.CreateInstance<HitPointGaugeSetting>();
                var actual = sut.GetHitPointGaugeColor(percentage, buff);

                Assert.That(actual, Is.EqualTo(expected).Using(ColorEqualityComparer.Instance));
            }
        }
    }
}
