using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    [SerializeField] string _NivelActual;
    [SerializeField] PlayerM _player;

    public void ResetScene()
    {
        SceneManager.LoadScene(_NivelActual);
        GameManager.instance.Lose();
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void GotoMenu()
    {
        SceneManager.LoadScene("TitleScreen");
    }
    public void GotoCheckPoint()
    {
        _player.ActivateCheckPoint();
        Cursor.lockState = CursorLockMode.Locked;
    }
}
