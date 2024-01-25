using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HeadGroundCheck : MonoBehaviour
{
    public AudioSource backToStartMusic;
    CollisionScript collisionScript;
    private void Start()
    {
        collisionScript = FindFirstObjectByType<CollisionScript>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            StartCoroutine(WaitDisableRagdoll());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Start Music") && collisionScript.startMusicEnabled)
        {
            backToStartMusic.Play();
            collisionScript.startMusicEnabled = false;
        }
    }
    IEnumerator WaitDisableRagdoll()
    {
        yield return new WaitForSeconds(2f);
        FindFirstObjectByType<CollisionScript>().DisableRagdoll();
    }

}
