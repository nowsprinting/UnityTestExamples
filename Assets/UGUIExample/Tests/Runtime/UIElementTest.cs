// Copyright (c) 2021-2023 Koji Hasegawa.
// This software is released under the MIT License.

using NUnit.Framework;
using TestHelper.Attributes;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace UGUIExample
{
    /// <summary>
    /// Canvas上のUI要素を操作する例
    /// </summary>
    [TestFixture]
    public class UIElementTest
    {
        private const string SandboxScenePath = "Assets/UGUIExample/Tests/Scenes/UIElementExample.unity";

        [Test]
        [LoadScene(SandboxScenePath)]
        public void Buttonをクリック_ReceiveOnClickが呼ばれること()
        {
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
