using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBounce
{
    Rigidbody _RigP;
    float _SpringDist;
    float _SpringStrength;
    float _SpringDamper;

    float _RayDirvel;
    float _OtherDirvel;
    float _FinalDirVelocity;
    float _SpringForce;

    public PlayerBounce SetRigidbody(Rigidbody Rig)
    {
        _RigP = Rig;
        return this;
    }
    public PlayerBounce SetSpring(float SpringDist, float SpringStrength, float SpringDamper)
    {
        _SpringDist = SpringDist;
        _SpringStrength = SpringStrength;
        _SpringDamper = SpringDamper;
        return this;
    }
    public void BounceFloat()
    {
        RaycastHit HitPoint;

        var RayDidHit = Physics.Raycast(_RigP.transform.position, Vector3.down, out HitPoint, _SpringDist);
        if (RayDidHit)
        {
            Vector3 PlayerVel = _RigP.velocity;
            Vector3 RayDir = Vector3.down;

            Vector3 othervel = Vector3.zero;
            Rigidbody hitBody = HitPoint.rigidbody;

            if (hitBody != null) othervel = hitBody.velocity;

            _RayDirvel = Vector3.Dot(RayDir, PlayerVel);
            _OtherDirvel = Vector3.Dot(RayDir, othervel);

            _FinalDirVelocity = _RayDirvel - _OtherDirvel;

            float X = HitPoint.distance - _SpringDist;

            _SpringForce = (X * _SpringStrength) - (_FinalDirVelocity * _SpringDamper);
            _RigP.AddForce(RayDir * _SpringForce);

            if (hitBody != null)
            {
                hitBody.AddForceAtPosition(RayDir * -_SpringForce, HitPoint.point);
            }
        }
    }
    public void UpdateForce()
    {

    }
}
