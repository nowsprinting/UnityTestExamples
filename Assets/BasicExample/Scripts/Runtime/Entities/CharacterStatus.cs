// Copyright (c) 2021 Koji Hasegawa.
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
        [SerializeField, Tooltip("属性")]
        private Element element;

        [SerializeField, Tooltip("最大ヒットポイント")]
        private int maxHitPoint;

        [SerializeField, Tooltip("防御力（buff分を除く）")]
        private int defense;

        [SerializeField, Tooltip("攻撃力（buff分を除く）")]
        private int attack;

        public int HitPoint { get; set; }
        private HitPointGaugeSetting _bounds;

        public CharacterStatus(Element ele = Element.None, int maxHp = 0, int def = 0, int atk = 0)
        {
            element = ele;
            maxHitPoint = maxHp;
            defense = def;
            attack = atk;
            HitPoint = maxHp;
            _bounds = ScriptableObject.CreateInstance<HitPointGaugeSetting>();
        }

        /// <inheritdoc/>
        public (float, Color) GetHitPointGauge()
        {
            var percentage = (float)HitPoint / maxHitPoint;
            return (percentage, _bounds.GetHitPointGaugeColor(percentage));
        }

        /// <inheritdoc/>
        public bool IsDestroyed()
        {
            return HitPoint <= 0;
        }

        /// <inheritdoc/>
        public bool GiveAttack(int attackPower, Element attackElement)
        {
            var attackPowerWithElement = (int)(attackPower * element.GetDamageMultiplier(attackElement));
            var damage = attackPowerWithElement - defense;
            if (damage <= 0)
            {
                return false;
            }

            HitPoint -= damage;
            return true;
        }
    }
}
