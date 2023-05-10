using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController), typeof(PlayerInput))]
public class SwitchWeapon : MonoBehaviour
{

    private PlayerInput playerInput;

    private InputAction grenadeInput;
    private InputAction PrimaryInput;

    private GameObject PrimaryGun;
    private GameObject Grenade;

    private bool grenadeActive = false;
    private bool primaryActive = true;
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        grenadeInput = playerInput.actions["Grenade"];
        PrimaryInput = playerInput.actions["Primary"];
        Grenade = GameObject.Find("GrenadeObj");
        PrimaryGun = GameObject.Find("Firearm");
        
    }

    private void Update()
    {
    
        if (PrimaryInput.triggered && grenadeActive == true)
        {
            Grenade.SetActive(false);
            grenadeActive = false;
            primaryActive = true;
            PrimaryGun.SetActive(true);
        }

        if (grenadeInput.triggered && primaryActive == true)
        {
            PrimaryGun.SetActive(false);
            primaryActive = false;
            grenadeActive = true;
            Grenade.SetActive(true);
        }

    }
}
