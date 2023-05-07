using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tuerca : MonoBehaviour
{
    [SerializeField] int value;

    public void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerM>();
        if (player != null)
        {
            CollectablesManager.instance.AddTuerca(value);
            Destroy(this.gameObject);
        }
    }
}
