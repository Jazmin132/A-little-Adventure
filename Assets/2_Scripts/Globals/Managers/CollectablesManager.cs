using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectablesManager : MonoBehaviour
{
    [SerializeField] Text Tuercas;
    //[SerializeField] Text Cristal;
    [SerializeField] int TotalTuercas;
    [SerializeField] int TotalCristals;
    public static CollectablesManager instance;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void AddTuerca(int value)
    {
        TotalTuercas += value;
        Tuercas.text = "Tuercas : " + TotalTuercas.ToString();
        //Debug.Log("Here comes the money : " + TotalTuercas);
    }
    public void AddCritales(int value)
    {
        TotalCristals += value;
        //Cristal.text = "Cristales: " + TotalCristals.ToString();
    }
}
