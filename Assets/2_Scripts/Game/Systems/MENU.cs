using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MENU : MonoBehaviour
{
    [SerializeField] string _NivelActual;

    public void Play()
    {
        SceneManager.LoadScene(_NivelActual);
        Cursor.lockState = CursorLockMode.Locked;
    }
}
