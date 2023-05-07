using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Config : MonoBehaviour
{
    [SerializeField] Transform _MainGame;//Padre de los objetos en pantalla
    [SerializeField] ScreenUI _ScreenPause;

    void Start()
    {
        ScreenManager.instance.Push(new ScreenGo(_MainGame));
    }

    public void Pause()
    {
        ScreenManager.instance.Push(_ScreenPause);
    }
}
