using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ocean : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerM P))
        {
            P.RecieveHit(1);
            P.ActivateCheckPoint();
        }
    }
}
