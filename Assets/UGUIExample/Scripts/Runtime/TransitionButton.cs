// Copyright (c) 2021-2023 Koji Hasegawa.
// This software is released under the MIT License.

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UGUIExample
{
    /// <summary>
    /// 画面遷移するボタン
    /// </summary>
    [RequireComponent(typeof(Button))]
    [DisallowMultipleComponent]
    public class TransitionButton : MonoBehaviour
    {
        [SerializeField]
        private string transitionToSceneName;

        private void OnEnable()
        {
            GetComponent<Button>().onClick.AddListener(Transit);
        }

        private void OnDisable()
        {
            GetComponent<Button>().onClick.RemoveListener(Transit);
        }

        private void Transit()
        {
            if (transitionToSceneName == null)
            {
                return;
            }

            SceneManager.LoadScene(transitionToSceneName);
        }
    }
}
