using UnityEngine;

public class BlockHard : MonoBehaviour, IDamageableBomb
{
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out PlayerM P) && P.IsPowerAttack)
            Destroy(this.gameObject);
    }
    public void RecieveBombDamage(int BombD)
    {
        Destroy(this.gameObject);
    }
}
