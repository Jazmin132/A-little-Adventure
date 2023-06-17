using UnityEngine;

public class MushoomHandler : MonoBehaviour
{
    [SerializeField] Mushroom[] _Mushrooms;//Llenar manualmente
    int count = 0;

    private void Start()
    {
        for (int i = 0; i < _Mushrooms.Length; i++)
        {
            if (i != 0)
                _Mushrooms[i].gameObject.SetActive(false);
        }
    }
    public void ActivateNextMushroom()
    {
        if (_Mushrooms[count].PlayerDetected)
        {
            _Mushrooms[count].gameObject.SetActive(false);
            _Mushrooms[count].PlayerDetected = false;

            count++;
            if (count >= _Mushrooms.Length)
            {
                count = 0;
                _Mushrooms[count].gameObject.SetActive(true);
            }
            else
                _Mushrooms[count].gameObject.SetActive(true);
        }
    }
}
