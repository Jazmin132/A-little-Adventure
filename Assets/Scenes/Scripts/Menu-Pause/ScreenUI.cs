using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenUI : MonoBehaviour, IScreen
{
    Button[] _buttons;

    void Awake()
    {
        _buttons = GetComponentsInChildren<Button>();
    }

    public void Activate()
    {
        if (_buttons == null)
            _buttons = GetComponentsInChildren<Button>();
        
        foreach (var button in _buttons)
            button.interactable = true;

        gameObject.SetActive(true);
    }

    public void DeActivate()
    {
        foreach (var button in _buttons)
            button.interactable = false;
    }

    public void Free()
    {
        Destroy(gameObject);
    }
}
