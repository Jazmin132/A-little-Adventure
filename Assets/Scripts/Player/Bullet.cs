using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float _Speed;
    [SerializeField] float _Duration;
    [SerializeField] int _Damage;
    Rigidbody _Rig;

    void Start()
    {
        _Rig = GetComponent<Rigidbody>();
        _Rig.velocity = transform.forward * _Speed;
        StartCoroutine(Destroy());
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.GetComponent<PlayerM>() && !other.GetComponent<Bullet>()) Destroy(this.gameObject);
    }

    public IEnumerator Destroy()
    {
        yield return new WaitForSeconds(_Duration);
        Destroy(this.gameObject);
    }
}
