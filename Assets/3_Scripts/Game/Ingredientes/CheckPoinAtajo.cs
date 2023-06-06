using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoinAtajo : MonoBehaviour
{//Agregar CheckPoints manualmente
    public Transform[] checkPoints;
    public PlayerM player;
    int cont = 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            player.transform.position = checkPoints[cont].position;
            cont++;
            Debug.Log(cont + " Cantidad");
            if (cont > checkPoints.Length) cont = 0;
        }
    }
}
