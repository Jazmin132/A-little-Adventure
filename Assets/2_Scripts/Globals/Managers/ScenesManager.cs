//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public CanvasWinLoseManager canvasManager;
    public event System.Action onCheckPoint;
    public event System.Action<int> ActivateCheckPoint;
    public string[] loadLevels;
    //Tener las escena asincrónicas en el array  
    public static ScenesManager instance;

    void Awake()
    {
        instance = this;
    }

    public void Play(string LevelName)
    {
        SceneManager.LoadScene(LevelName);
        /*for (int i = 0; i < loadLevels.Length; i++)
        {
            SceneManager.LoadScene(loadLevels[i].LoadScene.Additive);
        }*/
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
        ActivateCheckPoint?.Invoke(3);
        canvasManager.CloseSubMenu();
    }
}
