using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VitalPiece : Collectables
{
    public override void WhatToAdd()
    {
        CollectablesManager.instance.AddVital(1);
    }
}
