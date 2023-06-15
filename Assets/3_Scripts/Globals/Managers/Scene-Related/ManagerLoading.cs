using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerLoading : MonoBehaviour
{
    public static ManagerLoading instance;
    public string NextLevel;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }//Va a estar presente en todas las escenas
        else 
            Destroy(gameObject);
    }
}
