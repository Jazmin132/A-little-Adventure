using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HearthDisplay : MonoBehaviour
{
    public Image[] hearthIcons;
    
    private void Awake()
    {
        hearthIcons = GetComponentsInChildren<Image>();
    }

    public void UpdateHealth(int currentHealth)
    {
        for (int i = 0; i < hearthIcons.Length; i++)
        {
            hearthIcons[i].enabled = (currentHealth > i);
        }
    }
}
