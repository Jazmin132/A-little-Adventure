using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenPause : ScreenUI
{
    public ScreenOptions _ScreenOptions;

    public void BTN_Options()
    {
        ScreenManager.instance.Push(_ScreenOptions);
    }

    public void BTN_Back()
    {
        ScreenManager.instance.Pop();
    }
}
