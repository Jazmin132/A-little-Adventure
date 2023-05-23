using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.SceneManagement; Como hago para reiniciar el nivel
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] Transform _MainGame;
    public ScreenUI _CanvasLose;
    [SerializeField] ScreenUI _CanvasWin;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
    void Start()
    {
        ScreenManager.instance.Push(new ScreenGo(_MainGame));
    }
    public void Lose()
    {
        ScreenManager.instance.Push(_CanvasLose);
    }
    public void Win()
    {
        ScreenManager.instance.Push(_CanvasWin);
    }
}
