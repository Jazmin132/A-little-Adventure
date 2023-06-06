using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tuerca : MonoBehaviour
{
    [SerializeField] int value;
    [SerializeField] Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        GameManager.instance.onPlay += PlayAnim;
        GameManager.instance.onPause += StopAnim;
    }
    public void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerM>();
        if (player != null)
        {
            GameManager.instance.onPlay += PlayAnim;
            GameManager.instance.onPause -= StopAnim;
            CollectablesManager.instance.AddTuerca(value);
            Destroy(this.gameObject);
        }
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
