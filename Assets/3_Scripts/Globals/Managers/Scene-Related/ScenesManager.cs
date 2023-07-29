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

    public void GotoLevel(string LevelName)
    {//Le pasamos el nombre del nivel siguiente a ManagerLoading
        ManagerLoading.instance.NextLevel = LevelName;
        SceneManager.LoadScene("LoadingScene");
        //GameManager.instance.Play();
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
    public void EXITGame()
    {
        Application.Quit();
    }
    public void GotoCheckPoint()
    {
        onCheckPoint?.Invoke();
        ActivateCheckPoint?.Invoke(3);
        GameManager.instance.Play();
    }
}
