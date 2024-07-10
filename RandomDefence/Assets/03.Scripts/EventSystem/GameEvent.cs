using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameEvent : ScriptableObject
{
    private List<GameEventListener> listeners = new List<GameEventListener>();

    // �̺�Ʈ Ʈ���Ű� �������鿡�� ����(Notify)�ϱ� ���� �Լ�.
    public void Raise()
    {
        if (listeners.Count == 0)
            return;

        for (int i = listeners.Count - 1; i >= 0; i--)
            // �̺�Ʈ�� ���� ���� ���������� �ݹ� ȣ��. 
            listeners[i].OnEventRaised();
    }

    // �������� �̺�Ʈ ��� �Լ�
    public void RegisterListener(GameEventListener listener)
    {
        listeners.Add(listener);
    }
    // �������� �̺�Ʈ ��� ��� �Լ�
    public void UnregisterListener(GameEventListener listener)
    {
        listeners.Remove(listener);
    }
}
