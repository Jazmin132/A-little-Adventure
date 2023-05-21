using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float _Speed;
    [SerializeField] int _Damage;
    [SerializeField] float _maxDistance;
    float _currentDistance;

    Rigidbody _Rig;
    public Vector3 target { get; set; }
    public bool hit { get; set; }

    void Start()
    {
        _Rig = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        _Rig.transform.position = Vector3.MoveTowards(transform.position, target, _Speed * Time.fixedDeltaTime);
       
        _currentDistance += _Speed * Time.deltaTime;

        if (!hit && Vector3.Distance(transform.position, target) < 0.1f)
            BulletFactory._instance.ReturnBullet(this);
        else if (_currentDistance > _maxDistance) 
            BulletFactory._instance.ReturnBullet(this);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamage D)) D.RecieveDamage(_Damage);
        else if (other.TryGetComponent(out Ingredient I) && I.CanBeShoot) I.Activate();

        BulletFactory._instance.ReturnBullet(this);
    }

    private void Reset()
    {
        _currentDistance = 0;
    }
    public static void TurnOn(Bullet b)
    {
        b.Reset();
        b.gameObject.SetActive(true);
    }

    public static void TurnOff(Bullet b)
    {
        b.gameObject.SetActive(false);
    }
}
