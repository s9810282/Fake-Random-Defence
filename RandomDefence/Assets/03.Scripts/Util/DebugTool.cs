using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;


public static class DebugTool
{
    public static bool isDebug = true;

    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void Log(object msg)
    {
        if (isDebug == false)
            return;

        Debug.Log(msg.ToString());
    }

    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void LogWarning(object msg)
    {
        if (isDebug == false)
            return;

        Debug.LogWarning(msg.ToString());
    }

    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void LogError(object msg)
    {
        if (isDebug == false)
            return;

        Debug.LogError(msg.ToString());
    }

    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void LogFormat(object msg)
    {
        if (isDebug == false)
            return;

        Debug.LogFormat(msg.ToString());
    }


    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void LogException(Exception exception)
    {
        if (isDebug == false)
            return;

        Debug.LogException(exception);
    }

    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void LogException(Exception exception, UnityEngine.Object context)
    {
        if (isDebug == false)
            return;

        Debug.LogException(exception, context);
    }

    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void DrawLine(Vector3 start, Vector3 end)
    {
        if (isDebug == false)
            return;

        Debug.DrawLine(start, end);
    }

    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void DrawLine(Vector3 start, Vector3 end, Color color)
    {
        if (isDebug == false)
            return;

        Debug.DrawLine(start, end, color);
    }

    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void DrawLine(Vector3 start, Vector3 end, Color color, float duration)
    {
        if (isDebug == false)
            return;

        Debug.DrawLine(start, end, color, duration);
    }

    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void DrawRay(Vector3 start, Vector3 end)
    {
        if (isDebug == false)
            return;

        Debug.DrawRay(start, end);
    }

    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void DrawRay(Vector3 start, Vector3 end, Color color)
    {
        if (isDebug == false)
            return;

        Debug.DrawRay(start, end, color);
    }

    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void DrawRay(Vector3 start, Vector3 end, Color color, float duration)
    {
        if (isDebug == false)
            return;

        Debug.DrawRay(start, end, color, duration);
    }




    [UnityEditor.Callbacks.OnOpenAsset()]
    private static bool OnOpenDebugLog(int instance, int line)
    {
        string name = EditorUtility.InstanceIDToObject(instance).name;
        if (!name.Equals("Debug")) return false;

        // ������ �ܼ� �������� �ν��Ͻ��� ã�´�.
        var assembly = Assembly.GetAssembly(typeof(EditorWindow));
        if (assembly == null) return false;

        var consoleWindowType = assembly.GetType("UnityEditor.ConsoleWindow");
        if (consoleWindowType == null) return false;

        var consoleWindowField = consoleWindowType.GetField("ms_ConsoleWindow", BindingFlags.Static | BindingFlags.NonPublic);
        if (consoleWindowField == null) return false;

        var consoleWindowInstance = consoleWindowField.GetValue(null);
        if (consoleWindowInstance == null) return false;

        if (consoleWindowInstance != (object)EditorWindow.focusedWindow) return false;

        // �ܼ� ������ �ν��Ͻ��� Ȱ��ȭ�� �ؽ�Ʈ�� ã�´�.
        var activeTextField = consoleWindowType.GetField("m_ActiveText", BindingFlags.Instance | BindingFlags.NonPublic);
        if (activeTextField == null) return false;

        string activeTextValue = activeTextField.GetValue(consoleWindowInstance).ToString();
        if (string.IsNullOrEmpty(activeTextValue)) return false;

        // ����� �α׸� ȣ���� ���� ��θ� ã�� ������� ����.
        Match match = Regex.Match(activeTextValue, @"\(at (.+)\)");
        if (match.Success) match = match.NextMatch(); // stack trace�� ù��°�� �ǳʶڴ�.

        if (match.Success)
        {
            string path = match.Groups[1].Value;
            var split = path.Split(':');
            string filePath = split[0];
            int lineNum = Convert.ToInt32(split[1]);

            string dataPath = UnityEngine.Application.dataPath.Substring(0, UnityEngine.Application.dataPath.LastIndexOf("Assets"));
            UnityEditorInternal.InternalEditorUtility.OpenFileAtLineExternal(dataPath + filePath, lineNum);
            return true;
        }
        return false;
    }
    
}

