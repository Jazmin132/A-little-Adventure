using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    public GameObject Object;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerM P))
            Activate(P);
    }
    public virtual void Activate(PlayerM Player) { }
}
