using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VitalPiece : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerM>() != null)
        {
            GameManager.instance.Win();
        }
    }
}
