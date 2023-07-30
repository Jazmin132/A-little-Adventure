using UnityEngine;
using System.Collections;

public class JumpPower : PowerUp
{
    [SerializeField] float time; 
    [SerializeField] float JumpForceAdded;

    public override void Activate(PlayerM Player)
    {
        Player.DoubleJump(time, JumpForceAdded);
        particle.Play();
        Debug.Log("Activate double Jump");
        StartCoroutine(ForHowLongGone(3));
    }
    IEnumerator ForHowLongGone(float time)
    {
        _Box.enabled = false;
        Object.SetActive(false);
        yield return new WaitForSeconds(time);
        Object.SetActive(true);
        _Box.enabled = true;
    }
}
