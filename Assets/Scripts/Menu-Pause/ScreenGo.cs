using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenGo : IScreen
{
    Dictionary<Behaviour, bool> _before;

    Transform _root;

    public ScreenGo(Transform root)
    {
        _root = root;
        _before = new Dictionary<Behaviour, bool>();
    }

    public void Activate()
    {
        foreach (var KeyValue in _before)
            KeyValue.Key.enabled = KeyValue.Value;
    }

    public void DeActivate()
    {
        foreach ( var b in _root.GetComponentsInChildren<Behaviour>())
        {
            _before[b] = b.enabled;
            b.enabled = false;
        }
    }

    public void Free()
    {
        GameObject.Destroy(_root.gameObject);
    }
}
