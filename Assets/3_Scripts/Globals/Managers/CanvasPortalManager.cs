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
    public void InstantiateText(Portal portal)
    {
        var TEXT = Instantiate(CurrencyPrefab);
        TEXT.SetOwner(portal);
    }
    public void InstantiatePlaneText(Plane plane)
    {
        var TEXT = Instantiate(CurrencyPrefab);
        TEXT.SetPlaneOwner(plane);
    }
}
