using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GZombie_PlayerMove : MonoBehaviour
{

    private float movingSpeed = 20f;
    private float rotateSpeed = 100f;

    //private PlayerInput playerInput;
    private Rigidbody playerRigidbody;

    bool isJump = false;


    private void Start()
    {
        //playerInput = GetComponent<PlayerInput>();
        playerRigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Move();
        Jump();
        RotateWithKey();
    }

    private void Move()
    {
        float axisV = Input.GetAxis("Vertical");
        float axisY = Input.GetAxis("Horizontal");

        transform.Translate(Vector3.forward * axisV * movingSpeed * Time.deltaTime);
        transform.Translate(Vector3.right * axisY * movingSpeed * Time.deltaTime);
    }

    private void Jump()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            playerRigidbody.AddForce(Vector3.up * 5, ForceMode.Impulse);
        }
    }

    private void RotateWithKey()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(Vector3.down, rotateSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
        }
    }
}// end of class
  

