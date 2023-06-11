// Copyright (c) 2021-2023 Koji Hasegawa.
// This software is released under the MIT License.

using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
#if UNITY_EDITOR
using UnityEditor.SceneManagement;
#endif

namespace UGUIExample
{
    /// <summary>
    /// EventTriggerをアタッチした3D要素を操作する例
    /// </summary>
    /// <remarks>
    /// 2Dは省略していますがコードは同じです。
    /// 2D/3D要素であっても、EventTriggerでなくIEventSystemHandlerのサブインターフェイスを使用している場合はUIElementTest.csと同じ方式で操作できます
    /// </remarks>
    [TestFixture]
    [UnityPlatform(RuntimePlatform.OSXEditor, RuntimePlatform.WindowsEditor, RuntimePlatform.LinuxEditor)]
    public class UsingEventTriggerTest
    {
        private const string SandboxScenePath = "Assets/UGUIExample/Tests/Scenes/UsingEventTriggerExample.unity";

        [SetUp]
        public async Task SetUp()
        {
#if UNITY_EDITOR
            await EditorSceneManager.LoadSceneAsyncInPlayMode(
                SandboxScenePath,
                new LoadSceneParameters(LoadSceneMode.Single));
#endif
        }

        [Test]
        public void ClickableCubeをクリック_ReceiveOnClickが呼ばれること()
        {
            var eventTrigger = GameObject.Find("ClickableCube").GetComponent<EventTrigger>();
            var eventData = new PointerEventData(EventSystem.current);
            eventTrigger.OnPointerClick(eventData); // ボタンをクリック

            LogAssert.Expect(LogType.Log, $"ClickableCube.ReceiveOnClick");
        }

        [Test]
        public void リスナが登録されていないイベントを呼んでみる_エラーは出ない()
        {
            var eventTrigger = GameObject.Find("ClickableCube").GetComponent<EventTrigger>();
            var eventData = new PointerEventData(EventSystem.current);
            eventTrigger.OnBeginDrag(eventData); // リスナを登録していないイベント
        }

        /// <summary>
        /// GameObjectのワールド座標をスクリーン座標に変換して返す
        /// 2D/3D要素専用で、UI要素は考慮していない
        /// </summary>
        /// <param name="gameObject">操作対象のGameObject</param>
        /// <returns>スクリーン座標</returns>
        private static Vector2 GetScreenPoint(GameObject gameObject) // TODO: Buttonでは使わないが一時的に残しておく
        {
            return RectTransformUtility.WorldToScreenPoint(Camera.main, gameObject.transform.position);
        }
    }
}
