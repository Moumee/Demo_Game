using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    CharacterController controller;

    public float moveSpeed = 10f;
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        Vector3 moveDir = new Vector3 (horizontal, 0, vertical);
        controller.Move(moveDir * moveSpeed * Time.deltaTime);
    }
}
