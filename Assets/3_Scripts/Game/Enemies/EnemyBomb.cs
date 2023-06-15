using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBomb : Enemies, IDamage, IDamageableBomb
{
    [Header("Bomb Variables")]
    [SerializeField] SphereCollider _BombCollider;
    [SerializeField] GameObject _VisibleRadious;
    [SerializeField] int TimeBeforeExplosion;
    [SerializeField] int ExplosionDamage;
    [SerializeField] float RadiusExplosion;

    [Header("Move Variables")]
    [SerializeField] float _ViewRadius;
    [SerializeField] float _Angle;
    [SerializeField] float _SpeedRot;

    Rigidbody _Rig;
    Vector3 _Dir;
    Vector3 _LerpDir;
    bool _IsActive;

    public override void Start()
    {
        base.Start();
        GameManager.instance.SubscribeBehaviours(this);
        _Rig = GetComponent<Rigidbody>();
        _BombCollider = _VisibleRadious.GetComponent<SphereCollider>();

        _BombCollider.enabled = false;
        _VisibleRadious.SetActive(false);
    }

    void FixedUpdate()
    {
        if (EnemiesManager.instance.InFieldOfView(this, _ViewRadius, _Angle))
        {
            _BombCollider.enabled = true;
            _VisibleRadious.SetActive(true);
            Movement();
        }
    }
    public override void Movement()
    {
        _Dir = (_Maintarget.transform.position - transform.position).normalized;
        _LerpDir = Vector3.Lerp(transform.forward, _Dir, _SpeedRot * Time.fixedDeltaTime);

        transform.forward = _LerpDir.normalized;
        transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);

        _Rig.position += transform.forward * _Speed * Time.fixedDeltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerM P) && _IsActive == false)
        {
            _IsActive = true;//Sin esto se generan mil corrutinas al mismo tiempo
            StartCoroutine(TimeToExplode(TimeBeforeExplosion)); 
        }
    }
    IEnumerator TimeToExplode(int time)
    {
        yield return new WaitForSeconds(time);
        Debug.Log("CountDown: "+ time);
        Explode();
    }
    void Explode()
    {
        Collider [] colliders = Physics.OverlapSphere(transform.position, RadiusExplosion);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject == gameObject)//Pregunto si soy yo
                continue;//me Evito, paso al siguiente
            if (colliders[i].TryGetComponent(out IDamageableBomb B))
                B.RecieveBombDamage(ExplosionDamage);
        }
        Destroy();
    }
    public void RecieveDamage(int damage)
    {
        _CurrentLife -= damage;
        if (_CurrentLife <= 0) Explode();
    }
    public void RecieveBombDamage(int BombD)
    {
        RecieveDamage(BombD);
    }
    public override void Destroy()
    {
        base.Destroy();
        GameManager.instance.UnSubscribeBehaviours(this);
        Destroy(this.gameObject);
    }
    Vector3 GetDirFromAngle(float Angle)
    { return new Vector3(Mathf.Sin(Angle * Mathf.Deg2Rad), 0, Mathf.Cos(Angle * Mathf.Deg2Rad)); }
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _ViewRadius);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, RadiusExplosion);

        Vector3 LineaA = GetDirFromAngle(_Angle / 2 + transform.eulerAngles.y);
        Vector3 LineaB = GetDirFromAngle(-_Angle / 2 + transform.eulerAngles.y);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + LineaA * _ViewRadius);
        Gizmos.DrawLine(transform.position, transform.position + LineaB * _ViewRadius);
    }
}
