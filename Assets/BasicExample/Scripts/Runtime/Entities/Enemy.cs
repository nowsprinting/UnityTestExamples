// Copyright (c) 2021-2025 Koji Hasegawa.
// This software is released under the MIT License.

using BasicExample.Entities.Enums;
using BasicExample.Entities.ScriptableObjects;
using UnityEngine;

namespace BasicExample.Entities
{
    public class Enemy : MonoBehaviour, IDestructible
    {
        [SerializeField, Tooltip("固有名（省略時は種族名を表示）")]
        private string uniqueName;

        [SerializeField, Tooltip("種族")]
        private Race race;

        /// <inheritdoc/>
        public (float, Color) GetHitPointGauge() => race.GetHitPointGauge();

        /// <inheritdoc/>
        public bool IsDestroyed() => race.IsDestroyed();

        /// <inheritdoc/>
        public bool TakeDamage(Element element, int attackPower) => race.TakeDamage(element, attackPower);
    }
}
