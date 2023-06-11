// Copyright (c) 2021-2023 Koji Hasegawa.
// This software is released under the MIT License.

using System.Diagnostics.CodeAnalysis;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UGUIExample.PageObjects
{
    /// <summary>
    /// ログイン画面のページオブジェクト.
    /// ページオブジェクトパターンの実装例
    /// </summary>
    [SuppressMessage("ReSharper", "MemberCanBeMadeStatic.Global")]
    public class LoginPage
    {
        public static async UniTask<LoginPage> Load()
        {
            await SceneManager.LoadSceneAsync("Login");
            return new LoginPage();
        }

        public bool IsShown()
        {
            return SceneManager.GetActiveScene().name == "Login";
        }

        public void InputUsername(string s)
        {
            var inputField = GameObject.Find("Username").GetComponentInChildren(typeof(Text)) as Text;
            if (inputField == null)
            {
                return;
            }

            inputField.text = s;
        }

        public void InputPassword(string s)
        {
            var inputField = GameObject.Find("Password").GetComponentInChildren(typeof(Text)) as Text;
            if (inputField == null)
            {
                return;
            }

            inputField.text = s;
        }

        public async UniTask<MainMenuPage> Login()
        {
            var button = GameObject.Find("LoginButton").GetComponent<Button>();
            button.OnPointerClick(new PointerEventData(EventSystem.current));
            await UniTask.NextFrame();
            return new MainMenuPage();
        }
    }
}
