using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump
{
    Rigidbody _Rig;
    float _RayJumpDist;
    float _ForceJ;
    float _ForceJOriginal;
    LayerMask _NotGround;

    public PlayerJump SetJump(float MaxJumpDist, float ForceJ)
    {
        _RayJumpDist = MaxJumpDist;
        _ForceJ = ForceJ;
        _ForceJOriginal = ForceJ;
        return this;
    }
    public PlayerJump SetRigidbody(Rigidbody Rig)
    {
        _Rig = Rig;
        return this;
    }
    public PlayerJump SetGround(LayerMask Ground)
    {
        _NotGround = Ground;
        return this;
    }
    public void Jump()
    {
        _Rig.AddForce(Vector3.up * _ForceJ, ForceMode.VelocityChange);
    }
    public void Superjump(float ForceSuperJ)
    {
        _ForceJ += ForceSuperJ;
    }
    public void Resetjump()
    {
        _ForceJ = _ForceJOriginal;
    }
    public bool IsGrounded()
    {
        //Debug.Log("ISGround");
        return Physics.Raycast(_Rig.transform.position, Vector3.down, _RayJumpDist);
    }
    public bool IsNotGrounded()
    {
        return Physics.Raycast(_Rig.transform.position, Vector3.down, _RayJumpDist, _NotGround);
    }
}
