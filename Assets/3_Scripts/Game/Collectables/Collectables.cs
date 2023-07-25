using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Collectables : MonoBehaviour
{
    public int value;
    public Animator anim;

    public virtual void Start()
    {
        anim = GetComponent<Animator>();
        GameManager.instance.onPlay += PlayAnim;
        GameManager.instance.onPause += StopAnim;
    }
    public virtual void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerM>();
        var playerBullet = other.GetComponent<BulletPlayer>();

        if (player != null || playerBullet != null)
        {
            GameManager.instance.onPlay -= PlayAnim;
            GameManager.instance.onPause -= StopAnim;
            WhatToAdd();
            Destroy(gameObject);
        }
    }
    public virtual void WhatToAdd()
    { }
    void PlayAnim()
    {
        anim.enabled = true;
    }
    void StopAnim()
    {
        anim.enabled = false;
    }
}
