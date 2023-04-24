using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpulsePoint : MonoBehaviour, Iingredient
{
    [Header("Impulse")]
    [SerializeField] float _impulse;
    [SerializeField] float _OnTime;
    [SerializeField] GameObject _SpherePoint;
    private SphereCollider _SphereCollider;
    private PlayerM _Maintarget;

    public void Start()
    {
        _SphereCollider = GetComponentInChildren<SphereCollider>();
        _Maintarget = EnemiesManager.instance._target;
    }

    public void Activate()
    {
        _Maintarget.UpImpulse(_impulse);

        _SpherePoint.SetActive(false);
        _SphereCollider.enabled = false;
        StartCoroutine(NowOn(_OnTime));
    }

    IEnumerator NowOn(float Time)
    {
        yield return new WaitForSeconds(Time);
        _SpherePoint.SetActive(true);
        _SphereCollider.enabled = true;
    }
}
