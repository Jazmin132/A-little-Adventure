using UnityEngine;
using System.Collections;

public class JumpPower : PowerUp
{
    [SerializeField] float time; 
    [SerializeField] float JumpForceAdded;
    public BoxCollider Box;

    public void Awake()
    {
        Box = GetComponent<BoxCollider>();
    }
    public override void Activate(PlayerM Player)
    {
        Player.DoubleJump(time, JumpForceAdded);
        Debug.Log("Activate double Jump");
        StartCoroutine(ForHowLongGone(5));
    }
    IEnumerator ForHowLongGone(float time)
    {
        Box.enabled = false;
        gameObject.SetActive(false);
        yield return new WaitForSeconds(time);
        gameObject.SetActive(true);
        Box.enabled = true;
    }
}
