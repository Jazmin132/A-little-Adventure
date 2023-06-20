using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadAsyncScenes : MonoBehaviour
{ //Tener las escena asincrónicas en el array  
    public string[] loadLevels;

    void Start()
    {
        StartCoroutine(LoadYourAsyncScene());
        GameManager.instance.Play();
    }

    IEnumerator LoadYourAsyncScene()
    {
        for (int i = 0; i < loadLevels.Length; i++)
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(loadLevels[i], LoadSceneMode.Additive);
            
            while (!asyncLoad.isDone)   
            {
                yield return null;
            }
        }
    }
}
