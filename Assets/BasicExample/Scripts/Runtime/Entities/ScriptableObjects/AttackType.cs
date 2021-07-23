// Copyright (c) 2021 Koji Hasegawa.
// This software is released under the MIT License.

using UnityEngine;

namespace BasicExample.Entities
{
    /// <summary>
    /// 敵の攻撃方法
    /// </summary>
    [CreateAssetMenu(menuName = "BasicExample/ScriptableObject/" + nameof(AttackType))]
    public class AttackType : ScriptableObject
    {
        [SerializeField, Tooltip("表示名")]
        private string displayName;

        [SerializeField, Tooltip("攻撃の属性")]
        private Element element;
    }
}
