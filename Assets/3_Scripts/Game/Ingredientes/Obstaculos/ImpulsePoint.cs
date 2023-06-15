using System.Collections;
using UnityEngine;

public class ImpulsePoint : Ingredient
{
    [Header("Impulse")]
    [SerializeField] float _ImpulseForce;
    [SerializeField] float _NonActiveTime;
    [SerializeField] GameObject _SpherePoint;
    SphereCollider _SphereCollider;
    PlayerM _Maintarget;

    public void Start()
    {
        _SphereCollider = GetComponentInChildren<SphereCollider>();
        _Maintarget = EnemiesManager.instance._target;
    }
    public override void Activate()
    {
        _Maintarget.UpImpulse(_ImpulseForce);

        _SpherePoint.SetActive(false);
        _SphereCollider.enabled = false;
        StartCoroutine(NowOn(_NonActiveTime));
    }

    IEnumerator NowOn(float Time)
    {
        yield return new WaitForSeconds(Time);
        _SpherePoint.SetActive(true);
        _SphereCollider.enabled = true;
    }
}
