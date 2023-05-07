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
    }//A veces ocurren super saltos, no entiendo bien porqué
    public bool IsGrounded()
    {//No siempre detacta el piso, por alguna razón
        return Physics.Raycast(_Rig.transform.position, Vector3.down, _RayJumpDist);
    }
}
