using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Audio;

[RequireComponent(typeof(CharacterController), typeof(PlayerInput))]
public class Gun : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction shootAction;
    public Transform bulletSpawnPoint;

    public GameObject bulletPrefab;
    public float bulletSpeed = 50;

    //audio stuff
    public AudioSource source;
    public AudioClip clip;
    private void Awake()
    { 
        playerInput = GetComponent<PlayerInput>();
        shootAction = playerInput.actions["Shoot"];

    }


    private void Update()
    {

        if (shootAction.triggered)
        {
            //Debug.Log("shot");
            var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
            source.PlayOneShot(clip);
        }
    }
}
