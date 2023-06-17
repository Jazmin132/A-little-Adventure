using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    PlatformLumineseHandler _Handler;

    void Start()
    {
        _Handler = GetComponentInParent<PlatformLumineseHandler>();
    }

    private void OnTriggerEnter(Collider other)
    {//Ponerselo a un bot�n
        if (other.GetComponent<PlayerM>() != null && _Handler.CanReset) _Handler.ResetPlatforms();
    }
}
