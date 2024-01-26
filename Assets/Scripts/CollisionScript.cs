using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CollisionScript : MonoBehaviour
{
    public bool isGameCleared = false;
    public AudioSource gameClearSound;
    public GameObject gameClearScreen;
    public CinemachineFreeLook freelookCam;
    private Animator animator;
    private Collider mainCollider;
    private Rigidbody mainRigidbody;
    public Rigidbody[] ragdollRigidbodies;
    private Collider[] ragdollColliders;
    private Transform hipsBonePos;
    Rigidbody rb;
    public AudioSource hitSound;
    public AudioSource hitEffectSound;
    private float collisionForce = 200f;
    public bool startMusicEnabled = false;



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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enable Start Music"))
        {
            startMusicEnabled = true;
        }

        if (other.CompareTag("Portal"))
        {
            isGameCleared = true;
            gameClearScreen.SetActive(true);
            gameClearSound.Play();
            FindFirstObjectByType<HeadGroundCheck>().backgroundMusic.Stop();
            StartCoroutine(BackToMain());
            FindFirstObjectByType<PlayerMovement>().playerControls.Disable();
        }
    }

    IEnumerator BackToMain()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(0);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!rb.isKinematic && collision.collider.CompareTag("Enemy"))
        {
            EnableRagdoll();
            freelookCam.m_LookAt = hipsBonePos;
            freelookCam.m_Follow = hipsBonePos;
            foreach (var ragdollRb in ragdollRigidbodies)
            {
                ragdollRb.AddForce(new Vector3(0, 1, -1).normalized * collisionForce, ForceMode.Impulse);
            }
            hitSound.Play();
            hitEffectSound.Play();
        }

    }


    void EnableRagdoll()
    {
        FindFirstObjectByType<PlayerMovement>().playerControls.Disable();
        animator.enabled = false;
        foreach (var ragdollRb in ragdollRigidbodies)
        {
            ragdollRb.isKinematic = false;
        }
        foreach (var col in ragdollColliders)
        {
            col.enabled = true;
        }
        mainCollider.enabled = false;
        mainRigidbody.isKinematic = true;
    }

    public void DisableRagdoll()
    {
        AlignPosToHips();
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
        
        FindFirstObjectByType<PlayerMovement>().playerControls.Enable();

    }

    public void AlignPosToHips()
    {
        Vector3 originalHipsPos = hipsBonePos.position;
        transform.position = hipsBonePos.position;
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hitinfo))
        {
            transform.position = new Vector3(transform.position.x, hitinfo.point.y, transform.position.z);
        }
        hipsBonePos.position = originalHipsPos;
    }


}




