using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenOptions : ScreenUI
{
    public void BTN_Back()
    {
        ScreenManager.instance.Pop();
    }
}
