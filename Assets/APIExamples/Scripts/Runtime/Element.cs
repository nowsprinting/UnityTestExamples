// Copyright (c) 2021-2025 Koji Hasegawa.
// This software is released under the MIT License.

using System;

namespace APIExamples
{
    /// <summary>
    /// 元素的な属性.
    /// </summary>
    /// <remarks>
    /// このenumは <c>BasicExample.Entities.Enums.Element</c> とは異なります（仕様を簡略化しました）
    /// </remarks>
    public enum Element
    {
        None,
        Fire,
        Water,
        Wood,
    }

    public static class ElementExtensions
    {
        /// <summary>
        /// 属性名を返す.
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static string GetName(this Element self)
        {
            switch (self)
            {
                case Element.None:
                    return "無";
                case Element.Fire:
                    return "火";
                case Element.Water:
                    return "水";
                case Element.Wood:
                    return "木";
                default:
                    throw new ArgumentException($"Unknown Element: {self.ToString()}");
            }
        }

        /// <summary>
        /// 属性攻撃を受けたときの被ダメージ倍率を返す.
        /// </summary>
        /// <param name="self"></param>
        /// <param name="attack">攻撃側の属性</param>
        /// <returns>被ダメージ倍率</returns>
        public static float GetDamageMultiplier(this Element self, Element attack)
        {
            if (self == Element.Wood && attack == Element.Fire)
            {
                return 2.0f;
            }

            if (self == Element.Fire && attack == Element.Water)
            {
                return 2.0f;
            }

            if (self == Element.Water && attack == Element.Wood)
            {
                return 2.0f;
            }

            return 1.0f;
        }
    }
}
