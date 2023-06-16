using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump
{
    Rigidbody _Rig;
    float _RayJumpDist;
    float _ForceJ;

    public PlayerJump SetJump(float MaxJumpDist, float ForceJ)
    {
        _RayJumpDist = MaxJumpDist;
        _ForceJ = ForceJ;
        return this;
    }
    public PlayerJump SetRigidbody(Rigidbody Rig)
    {
        _Rig = Rig;
        return this;
    }
    public void Jump()
    {
        _Rig.AddForce(Vector3.up * _ForceJ, ForceMode.VelocityChange);
    }
    public void Doublejump(float ForceDoubleJ)
    {
        var vel = _Rig.velocity;
        vel.y = 0;
        _Rig.velocity = vel;
        _Rig.AddForce(Vector3.up * ForceDoubleJ, ForceMode.VelocityChange);
    }
    public bool IsGrounded()
    {
        return Physics.Raycast(_Rig.transform.position, Vector3.down, _RayJumpDist);
    }
}
