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
    [SerializeField] float _DamageFly;
    [SerializeField] GameObject _Explosion;
    Rigidbody _Rig;
    Vector3 _Dir;
    Vector3 _LerpDir;
    Behaviour _script;
    bool _IsAlive;

    public override void Start()
    {
        base.Start();
        _Rig = GetComponent<Rigidbody>();
        _script = GetComponent<Behaviour>();
    }
    public void FixedUpdate()
    {
        if (EnemiesManager.instance.InFieldOfView(this, _ViewRadius, _Angle))
        {//No Se activa al entrar al InFieldOfView, solo cuando etá muy cerca
          if (IsGrounded()) Jump();

          GravityModifier();
          Movement();
          _IsGoing = true;
        }
        else _IsGoing = false;
    }

    public override void Movement()
    {
        _Dir = (_Maintarget.transform.position - transform.position);
        _LerpDir = Vector3.Lerp(transform.forward, _Dir, _SpeedRot * Time.fixedDeltaTime);
        transform.forward = _LerpDir.normalized;
        transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);

        _Rig.position += transform.forward * _ActualSpeed * Time.fixedDeltaTime;

        _FlyTo = -_Dir;
    }
    public void Jump()
    {
        _Rig.AddForce(Vector3.up * _JumpForce, ForceMode.VelocityChange);
    }
    
    public void RecieveDamage(int damage)
    {
        _CurrentLife -= damage;
        if (_CurrentLife <= 0) Destroy();
        if (_IsGoing == true) FlyTo(_FlyTo);
    }
    public void FlyTo(Vector3 Dir)
    {
        _Rig.AddForce(Dir * _DamageFly, ForceMode.VelocityChange);
    }
    
    public void OnCollisionEnter(Collision collision)
    {
        targetCollision = collision.transform.GetComponent<PlayerM>();
        if (targetCollision != null)
            targetCollision.life.RecieveHit(_Attack);
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
        if (_IsGoing == true)
        {
            StartCoroutine(Wait());
        }
        else
        {
            GameObject effect = Instantiate(_Explosion, transform.position, Quaternion.identity);
            Destroy(effect, 1f);
            Destroy(this.gameObject);
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.3f);
        GameObject effect = Instantiate(_Explosion, transform.position, Quaternion.identity);
        Destroy(effect, 1f);
        Destroy(this.gameObject);
    }

    Vector3 GetDirFromAngle(float Angle)
    {   return new Vector3(Mathf.Sin(Angle * Mathf.Deg2Rad), 0, Mathf.Cos(Angle * Mathf.Deg2Rad)); }

    public override void OnPause()
    {
        _script.enabled = false;
      //Si el enemigo está muerto, el GameManager aún quiere acceder al evento
    }
    public override void OnPlay()
    {
        _script.enabled = true;
    }
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
