// Copyright (c) 2021 Koji Hasegawa.
// This software is released under the MIT License.

using System;

namespace BasicExample.Entities.Enums
{
    /// <summary>
    /// 元素的な属性
    /// </summary>
    /// <remarks>
    /// 当たり障りなさそうな陰陽五行とします。種類は未来永劫バージョンアップしても増えません（フラグ
    /// </remarks>
    public enum Element
    {
        None,
        Wood,
        Fire,
        Water,
        Earth,
        Metal,
    }

    public static class ElementExtensions
    {
        /// <summary>
        /// 属性名を返す
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static string GetName(this Element self)
        {
            switch (self)
            {
                case Element.None:
                    return "無";
                case Element.Wood:
                    return "木";
                case Element.Fire:
                    return "火";
                case Element.Water:
                    return "水";
                case Element.Earth:
                    return "土";
                case Element.Metal:
                    return "金";
                default:
                    throw new ArgumentException($"Unknown Element: {self.ToString()}");
            }
        }

        /// <summary>
        /// 属性攻撃を受けたときの被ダメージ倍率を返す
        /// </summary>
        /// <param name="self"></param>
        /// <param name="attack">攻撃の属性</param>
        /// <returns></returns>
        public static float GetDamageMultiplier(this Element self, Element attack)
        {
            switch (self)
            {
                case Element.Wood:
                    switch (attack)
                    {
                        case Element.Fire:
                            return 2.0f;
                        case Element.Water:
                            return 0.5f;
                        default:
                            return 1.0f;
                    }
                case Element.Fire:
                    switch (attack)
                    {
                        case Element.Water:
                            return 2.0f;
                        case Element.Wood:
                            return 0.5f;
                        default:
                            return 1.0f;
                    }
                case Element.Water:
                    switch (attack)
                    {
                        case Element.Wood:
                            return 2.0f;
                        case Element.Fire:
                            return 0.5f;
                        default:
                            return 1.0f;
                    }
                case Element.Earth:
                    switch (attack)
                    {
                        case Element.Metal:
                            return 2.0f;
                        default:
                            return 0.5f;
                    }
                case Element.Metal:
                    switch (attack)
                    {
                        case Element.Earth:
                            return 2.0f;
                        default:
                            return 0.5f;
                    }
                default:
                    return 1.0f;
            }
        }
    }
}
