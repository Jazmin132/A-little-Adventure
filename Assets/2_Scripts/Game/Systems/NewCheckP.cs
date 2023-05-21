using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCheckP : MonoBehaviour
{
    [SerializeField] ParticleSystem _SavedCheck;
    [SerializeField] GameObject _Flag;
    [SerializeField] Material[] _Activate;
    CapsuleCollider _Capsule;

    private void Start()
    {
        _Capsule = GetComponent<CapsuleCollider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerM P))
        {
            P.CheckPoint();
            FeedBack();
            _Capsule.enabled = false;
        }
    }
    public void FeedBack()
    {
        if (_SavedCheck != null) _SavedCheck.Play();
    }
}
