using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Plane : MonoBehaviour
{
    [SerializeField] int _PartsRequired;
    public Text CurrencyDisplay;
    int _TotalParts;

    void Start()
    {
        CanvasPortalManager.instance.InstantiatePlaneText(this);
        ShowHowMuch();
    }
    void ShowHowMuch()
    {
        _TotalParts = PlayerPrefs.GetInt("Total Piezas");
        CurrencyDisplay.text = _TotalParts.ToString() + " / " + _PartsRequired.ToString();
    }
}
