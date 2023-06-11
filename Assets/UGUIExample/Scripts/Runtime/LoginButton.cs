// Copyright (c) 2021-2023 Koji Hasegawa.
// This software is released under the MIT License.

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UGUIExample
{
    /// <summary>
    /// ログインボタン押下時にユーザ名・パスワードを照合してメインメニューに遷移する
    /// </summary>
    [RequireComponent(typeof(Button))]
    [DisallowMultipleComponent]
    public class LoginButton : MonoBehaviour
    {
        private const string CorrectUserName = "Taro";
        private const string CorrectPassword = "12345678";

        public void Login()
        {
            var username = GameObject.Find("Username").GetComponentInChildren(typeof(Text)) as Text;
            if (username == null || username.text != CorrectUserName)
            {
                return;
            }

            var password = GameObject.Find("Password").GetComponentInChildren(typeof(Text)) as Text;
            if (password == null || password.text != CorrectPassword)
            {
                return;
            }

            SceneManager.LoadScene("MainMenu");
        }
    }
}
