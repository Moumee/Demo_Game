using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public AudioSource jumpSound;
    private Animator animator;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("YVelocity", rb.velocity.y);
        animator.SetBool("IsJumping", !FindFirstObjectByType<PlayerMovement>().IsGrounded());
        animator.SetFloat("Speed", rb.velocity.magnitude);
    }

    void PlayJumpSound()
    {
        jumpSound.Play();
    }
}
