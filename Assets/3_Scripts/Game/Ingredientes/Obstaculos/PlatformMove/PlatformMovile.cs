using UnityEngine;

public class PlatformMovile : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<PlayerM>())
        {
            collision.transform.SetParent(transform);
        }//El player no se puede mover como hijo del padre 
    }

    private void OnCollisionExit(Collision collision)
    {//Cuando el personaje sale de la plataforma su transform deja de ser hijo de ella
        if (collision.collider.GetComponent<PlayerM>())
        {
            collision.transform.SetParent(null);
        }
    }
}
