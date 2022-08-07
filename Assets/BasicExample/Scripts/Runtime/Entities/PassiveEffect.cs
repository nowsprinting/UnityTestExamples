// Copyright (c) 2021-2022 Koji Hasegawa.
// This software is released under the MIT License.

using UnityEngine;

namespace BasicExample.Entities
{
    public class PassiveEffect : MonoBehaviour
    {
        private PassiveEffectState _state = PassiveEffectState.Normal;
        private float _passiveEffectStartTime;
        private const float PassiveEffectDuration = 30f;

        /// <summary>
        /// 強化/弱体状態を返す
        /// </summary>
        /// <returns>強化/弱体状態</returns>
        public PassiveEffectState State()
        {
            return _state;
        }

        /// <summary>
        /// 強化/弱体状態の残り時間を返す
        /// </summary>
        /// <returns>強化/弱体状態の残り時間</returns>
        public float PassiveEffectExpireTime()
        {
            return PassiveEffectDuration - (Time.time - _passiveEffectStartTime);
        }

        /// <summary>
        /// バフを与える
        /// </summary>
        public void Buff()
        {
            if (_state == PassiveEffectState.Weakening)
            {
                _state = PassiveEffectState.Normal;
            }
            else if (_state == PassiveEffectState.Normal)
            {
                _state = PassiveEffectState.Strengthening;
            }

            _passiveEffectStartTime = Time.time; // 持続時間リセット
        }

        /// <summary>
        /// デバフを与える
        /// </summary>
        public void DeBuff()
        {
            if (_state == PassiveEffectState.Strengthening)
            {
                _state = PassiveEffectState.Normal;
            }
            else if (_state == PassiveEffectState.Normal)
            {
                _state = PassiveEffectState.Weakening;
            }

            _passiveEffectStartTime = Time.time; // 持続時間リセット
        }

        private void Update()
        {
            CheckPassiveEffectExpire();
        }

        /// <summary>
        /// 強化/弱体の持続時間切れ判定
        /// </summary>
        private void CheckPassiveEffectExpire()
        {
            if (_state == PassiveEffectState.Strengthening || _state == PassiveEffectState.Weakening)
            {
                if (Time.time > (_passiveEffectStartTime + PassiveEffectDuration))
                {
                    _state = PassiveEffectState.Normal;
                }
            }
        }
    }
}
