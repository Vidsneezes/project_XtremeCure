using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crawler : MonoBehaviour
{
    public Animator animator;
    public Rigidbody rbody;
    public Transform root;

    bool alive;
    bool attacking;

    Vector3 velocity;

    private void Awake()
    {
        attacking = false;
        alive = true;
    }

    public void OnTakeDamage()
    {
        if (alive)
        {
            alive = false;
            animator.SetBool("alive", false);
            velocity = Vector3.zero;
            attacking = false;
            rbody.velocity = Vector3.zero;
        }
    }

    public void DetectPlayerFront()
    {
        if (!attacking && alive)
        {
            rbody.AddForce(Vector3.up * 5 + root.transform.forward * 5, ForceMode.Impulse);
            velocity = root.transform.forward * 2;
            attacking = true;
            animator.SetBool("attacking", true);
        }
    }

    private void FixedUpdate()
    {
        if(attacking)
        {
            //rbody.MovePosition(rbody.position + velocity * Time.deltaTime);
        }
    }
}
