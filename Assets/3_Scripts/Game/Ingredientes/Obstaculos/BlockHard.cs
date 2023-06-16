using UnityEngine;

public class BlockHard : Ingredient, IDamageableBomb
{
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out PlayerM P) && P.IsPowerAttack) 
            CanBeHit = true;
        else 
            CanBeHit = false;
    }
    public override void Activate()
    {
        Destroy(this.gameObject);
    }
    public void RecieveBombDamage(int BombD)
    {
        Destroy(this.gameObject);
    }
}
