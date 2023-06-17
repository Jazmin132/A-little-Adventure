using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformLuninese : Ingredient
{
    //public Material[] colorchange;
    public int count = 0;
    Renderer _RenderMat;
    public GameObject M;
    //Renderer _renderer;
    PlatformLumineseHandler _Handler;
    bool _HasWon = false;

    private void Start()
    {
        GameManager.instance.SubscribeBehaviours(this);
        _Handler = GetComponentInParent<PlatformLumineseHandler>();
        //_renderer = GetComponentInParent<Renderer>();

        _RenderMat = M.GetComponent<Renderer>();
        _RenderMat.material.SetFloat("Vector1_f4e6155ccc7242fd891606cc4eea181c", 0);
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
        count = Mathf.Min(count, 2);//colorchange.Length - 1
        _RenderMat.material.SetFloat("Vector1_f4e6155ccc7242fd891606cc4eea181c", count);
        _Handler.Check();
    }
    public void SetPermanentColor(int number)
    {
        _HasWon = true;
        _RenderMat.material.SetFloat("Vector1_f4e6155ccc7242fd891606cc4eea181c", number);
    }
    public void ResetColor()
    {
        count = 0;
        _RenderMat.material.SetFloat("Vector1_f4e6155ccc7242fd891606cc4eea181c", count);
    }
    
}
