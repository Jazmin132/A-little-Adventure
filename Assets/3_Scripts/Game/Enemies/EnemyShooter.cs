using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : Enemies, IDamage
{
    [Header("View Variables")]
    [SerializeField] float _Angle;
    [SerializeField] float _ViewRadius;
    [SerializeField] float _SpeedRot;
    [SerializeField] float _ReloadTime;
    [SerializeField] ParticleSystem _DamageParticle;
    [Header("Shoot Variables")]
    [SerializeField] BulletEnemy _bulletPrefab;
    [SerializeField] Transform _firePoint;
    bool _IsReloading = true;
    Vector3 _Dir;
    Vector3 _LerpDir;

    public override void Start()
    {
        base.Start();
        GameManager.instance.SubscribeBehaviours(this);
    }
    public void FixedUpdate()
    {
        if (EnemiesManager.instance.InFieldOfView(this, _ViewRadius, _Angle))
        {
            LookTowards();
            Debug.Log("_IsReloading " + _IsReloading);
            if (_IsReloading) Shoot();
        }
    }
    void LookTowards()
    {
        _Dir = (_Maintarget.transform.position - transform.position);
        _LerpDir = Vector3.Lerp(transform.forward, _Dir, _SpeedRot * Time.fixedDeltaTime);
        _LerpDir.y = 0;
        transform.forward = _LerpDir.normalized;
    }
    void Shoot()
    {
        Debug.Log("SHOOT");
        //var _bulletObject = EnemyFactoryB._instance.pool.GetObject();
        //_bulletObject.transform.position = _firePoint.position;
        //_bulletObject.transform.forward = transform.forward;
        var bullet = Instantiate(_bulletPrefab);
        bullet.transform.forward = transform.forward;
        bullet.transform.position = _firePoint.position;
        StartCoroutine(Reload(_ReloadTime));
    }
    IEnumerator Reload(float time)
    {
        _IsReloading = false;
        yield return new WaitForSeconds(time);
        _IsReloading = true;
    }
    public void RecieveDamage(int damage)
    {
        _CurrentLife -= damage;
       _DamageParticle.Play();

        if (_CurrentLife <= 0)
            Destroy();
    }
    public override void Destroy()
    {
        GameManager.instance.UnSubscribeBehaviours(this);
        Destroy(this.gameObject);
    }

    Vector3 GetDirFromAngle(float Angle)
    { return new Vector3(Mathf.Sin(Angle * Mathf.Deg2Rad), 0, Mathf.Cos(Angle * Mathf.Deg2Rad)); }
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _ViewRadius);

        Vector3 LineaA = GetDirFromAngle(-_Angle / 2 + transform.eulerAngles.y);
        Vector3 LineaB = GetDirFromAngle(_Angle / 2 + transform.eulerAngles.y);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + LineaA * _ViewRadius);
        Gizmos.DrawLine(transform.position, transform.position + LineaB * _ViewRadius);
    }
}
