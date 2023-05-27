using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.SceneManagement; Como hago para reiniciar el nivel
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public event System.Action onPause;
    public event System.Action onPlay;

    public CanvasWinLoseManager canvasManager;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
    public void Start()
    {
        Play();
    }
    public void Pause()
    {
        onPause?.Invoke();
        UnlockCursor();
    }

    public void Play()
    {
        onPlay?.Invoke();
        LockCursor();
    }

    public void Lose()
    {
        canvasManager.ShowSubMenu("Lose");
        Pause();
    }

    public void Win()
    {
        canvasManager.ShowSubMenu("Win");
        Pause();
    }

    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
    }
}
