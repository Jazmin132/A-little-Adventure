using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCheckP : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerM P))
        {
            P.NewCheckPoint();
            FeedBack();
        }
    }
    public void FeedBack()
    {

    }
}
