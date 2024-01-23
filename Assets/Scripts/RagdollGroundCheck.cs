using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollGroundCheck : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            StartCoroutine(RagdollDisableWait());
        }
    }

    IEnumerator RagdollDisableWait()
    {
        yield return new WaitForSeconds(3f);
        FindFirstObjectByType<CollisionScript>().DisableRagdoll();
    }
}
