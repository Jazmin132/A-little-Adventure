using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    public bool CanBeShoot;
    public bool CanBeHit;

    public virtual void Activate() { }
}
