using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{//Puedo hacer un manager de portales que los intancie y los ubique en su respectivo portal?
    [SerializeField] int TotalCollectabes;
    [SerializeField] int TotalCrucialCollectabes;
    [SerializeField] string Level;

    public void GoToLevel(int curretCollectables, int currentCrucialCollectables)
    {//Que reciba por parámetro a que nivel tendría que ir

        if (curretCollectables <= TotalCollectabes && currentCrucialCollectables <= TotalCrucialCollectabes)
        {
            ScenesManager.instance.Play(Level);
            //Llamar a scenesManager para ir a otro nivel
        }
    }
    void DisplayText(int curretCollectables, int currentCrucialCollectables)
    {
        //Mostrar la cantidad total de los coleccionables y cuantos tiene el player
    }
}
