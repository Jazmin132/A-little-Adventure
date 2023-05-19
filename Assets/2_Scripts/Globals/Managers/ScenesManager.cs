using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public void ResetScene()
    {
        SceneManager.LoadScene("FirstLevel");
    }
    public void GotoMenu()
    {
        SceneManager.LoadScene("TitleScreen");
    }
}
