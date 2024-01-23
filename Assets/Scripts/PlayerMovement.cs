using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
    PlayerControls playerControls;
    InputAction move;
    
    Rigidbody rb;
    public float moveForce = 1f;
    public float jumpForce = 10f;
    public float gravityScale = 4f;
    Vector3 forceDirection = Vector3.zero;
    float maxSpeed = 5f;

    public CinemachineFreeLook freelookCam;
    public Camera cam;
    public Transform groundCheck;
    public LayerMask groundLayer;
    
    void Awake()
    {
        
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnEnable()
    {
        playerControls.Player.Jump.started += DoJump;
        move = playerControls.Player.Move;
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Player.Jump.started -= DoJump;
        playerControls.Player.Disable();

    }

    void DoJump(InputAction.CallbackContext context)
    {
        if (IsGrounded())
        {
            forceDirection += Vector3.up * jumpForce;
        }
    }

    public bool IsGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, 0.1f, groundLayer);
    }

    void FixedUpdate()
    {
        forceDirection += move.ReadValue<Vector2>().x * GetCameraRight(cam) * moveForce;
        forceDirection += move.ReadValue<Vector2>().y * GetCameraForward(cam) * moveForce;

        rb.AddForce(forceDirection, ForceMode.Impulse);
        forceDirection = Vector3.zero;

        if (rb.velocity.y < 0f)
        {
            rb.velocity -= Vector3.down * Physics.gravity.y * Time.fixedDeltaTime;
        }

        Vector3 horizontalVelocity = rb.velocity;
        horizontalVelocity.y = 0f;
        if (horizontalVelocity.sqrMagnitude > maxSpeed * maxSpeed)
        {
            rb.velocity = horizontalVelocity.normalized * maxSpeed + Vector3.up * rb.velocity.y;
        }

        LookAt();
    }

    Vector3 GetCameraRight(Camera cam)
    {
        Vector3 right = cam.transform.right;
        right.y = 0f;
        return right.normalized;
    }

    Vector3 GetCameraForward(Camera cam)
    {
        Vector3 forward = cam.transform.forward;
        forward.y = 0f;
        return forward.normalized;
    }

    void LookAt()
    {
        Vector3 direction = rb.velocity;
        direction.y = 0f;
        if (freelookCam.m_LookAt == transform && freelookCam.m_Follow == transform)
        {
            if (move.ReadValue<Vector2>().sqrMagnitude > 0.1f && direction.sqrMagnitude > 0.1f)
            {
                rb.rotation = Quaternion.LookRotation(direction, Vector3.up);
            }
            else
            {
                rb.angularVelocity = Vector3.zero;
            }
        }
        
    }
}
