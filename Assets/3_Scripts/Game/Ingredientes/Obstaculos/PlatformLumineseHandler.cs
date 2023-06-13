using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformLumineseHandler : MonoBehaviour
{
    public PlatformLuninese[] Platforms;
    public System.Action Change;
    int count = 0;
    //Como checheo que todas las plataformas estén en el segundo color

    private void Start()
    {
        Platforms = GetComponentsInChildren<PlatformLuninese>();
    }
    public void Check()
    {
        foreach (var platform in Platforms)
        {
            if (platform.count == 1) count++;
            //COMO checkeo que todas las plataformas tiene count 1
        }
        if (count == Platforms.Length) Win();
    }
    private void Win()
    {
        foreach (var platform in Platforms)
        {
            platform.SetPermanentColor(1);
        }
    }
    void ResetPlatforms()
    {
        foreach (var platform in Platforms)
        {
            platform.ResetColor();
        }
    }

    private void OnTriggerEnter(Collider other)
    {//Ponerselo a un botón
        if (other.TryGetComponent(out PlayerM P)) ResetPlatforms();
    }
}
