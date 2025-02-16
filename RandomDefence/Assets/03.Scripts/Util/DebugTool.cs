using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public static class DebugTool
{
    public static bool isDebug = true;

    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void Log(object msg)
    {
        if (!isDebug)
            return;
        Debug.Log(msg.ToString());
    }

    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void LogWarning(object msg)
    {
        if (!isDebug)
            return;
        Debug.LogWarning(msg.ToString());
    }

    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void LogError(object msg)
    {
        if (!isDebug)
            return;
        Debug.LogError(msg.ToString());
    }

    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void LogFormat(object msg)
    {
        if (!isDebug)
            return;
        Debug.LogFormat(msg.ToString());
    }

    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void LogException(Exception exception)
    {
        if (!isDebug)
            return;
        Debug.LogException(exception);
    }

    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void LogException(Exception exception, UnityEngine.Object context)
    {
        if (!isDebug)
            return;
        Debug.LogException(exception, context);
    }

    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void DrawLine(Vector3 start, Vector3 end)
    {
        if (!isDebug)
            return;
        Debug.DrawLine(start, end);
    }

    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void DrawLine(Vector3 start, Vector3 end, Color color)
    {
        if (!isDebug)
            return;
        Debug.DrawLine(start, end, color);
    }

    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void DrawLine(Vector3 start, Vector3 end, Color color, float duration)
    {
        if (!isDebug)
            return;
        Debug.DrawLine(start, end, color, duration);
    }

    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void DrawRay(Vector3 start, Vector3 end)
    {
        if (!isDebug)
            return;
        Debug.DrawRay(start, end);
    }

    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void DrawRay(Vector3 start, Vector3 end, Color color)
    {
        if (!isDebug)
            return;
        Debug.DrawRay(start, end, color);
    }

    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void DrawRay(Vector3 start, Vector3 end, Color color, float duration)
    {
        if (!isDebug)
            return;
        Debug.DrawRay(start, end, color, duration);
    }
}
