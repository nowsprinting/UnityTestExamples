// Copyright (c) 2021-2023 Koji Hasegawa.
// This software is released under the MIT License.

using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor.SceneManagement;
#endif

namespace UGUIExample
{
    /// <summary>
    /// Canvas上のUI要素を操作する例
    /// </summary>
    [TestFixture]
    [UnityPlatform(RuntimePlatform.OSXEditor, RuntimePlatform.WindowsEditor, RuntimePlatform.LinuxEditor)]
    public class UIElementTest
    {
        private const string SandboxScenePath = "Assets/UGUIExample/Tests/Scenes/UIElementExample.unity";

        [Test]
        public async Task Buttonをクリック_ReceiveOnClickが呼ばれること()
        {
#if UNITY_EDITOR
            // テスト対象が配置されたSceneのロード
            await EditorSceneManager.LoadSceneAsyncInPlayMode(
                SandboxScenePath,
                new LoadSceneParameters(LoadSceneMode.Single));
#endif
            // 操作対象を検索して取得
            var button = GameObject.Find("Button").GetComponent<Button>();

            // 操作対象のスクリーン座標を取得してEventDataにセット
            var eventData = new PointerEventData(EventSystem.current);
            eventData.position = GetScreenPoint(button.gameObject);

            // クリックイベントを発生させる
            button.OnPointerClick(eventData);

            // 動作結果を検証（画面遷移や表示の変化など。本例のような「クリックされたという事象」ではなく、クリックの結果どうなるかを検証します）
            LogAssert.Expect(LogType.Log, $"Button.ReceiveOnClick");
        }

        /// <summary>
        /// GameObjectのワールド座標をスクリーン座標に変換して返す
        /// UI要素専用で、2D/3D要素は考慮していない
        /// </summary>
        /// <param name="gameObject">操作対象のGameObject</param>
        /// <returns>スクリーン座標</returns>
        private static Vector2 GetScreenPoint(GameObject gameObject)
        {
            var canvas = gameObject.GetComponentInParent<Canvas>();
            if (!canvas.isRootCanvas)
            {
                canvas = canvas.rootCanvas;
            }

            if (canvas.renderMode == RenderMode.ScreenSpaceOverlay)
            {
                return RectTransformUtility.WorldToScreenPoint(null, gameObject.transform.position);
            }

            return RectTransformUtility.WorldToScreenPoint(canvas.worldCamera, gameObject.transform.position);
        }
    }
}
