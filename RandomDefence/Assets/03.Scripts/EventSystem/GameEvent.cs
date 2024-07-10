using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameEvent : ScriptableObject
{
    private List<GameEventListener> listeners = new List<GameEventListener>();

    // 이벤트 트리거가 옵저버들에게 전파(Notify)하기 위한 함수.
    public void Raise()
    {
        if (listeners.Count == 0)
            return;

        for (int i = listeners.Count - 1; i >= 0; i--)
            // 이벤트를 구독 중인 옵저버들의 콜백 호출. 
            listeners[i].OnEventRaised();
    }

    // 옵저버의 이벤트 등록 함수
    public void RegisterListener(GameEventListener listener)
    {
        listeners.Add(listener);
    }
    // 옵저버의 이벤트 등록 취소 함수
    public void UnregisterListener(GameEventListener listener)
    {
        listeners.Remove(listener);
    }
}
