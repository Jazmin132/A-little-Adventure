using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectablesManager : MonoBehaviour
{
    [SerializeField] Text Tuercas;
    [SerializeField] int TuercasCurrent;
    [SerializeField] int PiezasCurrent;
    int TotalTuercas;
    int TotalPiezas;
    [SerializeField] bool ResetAll;
    public static CollectablesManager instance;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
    private void Start()
    {
        if (ResetAll) StablishCurrency();

        if (PlayerPrefs.HasKey("Total Tuercas") || PlayerPrefs.HasKey("Total Piezas"))
        {
            TotalTuercas = PlayerPrefs.GetInt("Total Tuercas");
            TotalPiezas = PlayerPrefs.GetInt("Total Piezas");
            Debug.Log("TotalTuercas : " + TotalTuercas);
            Debug.Log("TotalVitales : " + TotalPiezas);
        }
        else 
            StablishCurrency();
    }
    public void AddTuerca(int valueT)
    {
        TuercasCurrent += valueT;
        Tuercas.text = "Tuercas : " + TuercasCurrent.ToString();
        AddTotalTuercas(valueT);
    }
    void AddTotalTuercas(int Totalvalue)
    {
        TotalTuercas += Totalvalue;
        PlayerPrefs.SetInt("Total Tuercas", TotalTuercas);
    }
    public void RemovePoints(int minus)
    {
        TuercasCurrent -= minus;
        if (TuercasCurrent <= 0)
            TuercasCurrent = 0;

        Tuercas.text = "Tuercas : " + TuercasCurrent.ToString();
    }
    public void AddVital(int valueV)
    {
        PiezasCurrent += valueV;
        AddTotalVital(valueV);
    }
    void AddTotalVital(int Totalvalue)
    {
        TotalPiezas += Totalvalue;
        PlayerPrefs.SetInt("Total Piezas", TotalPiezas);
    }

    private void StablishCurrency()
    {
        TotalTuercas = 0;
        TotalPiezas = 0;
        PlayerPrefs.SetInt("Total Tuercas", TotalTuercas);
        PlayerPrefs.SetInt("Total Piezas", TotalPiezas);
    }
}
