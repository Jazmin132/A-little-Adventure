using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCheckP : MonoBehaviour
{
    [SerializeField] ParticleSystem _SavedCheck;
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerM P))
        {
            P.CheckPoint();
            FeedBack();
        }
    }
    public void FeedBack()
    {
        if (_SavedCheck != null) _SavedCheck.Play();
    }
}
