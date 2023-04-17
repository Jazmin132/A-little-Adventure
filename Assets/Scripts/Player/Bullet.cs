using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float _Speed;
    [SerializeField] float _Duration;
    [SerializeField] int _Damage;
    public Vector3 target { get; set; }
    public bool hit { get; set; }
    Rigidbody _Rig;

    void Start()
    {
        _Rig = GetComponent<Rigidbody>();
        //_Rig.velocity = transform.forward * _Speed;
        StartCoroutine(Destroy());
    }
    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, _Speed * Time.fixedDeltaTime);
        if (!hit && Vector3.Distance(transform.position,target) < 0.1f)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        var D = other.GetComponent<IDamage>();
        if (D != null) D.RecieveDamage(_Damage);
        Destroy(this.gameObject);
    }

    public IEnumerator Destroy()
    {
        yield return new WaitForSeconds(_Duration);
        Destroy(this.gameObject);
    }
}
