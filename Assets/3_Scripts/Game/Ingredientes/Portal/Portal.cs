using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Portal : MonoBehaviour
{
    [SerializeField] string _NextLevel;
    [SerializeField] bool _RequiresMoney;
    [SerializeField] int _MoneyRequired;
    public Text CurrencyDisplay;
    int _TotalTuercas;

    void Start()
    {
        if (_RequiresMoney)
        {
            CanvasPortalManager.instance.InstantiateText(this);
            ShowHowMuch();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!_RequiresMoney)
            ScenesManager.instance.GotoLevel(_NextLevel);
        else if (_RequiresMoney && _MoneyRequired <= _TotalTuercas) 
            ScenesManager.instance.GotoLevel(_NextLevel);
    }
    void ShowHowMuch()
    {
        _TotalTuercas = PlayerPrefs.GetInt("Total Tuercas");
        //     Muestro la plata que tengo / la plata que necesito
        CurrencyDisplay.text = _TotalTuercas.ToString() + " / " + _MoneyRequired.ToString();
    }
}

