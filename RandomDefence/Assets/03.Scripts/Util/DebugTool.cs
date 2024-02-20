using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class DebugTool
{
    public static bool isDebug = true;

    public static void Log(object msg)
    {
        if (isDebug == false)
            return;

        Debug.Log(msg.ToString());
    }
    public static void LogWarning(object msg)
    {
        if (isDebug == false)
            return;

        Debug.LogWarning(msg.ToString());
    }
    public static void LogError(object msg)
    {
        if (isDebug == false)
            return;

        Debug.LogError(msg.ToString());
    }
    public static void LogFormat(object msg)
    {
        if (isDebug == false)
            return;

        Debug.LogFormat(msg.ToString());
    }

    public static void LogException(Exception exception)
    {
        if (isDebug == false)
            return;

        Debug.LogException(exception);
    }
    public static void LogException(Exception exception, UnityEngine.Object context)
    {
        if (isDebug == false)
            return;

        Debug.LogException(exception, context);
    }

    public static void DrawLine(Vector3 start, Vector3 end)
    {
        if (isDebug == false)
            return;

        Debug.DrawLine(start, end);
    }
    public static void DrawLine(Vector3 start, Vector3 end, Color color)
    {
        if (isDebug == false)
            return;

        Debug.DrawLine(start, end, color);
    }
    public static void DrawLine(Vector3 start, Vector3 end, Color color, float duration)
    {
        if (isDebug == false)
            return;

        Debug.DrawLine(start, end, color, duration);
    }


    public static void DrawRay(Vector3 start, Vector3 end)
    {
        if (isDebug == false)
            return;

        Debug.DrawRay(start, end);
    }
    public static void DrawRay(Vector3 start, Vector3 end, Color color)
    {
        if (isDebug == false)
            return;

        Debug.DrawRay(start, end, color);
    }
    public static void DrawRay(Vector3 start, Vector3 end, Color color, float duration)
    {
        if (isDebug == false)
            return;

        Debug.DrawRay(start, end, color, duration);
    }
}

