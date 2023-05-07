using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPoint : MonoBehaviour ,Iingredient
{
    [SerializeField] GameObject _SpherePoint;
    [SerializeField] GameObject _Platform;
    [SerializeField] float _OnTime;
    private BoxCollider _BoxCollider;
    private SphereCollider _SphereCollider;

    public void Start()
    {
        _BoxCollider = GetComponentInChildren<BoxCollider>();
        _SphereCollider = GetComponent<SphereCollider>();
        _Platform.SetActive(false);
        _BoxCollider.enabled = false;
    }

    public void Activate()
    {
        _SpherePoint.SetActive(false);
        _SphereCollider.enabled = false;

        _Platform.SetActive(true);
        _BoxCollider.enabled = true;

        StartCoroutine(ReApear(_OnTime));
    }

    IEnumerator ReApear(float Time)
    {
        yield return new WaitForSeconds(Time);
        _SpherePoint.SetActive(true);
        _SphereCollider.enabled = true;

        _Platform.SetActive(false);
        _BoxCollider.enabled = false;
    }
}
