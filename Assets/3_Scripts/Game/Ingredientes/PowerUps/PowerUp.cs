using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerM P))
        {
            Activate(P);
            Debug.Log("PlayerDetected");
        }
    }

    public virtual void Activate(PlayerM Player) { }
}
