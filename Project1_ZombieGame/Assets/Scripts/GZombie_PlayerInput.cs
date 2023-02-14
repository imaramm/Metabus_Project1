using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GZombie_PlayerInput : MonoBehaviour
{

    public string moveAxisVName = "Vertical";
    public string moveAxisYName = "Horizontal";

    public float moveV { get; private set; }
    public float moveY { get; private set; }
    public float rotate { get; private set; }


    void Start()
    {
        
    }

    void Update()
    {
        moveV = Input.GetAxis(moveAxisVName);
        moveY = Input.GetAxis(moveAxisYName);
       
    }


}// end of class


