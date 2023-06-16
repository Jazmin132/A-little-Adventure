using UnityEngine;

public class JumpPower : PowerUp
{
    [SerializeField] float time; 

    public override void Activate(PlayerM Player)
    {
        Player.DoubleJump(time);
        Debug.Log("Activate double Jump");
        Destroy(this.gameObject);
    }


}
