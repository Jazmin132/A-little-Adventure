using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    void Start()
    {//Va a cargar la escena siguiente, una vez carga, se cambia a esta autom�ticamente
        SceneManager.LoadSceneAsync(ManagerLoading.instance.NextLevel);
    }
}
