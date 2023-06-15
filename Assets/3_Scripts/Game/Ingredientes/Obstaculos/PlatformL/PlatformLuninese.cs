using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformLuninese : Ingredient
{
    public Material[] colorchange;
    public int count = 0;
    Renderer _renderer;
    PlatformLumineseHandler _Handler;
    bool _HasWon = false;

    private void Start()
    {
        _Handler = GetComponentInParent<PlatformLumineseHandler>();
        _renderer = GetComponentInParent<Renderer>();

        _renderer.material = colorchange[0];
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<PlayerM>() && _HasWon == false)
        {
            count++;
            Activate();
        }
    }
    public override void Activate()
    {
        count = Mathf.Min(count, colorchange.Length - 1);
        _renderer.material = colorchange[count];

        _Handler.Check();
    }
    public void SetPermanentColor(int number)
    {
        _HasWon = true;
        _renderer.material = colorchange[number];
    }
    public void ResetColor()
    {
        count = 0;
        _renderer.material = colorchange[count];
    }
    
}
