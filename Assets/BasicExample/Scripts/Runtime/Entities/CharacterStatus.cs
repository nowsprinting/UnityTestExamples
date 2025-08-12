// Copyright (c) 2021-2025 Koji Hasegawa.
// This software is released under the MIT License.

using System;
using BasicExample.Entities.Enums;
using BasicExample.Entities.Settings;
using UnityEngine;

namespace BasicExample.Entities
{
    /// <summary>
    /// ゲーム内キャラクターのステータスと、被ダメージを扱うメソッドを実装
    /// </summary>
    [Serializable]
    public class CharacterStatus : IDestructible
    {
        [field: SerializeField, Tooltip("属性")]
        private Element Element { get; set; }

        [field: SerializeField, Tooltip("防御力（buff分を除く）")]
        private int Defense { get; set; }

        [field: SerializeField, Tooltip("攻撃力（buff分を除く）")]
        private int Attack { get; set; }

        [field: SerializeField, Tooltip("最大ヒットポイント")]
        private int MaxHitPoint { get; set; }

        internal int HitPoint { get; private set; }
        private HitPointGaugeSetting _bounds;

        public CharacterStatus(Element element = Element.None, int defense = 0, int attack = 0, int hp = 0)
        {
            Element = element;
            Defense = defense;
            Attack = attack;
            MaxHitPoint = hp;
            HitPoint = hp;
            _bounds = ScriptableObject.CreateInstance<HitPointGaugeSetting>();
        }

        /// <inheritdoc/>
        public (float, Color) GetHitPointGauge()
        {
            var percentage = (float)HitPoint / MaxHitPoint;
            return (percentage, _bounds.GetHitPointGaugeColor(percentage));
        }

        /// <inheritdoc/>
        public bool IsDestroyed()
        {
            return HitPoint <= 0;
        }

        /// <inheritdoc/>
        public bool TakeDamage(Element element, int attack)
        {
            var attackPowerWithElement = (int)(attack * Element.GetDamageMultiplier(element));
            var damage = attackPowerWithElement - Defense;
            if (damage <= 0)
            {
                return false;
            }

            HitPoint -= damage;
            return true;
        }
    }
}
