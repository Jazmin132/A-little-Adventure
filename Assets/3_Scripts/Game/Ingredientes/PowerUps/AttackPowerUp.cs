using UnityEngine;

public class AttackPowerUp : PowerUp
{
    [SerializeField] int _NewAttack;
    [SerializeField] float _time;

    public override void Activate(PlayerM Player)
    {
        Player.SuperAttack(_time, _NewAttack);
        Destroy(this.gameObject);
    }
}
