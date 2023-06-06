using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public event System.Action onCheckPoint;
    public event System.Action<int> ActivateCheckPoint;
   
    public static ScenesManager instance;

    void Awake()
    {
        instance = this;
    }

    public void Play(string LevelName)
    {
        SceneManager.LoadScene(LevelName);
       
        GameManager.instance.Play();
    }

    public void ResetScene(string ThisLevelName)
    {
        SceneManager.LoadScene(ThisLevelName);
        GameManager.instance.Play();
    }
    public void GotoMenu()
    {
        SceneManager.LoadScene("TitleScreen");
    }
    public void GotoCheckPoint()
    {
        onCheckPoint?.Invoke();
        ActivateCheckPoint?.Invoke(3);
        GameManager.instance.Play();
    }
}