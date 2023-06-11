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
    /// 難易度設定画面のページオブジェクト.
    /// ページオブジェクトパターンの実装例
    /// </summary>
    [SuppressMessage("ReSharper", "MemberCanBeMadeStatic.Global")]
    public class DifficultyPage
    {
        public static async UniTask<DifficultyPage> Load()
        {
            await SceneManager.LoadSceneAsync("Difficulty");
            return new DifficultyPage();
        }

        public bool IsShown()
        {
            return SceneManager.GetActiveScene().name == "Difficulty";
        }

        public async UniTask<MainMenuPage> Easy()
        {
            var button = GameObject.Find("Easy").GetComponent<Button>();
            button.OnPointerClick(new PointerEventData(EventSystem.current));
            await UniTask.NextFrame();
            return new MainMenuPage();
        }

        public async UniTask<MainMenuPage> Normal()
        {
            var button = GameObject.Find("Normal").GetComponent<Button>();
            button.OnPointerClick(new PointerEventData(EventSystem.current));
            await UniTask.NextFrame();
            return new MainMenuPage();
        }

        public async UniTask<MainMenuPage> Hard()
        {
            var button = GameObject.Find("Hard").GetComponent<Button>();
            button.OnPointerClick(new PointerEventData(EventSystem.current));
            await UniTask.NextFrame();
            return new MainMenuPage();
        }
    }
}
