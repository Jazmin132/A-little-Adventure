using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWin : ScreenUI
{
    [SerializeField] GameObject _Lose;
    public void BTN_Back()
    {
        ScreenManager.instance.Pop();
    }
    public void BTN_CheckPoint()
    {
        _Lose.active = false;
    }
}
