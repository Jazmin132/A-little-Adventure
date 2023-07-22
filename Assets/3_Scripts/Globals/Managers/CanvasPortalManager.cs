using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasPortalManager : MonoBehaviour
{
    public TextDisplay CurrencyPrefab;
    public static CanvasPortalManager instance;

    void Awake()
    {
        instance = this;
    }
    public void InstantiateText(Portal P)
    {
        var TEXT = Instantiate(CurrencyPrefab);
        TEXT.SetOwner(P);
    }
}
