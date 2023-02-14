using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GZombie_PlayerMove : MonoBehaviour
{

    private float movingSpeed = 20f;
    private float rotateSpeed = 100f;



    private void Update()
    {
        Moving();
        RotateWithKey();
    }

    private void Moving()
    {
        float axisV = Input.GetAxis("Vertical");
        float axisY = Input.GetAxis("Horizontal");

        transform.Translate(Vector3.forward * axisV * movingSpeed * Time.deltaTime);
        transform.Translate(Vector3.right * axisY * movingSpeed * Time.deltaTime);
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
  

