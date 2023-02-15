// Copyright (c) 2021 Koji Hasegawa.
// This software is released under the MIT License.

using UnityEngine;

namespace BasicExample.Entities.ScriptableObjects
{
    /// <summary>
    /// 破壊（死亡）演出
    /// </summary>
    [CreateAssetMenu(menuName = "BasicExample/ScriptableObject/" + nameof(DestructionEffect))]
    public class DestructionEffect : ScriptableObject
    {
        [SerializeField, Tooltip("表示名")]
        private string displayName;
    }
}
