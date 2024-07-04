using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    // ����� �̺�Ʈ
    public GameEvent Event;
    // ������(�ݹ�)
    public UnityEvent Response;

    private void OnEnable()
    {
        Event.RegisterListener(this);
    }
    private void OnDisable()
    {
        Event.UnregisterListener(this);
    }
    public void OnEventRaised()
    {
        Response.Invoke();
    }
}