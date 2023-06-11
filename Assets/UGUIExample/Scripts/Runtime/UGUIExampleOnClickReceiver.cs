// Copyright (c) 2021-2022 Koji Hasegawa.
// This software is released under the MIT License.

using System.Runtime.CompilerServices;
using UnityEngine;

namespace UGUIExample
{
    /// <summary>
    /// IPointerClickHandlerのOnClickイベントを受けてログ出力するコンポーネント
    /// </summary>
    public class UGUIExampleOnClickReceiver : MonoBehaviour
    {
        public void ReceiveOnClick() => Log();

        private void Log([CallerMemberName] string member = null)
        {
            Debug.Log($"{this.name}.{member}");
        }
    }
}
