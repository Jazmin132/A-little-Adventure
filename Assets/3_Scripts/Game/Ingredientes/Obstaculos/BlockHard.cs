using UnityEngine;

public class BlockHard : Ingredient, IDamage
{
    public int life;

    public void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerM P) && P.OnPowerAttack) CanBeHit = true;
        else CanBeHit = false;
    }
    public override void Activate()
    {
        Destroy();
    }
    public void Destroy()
    {
        Destroy(this.gameObject);
    }
    public void RecieveDamage(int damage) 
    {
        life -= damage;
        if (life <= 0)
        {
            Destroy();
        }
    }
}
