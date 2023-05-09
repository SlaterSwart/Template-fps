using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwitchWeapon : MonoBehaviour
{

    private PlayerInput playerInput;

    private InputAction GrenadeInput;
    private InputAction PrimaryInput;

    private GameObject PrimaryGun;
    private GameObject Grenade;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        GrenadeInput = playerInput.actions["Grenade"];
        PrimaryInput = playerInput.actions["Primary"];

        
    }

    private void Update()
    {
        Grenade = GameObject.Find("GrenadeObj");

        if (PrimaryInput.triggered)
        {
            Grenade.SetActive(false);

        }

    }
}
