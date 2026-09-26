// Copyright (c) 2021-2023 Koji Hasegawa.
// This software is released under the MIT License.

using NUnit.Framework;
using TestHelper.Attributes;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.TestTools;

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
    public class UsingEventTriggerTest
    {
        private const string SandboxScenePath = "Assets/UGUIExample/Tests/Scenes/UsingEventTriggerExample.unity";

        [Test]
        [LoadScene(SandboxScenePath)]
        public void ClickableCubeをクリック_ReceiveOnClickが呼ばれること()
        {
            var eventTrigger = GameObject.Find("ClickableCube").GetComponent<EventTrigger>();
            var eventData = new PointerEventData(EventSystem.current);
            eventTrigger.OnPointerClick(eventData); // ボタンをクリック

            LogAssert.Expect(LogType.Log, $"ClickableCube.ReceiveOnClick");
        }

        [Test]
        [LoadScene(SandboxScenePath)]
        public void リスナが登録されていないイベントを呼んでみる_エラーは出ない()
        {
            var eventTrigger = GameObject.Find("ClickableCube").GetComponent<EventTrigger>();
            var eventData = new PointerEventData(EventSystem.current);
            eventTrigger.OnBeginDrag(eventData); // リスナを登録していないイベント
        }
    }
}
