using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tuerca : Collectables
{
    public void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerM>();
        var playerBullet = other.GetComponent<BulletPlayer>();

        if (player != null || playerBullet != null)
        {
            DestroyCollect();
            CollectablesManager.instance.AddTuerca(value);
            Destroy(this.gameObject);
        }
    }
}
