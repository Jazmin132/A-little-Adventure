using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    public int value;
    public Animator anim;

    public virtual void Start()
    {
        anim = GetComponent<Animator>();
        GameManager.instance.onPlay += PlayAnim;
        GameManager.instance.onPause += StopAnim;
    }
    public virtual void DestroyCollect()
    {
        GameManager.instance.onPlay -= PlayAnim;
        GameManager.instance.onPause -= StopAnim;
    }
    void PlayAnim()
    {
        anim.enabled = true;
    }
    void StopAnim()
    {
        anim.enabled = false;
    }
}
