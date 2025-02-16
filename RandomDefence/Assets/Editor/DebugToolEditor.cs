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
        // �ֿܼ��� ���õ� �α� �޽����� ��ü Ȯ��
        var obj = EditorUtility.InstanceIDToObject(instanceID);
        if (obj == null)
            return false;

        // �ܼ� â �ν��Ͻ� ��������
        var assembly = Assembly.GetAssembly(typeof(EditorWindow));
        var consoleWindowType = assembly.GetType("UnityEditor.ConsoleWindow");
        var consoleWindowField = consoleWindowType.GetField("ms_ConsoleWindow", BindingFlags.Static | BindingFlags.NonPublic);
        var consoleWindowInstance = consoleWindowField.GetValue(null);
        if (consoleWindowInstance == null || consoleWindowInstance != EditorWindow.focusedWindow)
            return false;

        // �ܼ� â���� Ȱ��ȭ�� �ؽ�Ʈ(���� Ʈ���̽�) ��������
        var activeTextField = consoleWindowType.GetField("m_ActiveText", BindingFlags.Instance | BindingFlags.NonPublic);
        string activeTextValue = activeTextField.GetValue(consoleWindowInstance)?.ToString();
        if (string.IsNullOrEmpty(activeTextValue))
            return false;

        // ���� Ʈ���̽����� (at ���:���ι�ȣ) ���� ã��
        MatchCollection matches = Regex.Matches(activeTextValue, @"\(at (.+?):(\d+)\)");
        foreach (Match match in matches)
        {
            string filePath = match.Groups[1].Value;
            int lineNumber = int.Parse(match.Groups[2].Value);

            // DebugTool.cs�� �ƴ� ù ��° ������ ã��
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
