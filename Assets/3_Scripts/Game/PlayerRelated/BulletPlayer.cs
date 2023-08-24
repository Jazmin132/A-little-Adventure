using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPlayer : Bullet
{
    public LayerMask NotHit;

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
        if (other.gameObject.layer == NotHit) return;

        if (other.TryGetComponent(out IDamage D)) D.RecieveDamage(_Damage);
        else if (other.TryGetComponent(out Ingredient I) && I.CanBeShoot) I.Activate();

        BulletFactory._instance.ReturnBullet(this);
    }
}
