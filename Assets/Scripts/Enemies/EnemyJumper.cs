using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyJumper : Enemies , IDamage
{
    [SerializeField] float _SpeedRot;
    [Header("Jump")]
    [SerializeField] float _JumpForce;
    [SerializeField] float _FallMultiplier;
    [SerializeField] float _VelocityFalloff;
    [SerializeField] float _RayJumpDist;
    [Header("View")]
    [SerializeField] float _ViewRadius;
    [SerializeField] float _Angle;
    private Rigidbody _Rig;
    private Vector3 _Dir;
    private Vector3 _LerpDir;

    public override void Start()
    {
        base.Start();
        _Rig = GetComponent<Rigidbody>();
    }
    public void FixedUpdate()
    {
        if (EnemiesManager.instance.InFieldOfView(_Maintarget.transform.position, _ViewRadius, _Angle))
          {//No Se activa al entrar al InFieldOfView, solo cuando etá muy cerca
            if (IsGrounded()) Jump();
            GravityModifier();
            Movement();
          }
    }

    public override void Movement()
    {
        _Dir = (_Maintarget.transform.position - transform.position);
        _LerpDir = Vector3.Lerp(transform.forward, _Dir, _SpeedRot * Time.fixedDeltaTime);
        transform.forward = _LerpDir.normalized;
        transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
        _Rig.position += transform.forward * _ActualSpeed * Time.fixedDeltaTime; 
    }
    public void Jump()
    {
        _Rig.AddForce(Vector3.up * _JumpForce, ForceMode.VelocityChange);
    }
    public void OnCollisionEnter(Collision collision)
    {
        targetCollision = collision.transform.GetComponent<PlayerM>();
        if (targetCollision != null)
            targetCollision.RecieveHit(_Attack);
    }
    
    public void RecieveDamage(int damage)
    {
        _CurrentLife -= damage;
        if (_CurrentLife <= 0) Destroy();
    }
    
    public void GravityModifier()
    {
        if (_Rig.velocity.y < _VelocityFalloff)
            _Rig.velocity += Vector3.up * Physics.gravity.y * (_FallMultiplier - 1) * Time.fixedDeltaTime;
    }

    public bool IsGrounded()
    {
        return Physics.Raycast(_Rig.transform.position, Vector3.down, _RayJumpDist);
    }

    public override void Destroy()
    {
        Destroy(this.gameObject);
    }

    Vector3 GetDirFromAngle(float Angle)
    {   return new Vector3(Mathf.Sin(Angle * Mathf.Deg2Rad), 0, Mathf.Cos(Angle * Mathf.Deg2Rad)); }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Vector3 X = new Vector3(0f, -_RayJumpDist, 0f);
        Gizmos.DrawLine(transform.position, transform.position + X);
        
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _ViewRadius);

        Vector3 LineaA = GetDirFromAngle(-_Angle / 2 + transform.eulerAngles.y);
        Vector3 LineaB = GetDirFromAngle(_Angle / 2 + transform.eulerAngles.y);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + LineaA * _ViewRadius);
        Gizmos.DrawLine(transform.position, transform.position + LineaB * _ViewRadius);
    }
}
