#if UNITY_EDITOR
using System;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEditor;

public static class DebugToolEditor
{
    [UnityEditor.Callbacks.OnOpenAsset()]
    private static bool OnOpenDebugLog(int instanceID, int line)
    {
        // 콘솔에서 선택된 로그 메시지의 객체 확인
        var obj = EditorUtility.InstanceIDToObject(instanceID);
        if (obj == null)
            return false;

        // 콘솔 창 인스턴스 가져오기
        var assembly = Assembly.GetAssembly(typeof(EditorWindow));
        var consoleWindowType = assembly.GetType("UnityEditor.ConsoleWindow");
        var consoleWindowField = consoleWindowType.GetField("ms_ConsoleWindow", BindingFlags.Static | BindingFlags.NonPublic);
        var consoleWindowInstance = consoleWindowField.GetValue(null);
        if (consoleWindowInstance == null || consoleWindowInstance != EditorWindow.focusedWindow)
            return false;

        // 콘솔 창에서 활성화된 텍스트(스택 트레이스) 가져오기
        var activeTextField = consoleWindowType.GetField("m_ActiveText", BindingFlags.Instance | BindingFlags.NonPublic);
        string activeTextValue = activeTextField.GetValue(consoleWindowInstance)?.ToString();
        if (string.IsNullOrEmpty(activeTextValue))
            return false;

        // 스택 트레이스에서 (at 경로:라인번호) 패턴 찾기
        MatchCollection matches = Regex.Matches(activeTextValue, @"\(at (.+?):(\d+)\)");
        foreach (Match match in matches)
        {
            string filePath = match.Groups[1].Value;
            int lineNumber = int.Parse(match.Groups[2].Value);

            // DebugTool.cs가 아닌 첫 번째 프레임 찾기
            if (!filePath.Contains("DebugTool.cs"))
            {
                string fullPath = Application.dataPath.Substring(0, Application.dataPath.LastIndexOf("Assets")) + filePath;
                UnityEditorInternal.InternalEditorUtility.OpenFileAtLineExternal(fullPath, lineNumber);
                return true;
            }
        }

        return false;
    }
}
#endif
