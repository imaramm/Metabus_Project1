using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GZombie_PlayerShoot : MonoBehaviour
{
    public GZombie_Gun GZombie_gun;
    public Transform gunPivot;
    private GZombie_PlayerInput GZombie_playerInput;


    private void Start()
    {
        GZombie_playerInput = GetComponent<GZombie_PlayerInput>();
    }

    private void OnEnable()
    {
        GZombie_gun.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        GZombie_gun.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (GZombie_playerInput.fire)
        {
            GZombie_gun.Fire();
        }

    }







}// end of class
