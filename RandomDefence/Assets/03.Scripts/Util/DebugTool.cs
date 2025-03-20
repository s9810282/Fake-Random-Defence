using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEngine;
using System.Diagnostics;


#if UNITY_EDITOR
using UnityEditor;
#endif

public static class DebugTool
{
    public static bool isDebug = true;

    private static string FormatMessage(object message)
    {
        StackFrame frame = new StackTrace(2, true).GetFrame(0); // 호출 원본 코드 위치 가져오기
        string fileName = frame.GetFileName();
        int lineNumber = frame.GetFileLineNumber();

        return $"[{fileName}:{lineNumber}] {message}";
    }

    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void Log(object msg)
    {
        if (!isDebug)
            return;
        UnityEngine.Debug.Log(msg.ToString());
    }

    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void LogWarning(object msg)
    {
        if (!isDebug)
            return;
        UnityEngine.Debug.LogWarning(msg.ToString());
    }

    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void LogError(object msg)
    {
        if (!isDebug)
            return;
        UnityEngine.Debug.LogError(msg.ToString());
    }

    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void LogFormat(object msg)
    {
        if (!isDebug)
            return;
        UnityEngine.Debug.LogFormat(msg.ToString());
    }

    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void LogException(Exception exception)
    {
        if (!isDebug)
            return;
        UnityEngine.Debug.LogException(exception);
    }

    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void LogException(Exception exception, UnityEngine.Object context)
    {
        if (!isDebug)
            return;
        UnityEngine.Debug.LogException(exception, context);
    }

    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void DrawLine(Vector3 start, Vector3 end)
    {
        if (!isDebug)
            return;
        UnityEngine.Debug.DrawLine(start, end);
    }

    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void DrawLine(Vector3 start, Vector3 end, Color color)
    {
        if (!isDebug)
            return;
        UnityEngine.Debug.DrawLine(start, end, color);
    }

    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void DrawLine(Vector3 start, Vector3 end, Color color, float duration)
    {
        if (!isDebug)
            return;
        UnityEngine.Debug.DrawLine(start, end, color, duration);
    }

    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void DrawRay(Vector3 start, Vector3 end)
    {
        if (!isDebug)
            return;
        UnityEngine.Debug.DrawRay(start, end);
    }

    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void DrawRay(Vector3 start, Vector3 end, Color color)
    {
        if (!isDebug)
            return;
        UnityEngine.Debug.DrawRay(start, end, color);
    }

    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void DrawRay(Vector3 start, Vector3 end, Color color, float duration)
    {
        if (!isDebug)
            return;
        UnityEngine.Debug.DrawRay(start, end, color, duration);
    }



}
