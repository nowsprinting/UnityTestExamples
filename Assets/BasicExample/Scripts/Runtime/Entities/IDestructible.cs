﻿// Copyright (c) 2021 Koji Hasegawa.
// This software is released under the MIT License.

using BasicExample.Entities.Enums;
using UnityEngine;

namespace BasicExample.Entities
{
    /// <summary>
    /// 破壊可能なもの
    /// </summary>
    public interface IDestructible
    {
        /// <summary>
        /// 破壊されているか
        /// </summary>
        /// <returns></returns>
        bool IsDestroyed();

        /// <summary>
        /// HPゲージの%と表示色を返す
        /// </summary>
        /// <returns></returns>
        (float, Color) GetHitPointGauge();

        /// <summary>
        /// 攻撃を与える
        /// </summary>
        /// <param name="attackPower">攻撃力</param>
        /// <param name="attackElement">攻撃の属性</param>
        /// <returns>true: 1以上のダメージが入った</returns>
        bool GiveAttack(int attackPower, Element attackElement);
    }
}
