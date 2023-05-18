using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController), typeof(PlayerInput))]
public class Grenade : MonoBehaviour
{

    public float throwSpeed;

    private PlayerInput playerInput;
    private InputAction Throw;
    public GameObject grenade;
    public int GrenadeSpeed;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        Throw = playerInput.actions["Throw"];
        //Grenade is set in editor
    }
    private void Update()
    {
        if (Throw.triggered)
        {
            grenade.GetComponent<Rigidbody>().velocity = grenade.GetComponent<Transform>().forward * GrenadeSpeed;
            grenade.GetComponent<Transform>().
        }
    }

}
