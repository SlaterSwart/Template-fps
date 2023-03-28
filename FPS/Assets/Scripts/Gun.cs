using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController), typeof(PlayerInput))]
public class Gun : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction shootAction;
    private InputAction aimAction;
    public Transform bulletSpawnPoint;
    private GameObject Camera;
    public float camera_FOV = 0f;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10;
    private void Awake()
    { 
        playerInput = GetComponent<PlayerInput>();
        shootAction = playerInput.actions["Shoot"];
        aimAction = playerInput.actions["Aim"];
        Camera = GameObject.Find("Main Camera");
    }


    private void Update()
    {
        Camera.GetComponent<Camera>().fieldOfView = camera_FOV;
        if (aimAction.IsPressed())
        {
            camera_FOV = 40;
        }
        else if(!aimAction.IsPressed()) camera_FOV = 60;

        if (shootAction.triggered)
        {
            var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
 
        }
    }
}
