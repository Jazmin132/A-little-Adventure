using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public event System.Action onPause;
    public event System.Action onPlay;

    public List<MonoBehaviour>_listBehaviour;

    public CanvasWinLoseManager canvasManager;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void SubscribeBehaviours(MonoBehaviour behaviour)
    {
        if (!_listBehaviour.Contains(behaviour))
            _listBehaviour.Add(behaviour);
    }
    public void UnSubscribeBehaviours(MonoBehaviour behaviour)
    {
        if (_listBehaviour.Contains(behaviour))
            _listBehaviour.Remove(behaviour);
    }
    public void Pause()
    {
        foreach (var behaviour in _listBehaviour)
        {
            behaviour.enabled = false;
        }
        onPause?.Invoke();
        UnlockCursor();
    }

    public void Play()
    {
        foreach (var behaviour in _listBehaviour)
        {
            behaviour.enabled = true;
        }
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
