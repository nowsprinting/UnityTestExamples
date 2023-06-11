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
    /// メインメニュー画面のページオブジェクト.
    /// ページオブジェクトパターンの実装例
    /// </summary>
    [SuppressMessage("ReSharper", "MemberCanBeMadeStatic.Global")]
    public class MainMenuPage
    {
        public static async UniTask<MainMenuPage> Load()
        {
            await SceneManager.LoadSceneAsync("MainMenu");
            return new MainMenuPage();
        }

        public bool IsShown()
        {
            return SceneManager.GetActiveScene().name == "MainMenu";
        }

        public async UniTask<DifficultyPage> DifficultySelect()
        {
            var button = GameObject.Find("DifficultySelect").GetComponent<Button>();
            button.OnPointerClick(new PointerEventData(EventSystem.current));
            await UniTask.NextFrame();
            return new DifficultyPage();
        }

        public async UniTask<HelpPage> Help()
        {
            var button = GameObject.Find("Help").GetComponent<Button>();
            button.OnPointerClick(new PointerEventData(EventSystem.current));
            await UniTask.NextFrame();
            return new HelpPage();
        }
    }
}
