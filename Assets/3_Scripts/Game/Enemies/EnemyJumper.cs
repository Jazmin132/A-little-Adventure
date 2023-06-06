using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyJumper : Enemies , IDamage
{
    
    [SerializeField] float _SpeedRot;

    public EnemyJJump jump;

    public EnemyJView view;

    Rigidbody _Rig;
    Vector3 _Dir;
    Vector3 _LerpDir;

    public override void Start()
    {
        base.Start();
        _Rig = GetComponent<Rigidbody>();
        GameManager.instance.SubscribeBehaviours(this);
    }
    public void FixedUpdate()
    {
        if (EnemiesManager.instance.InFieldOfView(this, view.ViewRadius, view.Angle))
        {
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

        _Rig.position += transform.forward * _Speed * Time.fixedDeltaTime;

        _FlyTo = -_Dir;
    }
    public void Jump()
    {
        _Rig.AddForce(Vector3.up * jump.JumpForce, ForceMode.VelocityChange);
    }
    
    public void RecieveDamage(int damage)
    {
        _CurrentLife -= damage;
        view.DamageParticle.Play();

        FlyTo(_FlyTo);
        _IsGoing = false;

        if (_CurrentLife <= 0)
            Destroy();
    }
    public void FlyTo(Vector3 Dir)
    {
        _IsGoing = true;
        _Rig.AddForce(Dir.normalized * view.DamageFly, ForceMode.VelocityChange);
    }
    
    public void OnCollisionEnter(Collision collision)
    {
        targetCollision = collision.transform.GetComponent<PlayerM>();
        if (targetCollision != null)
            targetCollision.life.RecieveHit(_Attack);
    }

    public void GravityModifier()
    {
        if (_Rig.velocity.y < jump.VelocityFalloff)
            _Rig.velocity += Vector3.up * Physics.gravity.y * (jump.FallMultiplier - 1) * Time.fixedDeltaTime;
    }

    public bool IsGrounded()
    {
        return Physics.Raycast(_Rig.transform.position, Vector3.down, jump.RayJumpDist);
    }

     public override void Destroy()
     {
        GameManager.instance.UnSubscribeBehaviours(this);
        base.Destroy();
        if (_IsGoing == true) StartCoroutine(Wait());
        else
        {
            GameObject effect = Instantiate(view.Explosion, transform.position, Quaternion.identity);
            Destroy(effect, 1f);
            Destroy(this.gameObject);
        }
     }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.3f);
        GameObject effect = Instantiate(view.Explosion, transform.position, Quaternion.identity);
        Destroy(effect, 1f);
        Destroy(this.gameObject);
    }

    Vector3 GetDirFromAngle(float Angle)
    {   return new Vector3(Mathf.Sin(Angle * Mathf.Deg2Rad), 0, Mathf.Cos(Angle * Mathf.Deg2Rad)); }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Vector3 X = new Vector3(0f, -jump.RayJumpDist, 0f);
        Gizmos.DrawLine(transform.position, transform.position + X);
        
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, view.ViewRadius);

        Vector3 LineaA = GetDirFromAngle(-view.Angle / 2 + transform.eulerAngles.y);
        Vector3 LineaB = GetDirFromAngle(view.Angle / 2 + transform.eulerAngles.y);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + LineaA * view.ViewRadius);
        Gizmos.DrawLine(transform.position, transform.position + LineaB * view.ViewRadius);
    }
}
[System.Serializable]
public class EnemyJJump
{
    public float JumpForce;//5
    public float FallMultiplier;//2
    public float VelocityFalloff;//4
    public float RayJumpDist;//1.06

}
[System.Serializable]
public class EnemyJView
{
    public float ViewRadius;//
    public float Angle;//150
    public float DamageFly;//10
    public GameObject Explosion;//ExplosionContainer
    public ParticleSystem DamageParticle;
}
