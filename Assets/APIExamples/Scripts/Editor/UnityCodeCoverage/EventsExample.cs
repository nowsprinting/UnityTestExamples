// Copyright (c) 2021 Koji Hasegawa.
// This software is released under the MIT License.

using UnityEditor;
using UnityEditor.TestTools.CodeCoverage;

namespace APIExamples.Editor.UnityCodeCoverage
{
    /// <summary>
    /// Code CoverageパッケージのコールバックAPI使用例
    /// <see href="https://docs.unity3d.com/Packages/com.unity.testtools.codecoverage@1.1/api/UnityEditor.TestTools.CodeCoverage.Events.html"/>
    /// </summary>
    public static class EventsExample
    {
        [InitializeOnLoadMethod]
        private static void SetupListeners()
        {
            Events.onCoverageSessionStarted += OnSessionStarted;
            Events.onCoverageSessionFinished += OnSessionFinished;
            Events.onCoverageSessionPaused += OnSessionPaused;
            Events.onCoverageSessionUnpaused += OnSessionUnpaused;
        }

        private static void OnSessionStarted(SessionEventInfo args)
        {
            // コードカバレッジセッションが開始されるときに呼ばれます
        }

        private static void OnSessionFinished(SessionEventInfo args)
        {
            // コードカバレッジセッションが終了したときに呼ばれます
        }

        private static void OnSessionPaused(SessionEventInfo args)
        {
            // コードカバレッジセッションが一時停止したときに呼ばれます
        }

        private static void OnSessionUnpaused(SessionEventInfo args)
        {
            // コードカバレッジセッションの一時停止が解除されたときに呼ばれます
        }
    }
}
