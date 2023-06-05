using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoinAtajo : MonoBehaviour
{//Agregar CheckPoints manualmente
    Transform[] checkPoints;
    PlayerM player;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            int cont = 0;
            player.transform.position = checkPoints[cont].position;
            cont++;
            if (cont > checkPoints.Length) cont = 0;
        }
    }
}
