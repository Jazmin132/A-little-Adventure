using UnityEngine;

public class BlockHard : Ingredient, IDamageableBomb
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerM P) && P.OnPowerAttack) CanBeHit = true;
        else CanBeHit = false;
    }
    public override void Activate()
    {
        DestroyBlock();
    }
    public void RecieveBombDamage(int BombD)
    {
        DestroyBlock();
    }
    public void DestroyBlock()
    {
        Destroy(this.gameObject);
    }
}
