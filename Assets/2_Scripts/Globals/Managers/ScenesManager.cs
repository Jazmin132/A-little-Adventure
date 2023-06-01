using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public event System.Action onCheckPoint;
    public CanvasWinLoseManager canvasManager;

    public static ScenesManager instance;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void Play(string LevelName)
    {
        SceneManager.LoadScene(LevelName);
        GameManager.instance.Play();
    }
    public void ResetScene(string ThisLevelName)
    {
        SceneManager.LoadScene(ThisLevelName);
    }
    public void GotoMenu()
    {
        SceneManager.LoadScene("TitleScreen");
    }
    public void GotoCheckPoint()
    {
        onCheckPoint?.Invoke();
        //Hacer que en en OncheckPoint también se le sume la vida al player
        // podría usar event Manager
        canvasManager.CloseSubMenu();
    }
    public void NextLevel()
    {

    }
}
