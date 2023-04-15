using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOX : Damage
{
    public override void Destroy()
    {
            Destroy(this.gameObject);
    }
}
