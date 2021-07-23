// Copyright (c) 2021 Koji Hasegawa.
// This software is released under the MIT License.

using UnityEngine;

namespace BasicExample.Entities
{
    /// <summary>
    /// ヒットポイントゲージの表示色しきい値
    /// </summary>
    [CreateAssetMenu(menuName = "BasicExample/ScriptableObject/" + nameof(HitPointGaugeSetting))]
    public class HitPointGaugeSetting : ScriptableObject
    {
        [SerializeField]
        private Color red = Color.red;

        [SerializeField]
        private Color yellow = Color.yellow;

        [SerializeField]
        private Color green = Color.green;

        [SerializeField, Tooltip("赤く表示するしきい値")]
        [Range(0.0f, 1.0f)]
        private float redBounds = 0.2f;

        [SerializeField, Tooltip("黄色く表示するしきい値")]
        [Range(0.0f, 1.0f)]
        private float yellowBounds = 0.5f;

        /// <summary>
        /// ヒットポイントの%から、ゲージの表示色を返す
        /// </summary>
        /// <param name="percentage">残ヒットポイント%</param>
        /// <param name="buff">バフあり</param>
        /// <returns>ゲージの表示色</returns>
        public Color GetHitPointGaugeColor(float percentage, bool buff = false)
        {
            return WithBuff(BaseColor(percentage), buff);
        }

        private Color BaseColor(float percentage)
        {
            if (percentage < redBounds)
            {
                return red;
            }
            else if (percentage < yellowBounds)
            {
                return yellow;
            }
            else
            {
                return green;
            }
        }

        private static Color WithBuff(Color color, bool buff)
        {
            Color.RGBToHSV(color, out var hue, out var saturation, out var value);

            if (buff)
            {
                saturation -= 0.3f;
            }

            return Color.HSVToRGB(hue, saturation, value);
        }
    }
}
