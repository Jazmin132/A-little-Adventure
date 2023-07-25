using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectablesManager : MonoBehaviour
{
    [SerializeField] Text Tuercas;
    [SerializeField] int TuercasCurrent;
    [SerializeField] int VitalesCurrent;
    int TotalTuercas;
    int TotalVitales;
    [SerializeField] bool ResetAll;
    public static CollectablesManager instance;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
    private void Start()
    {
        if (PlayerPrefs.HasKey("Total Tuercas") || PlayerPrefs.HasKey("Total Vitales"))
        {
            TotalTuercas = PlayerPrefs.GetInt("Total Tuercas");
            TotalVitales = PlayerPrefs.GetInt("Total Vitales");
           // Debug.Log("TotalTuercas : " + TotalTuercas);
           // Debug.Log("TotalVitales : " + TotalVitales);
        }
        else 
            StablishCurrency();

        if (ResetAll) StablishCurrency();
    }
    public void AddTuerca(int valueT)
    {
        TuercasCurrent += valueT;
        Tuercas.text = "Tuercas : " + TuercasCurrent.ToString();
        AddTotalTuercas(TuercasCurrent);
    }
    void AddTotalTuercas(int Totalvalue)
    {
        TotalTuercas += Totalvalue;
        PlayerPrefs.SetInt("Total Tuercas", TotalTuercas);
    }

    public void AddVital(int valueV)
    {
        VitalesCurrent += valueV;
        AddTotalVital(VitalesCurrent);
    }
    void AddTotalVital(int Totalvalue)
    {
        TotalVitales += Totalvalue;
        PlayerPrefs.SetInt("Total Vitales", TotalVitales);
    }

    private void StablishCurrency()
    {
        TotalTuercas = 0;
        TotalVitales = 0;
        PlayerPrefs.SetInt("Total Tuercas", TotalTuercas);
        PlayerPrefs.SetInt("Total Vitales", TotalVitales);
    }
}
