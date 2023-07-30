using UnityEngine;

public class HealthRestoreP : PowerUp
{
    [SerializeField] int HealthRestore;


    public override void Activate(PlayerM Player)
    {
        Player.life.AddLife(HealthRestore);
        Debug.Log("Destroy");
        particle.Play();
        _Box.enabled = false;
        Object.SetActive(false);
        //Destroy(this.gameObject);
    }
}
