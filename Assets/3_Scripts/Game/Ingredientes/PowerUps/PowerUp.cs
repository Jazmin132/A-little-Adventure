using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    public ParticleSystem particle;
    public GameObject Object;
    protected BoxCollider _Box;

    public void Awake()
    {
        _Box = GetComponent<BoxCollider>();
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerM P))
            Activate(P);
    }
    public virtual void Activate(PlayerM Player) { }
}
