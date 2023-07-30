using UnityEngine;

public class AttackPowerUp : PowerUp
{
    [SerializeField] int _NewAttack;
    [SerializeField] float _time;

    public override void Activate(PlayerM Player)
    {
        Player.SuperAttack(_time, _NewAttack);
        particle.Play();
        _Box.enabled = false;
        Object.SetActive(false);
    }
}
