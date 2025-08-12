// Copyright (c) 2021-2025 Koji Hasegawa.
// This software is released under the MIT License.

using BasicExample.Entities.Enums;
using UnityEngine;

namespace BasicExample.Entities
{
    public class Player : MonoBehaviour, IDestructible
    {
        [SerializeField, Tooltip("ステータス（共通部分）")]
        private CharacterStatus status;

        /// <inheritdoc/>
        public (float, Color) GetHitPointGauge() => status.GetHitPointGauge();

        /// <inheritdoc/>
        public bool IsDestroyed() => status.IsDestroyed();

        /// <inheritdoc/>
        public bool TakeDamage(Element element, int attack) => status.TakeDamage(element, attack);
    }
}
