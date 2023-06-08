using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformLuninese : Ingredient
{
    public Material[] colorchange;
    Mesh _myMesh;
    Material _MyColor;
    public int count;
    PlatformLumineseHandler _Handler;

    private void Start()
    {
        _Handler = GetComponentInParent<PlatformLumineseHandler>();

        _myMesh = GetComponent<Mesh>();//????????
        _MyColor = GetComponent<Material>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerM>())
        {
            Activate();
            count++;
        }
    }

    public override void Activate()
    {//Como hacer que cambie de color?
        count = Mathf.Min(count, colorchange.Length);
        _MyColor = colorchange[count];
        _Handler.Check();
    }

    public void SetPermanentColor(int number)
    {
        _MyColor = colorchange[number];
    }

    public void ResetColor()
    {
        _MyColor = colorchange[0];
    }
    
}
