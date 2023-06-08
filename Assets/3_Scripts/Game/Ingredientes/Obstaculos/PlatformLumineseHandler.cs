using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformLumineseHandler : MonoBehaviour
{
    public PlatformLuninese[] Platforms;
    public System.Action Change;
    //Como checheo que todas las plataformas estén en el segundo color
    private void Start()
    {
        Platforms = GetComponentsInChildren<PlatformLuninese>();
    }
    public void Check()
    {
        foreach (var platform in Platforms)
        {
            //COMO checkeo checkeo por si todos tiene count 1
        }
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
}
