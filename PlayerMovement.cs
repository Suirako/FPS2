using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float moveSpeed;
    public float walkSpeed = 12f;
    public float sprintSpeed = 15f;
    public float jumpSpeed = 40f;
    public float gravity = 6f;

    Vector3 moveDirection;

    CharacterController controller;

    public GameObject ok;

    public static bool activeThrust = false;

    void Start(){
        controller = GetComponent<CharacterController>();
    }

    void Update(){
        Move();
    }

    void Move(){
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.A)){
            for (int i = 0; i < 10; i++){
                moveZ += 5f;
            }
        }

        if (controller.isGrounded){
            moveDirection = transform.right * moveX + transform.forward * moveZ;
            if (Input.GetKey(KeyCode.LeftShift) && moveZ == 1){
                moveSpeed = sprintSpeed;
            }
            else{
                moveSpeed = walkSpeed;
            }

            moveDirection *= moveSpeed;

            if (Input.GetKeyDown(KeyCode.Space)){
                moveDirection.y += jumpSpeed;
            }

        }

        else if (!controller.isGrounded && activeThrust){
            if (Input.GetKey(KeyCode.Space)){
                moveDirection.y += 8;
            }
        }

        moveDirection.y -= gravity;
        controller.Move(moveDirection * Time.deltaTime);  
    }
}