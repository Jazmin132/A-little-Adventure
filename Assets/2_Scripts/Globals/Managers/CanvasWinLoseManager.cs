using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasWinLoseManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] subMenus;

    [SerializeField]
    GameObject principalCanvas;

    private void Start()
    {
        ScenesManager.instance.canvasManager = this;
    }
    public void ShowSubMenu(string name)
    {
        foreach (var item in subMenus)
        {
            if(name == item.name)
            {
                item.SetActive(true);
                principalCanvas.SetActive(true);
            }
            else
            {
                item.SetActive(false);
            }
        }
    }

    public void CloseSubMenu()
    {
        principalCanvas.SetActive(false);
    }
}
