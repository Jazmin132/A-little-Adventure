using UnityEngine;

public class PlatformLumineseHandler : MonoBehaviour
{
    public PlatformLuninese[] Platforms;
    public System.Action Win = delegate { };
    int count = 0;                       
    bool _CanReset;                     

    private void Start()
    {
        Platforms = GetComponentsInChildren<PlatformLuninese>();
    }
    public void Check()
    {
        foreach (var platform in Platforms)
        {
            if (platform.count == 1) count++;
        }
        if (count == Platforms.Length) PuzzleWin();
    }
    private void PuzzleWin()
    {
        foreach (var platform in Platforms)
        {
            platform.SetPermanentColor(1);
            _CanReset = false;
        }
        Win.Invoke();
    }
    void ResetPlatforms()
    {
        foreach (var platform in Platforms)
        {
            platform.ResetColor();
        }
    }
    private void OnTriggerEnter(Collider other)
    {//Ponerselo a un botón
        if (other.TryGetComponent(out PlayerM P) && _CanReset) ResetPlatforms();
    }
}
