using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Audio;
using TMPro;

[RequireComponent(typeof(CharacterController), typeof(PlayerInput))]
public class Gun : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction shootAction;
    private InputAction reloadAction;

    public TMP_Text ammoDisplay;// text display
    public Transform bulletSpawnPoint;

    public GameObject bulletPrefab;
    public float bulletSpeed = 50;
    public bool fullAuto;
    public bool burst;
    public int Ammo;
    private int ogAmmo;

    //audio stuff
    public AudioSource source;
    public AudioClip clip;
    private void Awake()
    { 
        playerInput = GetComponent<PlayerInput>();
        shootAction = playerInput.actions["Shoot"];
        reloadAction = playerInput.actions["Reload"];
        ogAmmo = Ammo; 
    }


    private void Update()
    {
        ammoDisplay.text = Ammo.ToString(); //display Ammo
        if (shootAction.triggered && Ammo >= 1)
        {
            //Debug.Log("shot");
            var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
            source.PlayOneShot(clip);
            Ammo--; //take ammo away
        }
        if (Ammo < ogAmmo && reloadAction.triggered)
        { 
            Ammo = ogAmmo; //set back to defalt for that gun
        }
    }
}
