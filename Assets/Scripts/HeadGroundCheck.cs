using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HeadGroundCheck : MonoBehaviour
{
    Rigidbody body;
    public AudioSource backToStartMusic;
    public AudioSource floorHitSound;
    CollisionScript collisionScript;
    public AudioSource backgroundMusic;
    private void Start()
    {
        body = GetComponent<Rigidbody>();
        collisionScript = FindFirstObjectByType<CollisionScript>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!body.isKinematic && collision.collider.CompareTag("Ground"))
        {
            FindFirstObjectByType<CollisionScript>().AlignPosToHips();
            floorHitSound.Play();
            StartCoroutine(WaitDisableRagdoll());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Start Music") && collisionScript.startMusicEnabled)
        {
            backgroundMusic.Stop();
            backToStartMusic.Play();
            backgroundMusic.PlayDelayed(backToStartMusic.clip.length);
            collisionScript.startMusicEnabled = false;
        }
    }

    
    IEnumerator WaitDisableRagdoll()
    {
        yield return new WaitForSeconds(1f);
        FindFirstObjectByType<CollisionScript>().DisableRagdoll();
    }

}
