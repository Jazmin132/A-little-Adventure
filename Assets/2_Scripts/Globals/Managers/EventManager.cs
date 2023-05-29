using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EventEnun
{
    pause,
    play,
    load,
}

public class EventManager : MonoBehaviour
{
    public static Dictionary<System.Enum, EventGeneric> events = new Dictionary<System.Enum, EventGeneric>();
        
    public void OnDestroy()
    {
        events.Clear();
    }
}
public class EventGeneric
{
    public delegate void _Event(params object[] param);
    public event _Event action;

    public virtual void Execute(params object[] param)
    {
        action?.Invoke(param);
    }
}
