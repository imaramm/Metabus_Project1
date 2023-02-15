using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GZombie_PlayerInput : MonoBehaviour
{

    public string moveAxisVName = "Vertical";
    public string moveAxisYName = "Horizontal";
<<<<<<< HEAD
    public string fireButtonName = "Fire1";
    public string reloadButtonName = "Reload";

    public float moveV { get; private set; }
    public float moveH { get; private set; }
    public bool fire { get; private set; }
    public bool reload { get; private set; }
    
=======

    public float moveV { get; private set; }
    public float moveY { get; private set; }
    public float rotate { get; private set; }


    void Start()
    {
        
    }
>>>>>>> 87d098ba57b49cd0b682cd49d53abb835e94485c

    void Update()
    {
        moveV = Input.GetAxis(moveAxisVName);
<<<<<<< HEAD
        moveH = Input.GetAxis(moveAxisYName);
        fire = Input.GetButton(fireButtonName);
        reload = Input.GetButton(reloadButtonName);
=======
        moveY = Input.GetAxis(moveAxisYName);
       
>>>>>>> 87d098ba57b49cd0b682cd49d53abb835e94485c
    }


}// end of class


