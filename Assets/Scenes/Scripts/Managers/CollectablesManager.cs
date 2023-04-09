using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectablesManager : MonoBehaviour
{
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
        Debug.Log("Here comes the money : " + TotalTuercas);
    }
    public void AddCritales(int value)
    {
        TotalCristals += value;
    }
}
