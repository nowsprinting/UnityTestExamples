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
    /// ヘルプ画面のページオブジェクト.
    /// ページオブジェクトパターンの実装例
    /// </summary>
    [SuppressMessage("ReSharper", "MemberCanBeMadeStatic.Global")]
    public class HelpPage
    {
        public static async UniTask<HelpPage> Load()
        {
            await SceneManager.LoadSceneAsync("Help");
            return new HelpPage();
        }

        public bool IsShown()
        {
            return SceneManager.GetActiveScene().name == "Help";
        }

        public async UniTask<MainMenuPage> Close()
        {
            var button = GameObject.Find("Close").GetComponent<Button>();
            button.OnPointerClick(new PointerEventData(EventSystem.current));
            await UniTask.NextFrame();
            return new MainMenuPage();
        }
    }
}
