using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : Bullet
{
    private void FixedUpdate()
    {
        _Rig.transform.position += transform.forward * _Speed * Time.fixedDeltaTime;

        _currentDistance += _Speed * Time.deltaTime;

        if (_currentDistance > _maxDistance)
            Destroy(this.gameObject);
        // BulletFactory._instance.ReturnBullet(this);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerM P))
            P.life.RecieveHit(_Damage);
        Destroy(this.gameObject);
        //BulletFactory._instance.ReturnBullet(this);
    }
}
