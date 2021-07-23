// Copyright (c) 2021 Koji Hasegawa.
// This software is released under the MIT License.

using UnityEngine;

namespace BasicExample.Entities
{
    /// <summary>
    /// 敵の種族
    /// 当たり障りのない伝承とか実在の生きものとします
    /// </summary>
    [CreateAssetMenu(menuName = "BasicExample/ScriptableObject/" + nameof(Race))]
    public class Race : ScriptableObject, IDestructible
    {
        [SerializeField, Tooltip("表示名")]
        private string displayName;

        [SerializeField, Tooltip("ステータス")]
        private CharacterStatus status;

        [SerializeField, Tooltip("攻撃方法")]
        private AttackType[] attackTypes;

        [SerializeField, Tooltip("破壊（死亡）演出")]
        private DestructionEffect[] destructionEffects;

        [SerializeField, Tooltip("倒したらもらえる経験値")]
        private int rewardExp;

        [SerializeField, Tooltip("倒したらもらえる通貨")]
        private int rewardGold;

        /// <inheritdoc/>
        public (float, Color) GetHitPointGauge() => status.GetHitPointGauge();

        /// <inheritdoc/>
        public bool IsDestroyed() => status.IsDestroyed();

        /// <inheritdoc/>
        public bool GiveAttack(int attackPower, Element attackElement) => status.GiveAttack(attackPower, attackElement);
    }
}
