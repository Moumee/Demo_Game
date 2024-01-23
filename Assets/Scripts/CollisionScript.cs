using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionScript : MonoBehaviour
{
    public CinemachineFreeLook freelookCam;
    private Animator animator;
    private Collider mainCollider;
    private Rigidbody mainRigidbody;
    private Rigidbody[] ragdollRigidbodies;
    private Collider[] ragdollColliders;
    private Transform hipsBonePos;
    Rigidbody rb;
    public float collisionForce = 100f;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        mainCollider = GetComponent<Collider>();
        mainRigidbody = GetComponent<Rigidbody>();
        hipsBonePos = animator.GetBoneTransform(HumanBodyBones.Hips);
        ragdollRigidbodies = GetComponentsInChildren<Rigidbody>();
        ragdollColliders = GetComponentsInChildren<Collider>();
        rb = GetComponent<Rigidbody>();
        DisableRagdoll();
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            EnableRagdoll();
            freelookCam.m_LookAt = hipsBonePos;
            freelookCam.m_Follow = hipsBonePos;
            foreach (var ragdollRb in ragdollRigidbodies)
            {
                ragdollRb.AddForce(new Vector3(0, 1, -2).normalized * collisionForce, ForceMode.Impulse);
                Invoke("DisableRagdoll", 3f);
            }
            
        }
        
    }

    void EnableRagdoll()
    {
        foreach (var ragdollRb in ragdollRigidbodies)
        {
            ragdollRb.isKinematic = false;
        }
        foreach (var col in ragdollColliders)
        {
            col.enabled = true;
        }
        animator.enabled = false;
        mainCollider.enabled = false;
        mainRigidbody.isKinematic = true;
    }

    void DisableRagdoll()
    {
        if (freelookCam.m_LookAt != gameObject.transform && freelookCam.m_Follow != gameObject.transform)
        {
            freelookCam.m_LookAt = gameObject.transform;
            freelookCam.m_Follow = gameObject.transform;
        }
        foreach (var rb in ragdollRigidbodies)
        {
            rb.isKinematic = true;
        }
        foreach (var col in ragdollColliders)
        {
            col.enabled = false;
        }
        animator.enabled = true;
        mainCollider.enabled = true;
        mainRigidbody.isKinematic = false;
        AlignPosToHips();
    }

    private void AlignPosToHips()
    {
        Vector3 originalHipsPos = hipsBonePos.position;
        transform.position = hipsBonePos.position;

        if(Physics.Raycast(transform.position, Vector3.down, out RaycastHit hitInfo))
        {
            transform.position = new Vector3(transform.position.x, hitInfo.point.y, transform.position.z);
        }
        hipsBonePos.position = originalHipsPos;
    }
}
