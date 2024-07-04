using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    // 등록할 이벤트
    public GameEvent Event;
    // 리스너(콜백)
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