using UnityEngine;
using System.Collections;

public class JumpPower : PowerUp
{
    [SerializeField] float time; 
    [SerializeField] float JumpForceAdded; 


    public override void Activate(PlayerM Player)
    {
        Player.DoubleJump(time, JumpForceAdded);
        Debug.Log("Activate double Jump");
        this.gameObject.SetActive(false);
        StartCoroutine(ForHowLongGone(5));
    }
    IEnumerator ForHowLongGone(float time)
    {
        yield return new WaitForSeconds(time);
        this.gameObject.SetActive(true);
    }
}
