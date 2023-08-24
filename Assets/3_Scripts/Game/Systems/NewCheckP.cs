using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCheckP : MonoBehaviour
{
    [SerializeField] ParticleSystem _SavedCheck;
    [SerializeField] GameObject _Flag;
    [SerializeField] Material[] _Activate;
    Material _FlafMat;
    BoxCollider _Box;

    private void Start()
    {
        _Box = GetComponent<BoxCollider>();
        _FlafMat = _Flag.GetComponent<Material>();
        _FlafMat = _Activate[0];
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerM P))
        {
            P.CheckPoint();
            FeedBack();
            _Box.enabled = false;
        }
    }
    public void FeedBack()
    {
        _SavedCheck.Play();
        _FlafMat = _Activate[1];
    }
}
