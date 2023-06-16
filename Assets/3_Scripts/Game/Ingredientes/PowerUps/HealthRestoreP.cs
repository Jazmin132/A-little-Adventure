using UnityEngine;

public class HealthRestoreP : PowerUp
{
    [SerializeField] int HealthRestore;

    public override void Activate(PlayerM Player)
    {
        Player.life.AddLife(HealthRestore);
        Destroy(this.gameObject);
    }
}
