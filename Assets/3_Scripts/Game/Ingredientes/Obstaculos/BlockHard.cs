using UnityEngine;

public class BlockHard : Ingredient
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
    public void DestroyBlock()
    {
        Destroy(this.gameObject);
    }
}
