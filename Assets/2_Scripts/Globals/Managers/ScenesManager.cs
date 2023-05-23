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
    }
    public void GotoMenu()
    {
        SceneManager.LoadScene("TitleScreen");
    }
    public void GotoCheckPoint()
    {
        _player.ActivateCheckPoint();//ARREGLAR EL CANVAS LOSE
        _player.life.AddLife(3);
    }
}
