using UnityEngine;

public class PlatformLumineseHandler : MonoBehaviour
{
    public PlatformLuninese[] Platforms;
    public System.Action Win = delegate { };               
    public bool CanReset;                     

    private void Start()
    {
        Platforms = GetComponentsInChildren<PlatformLuninese>();
    }
    public void Check()
    {
        bool active = true;
        foreach (var platform in Platforms)
        {
            if (platform.count != 1)
            {//Si hay alguno distinto de uno
                active = false;
                if (platform.count >= 2)
                {//Si alguna plataforma se pas� de largo
                    CanReset = true;
                }   
            }
        }
        if (active) 
            PuzzleWin();
    }
    private void PuzzleWin()
    {
        foreach (var platform in Platforms)
        {
            platform.SetPermanentColor(1);
        }
        CanReset = false;
        Win.Invoke();
    }
    public void ResetPlatforms()
    {
        foreach (var platform in Platforms)
        {
            platform.ResetColor();
        }
    }
    //private void OnTriggerEnter(Collider other)
    //{//Ponerselo a un bot�n
    //    if (other.GetComponent<PlayerM>() != null && _CanReset) ResetPlatforms();
    //}
}
