using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWin : ScreenUI
{
    public void BTN_Back()
    {
        ScreenManager.instance.Pop();
    }
}
