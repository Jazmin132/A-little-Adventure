using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectablesManager : MonoBehaviour
{
    [SerializeField] Text Tuercas;
    [SerializeField] int TotalTuercas;
    [SerializeField] int TotalCurrency;
    public static CollectablesManager instance;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        if (PlayerPrefs.HasKey("Total Currency"))
            TotalCurrency = PlayerPrefs.GetInt("Total Currency");
        else
        {
            TotalCurrency = 0;
            PlayerPrefs.SetInt("Total Currency", TotalCurrency);
        }
    }
    public void AddTuerca(int value)
    {
        TotalTuercas += value;
        Tuercas.text = "Tuercas : " + TotalTuercas.ToString();
        //Debug.Log("Here comes the money : " + TotalTuercas);
        AddTotalTuercas(TotalTuercas);
    }
    void AddTotalTuercas(int Totalvalue)
    {
        TotalCurrency += Totalvalue;
        PlayerPrefs.SetInt("Total Currency", TotalCurrency);
    }
    private void ResetAll()
    {
        PlayerPrefs.SetInt("Total Currency", 0);
    }
}
