using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

[RequireComponent(typeof(CharacterController), typeof(PlayerInput))]
public class Grenade : MonoBehaviour
{

    public float throwSpeed;

    public float fuseTime;

    private PlayerInput playerInput;
    private InputAction Throw;
    public GameObject grenade;
    public int GrenadeSpeed;
    public Rigidbody RB;
    public SphereCollider BoxC;
    public bool IsThrown = false;


    public GameObject explosionEffect;

    public float explosionForce = 10f;
    public float blastRad = 30f;


    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        Throw = playerInput.actions["Throw"];
        //Grenade is set in editor
    }
    private void Update()
    {

        if (Throw.triggered && IsThrown == false)
        {
            RB.isKinematic = false;
            BoxC.isTrigger = false;            
            RB.velocity = grenade.GetComponent<Transform>().forward * GrenadeSpeed;
            grenade.GetComponent<Transform>().SetParent(null, true);
            IsThrown = true;
        }

        if (fuseTime > 0 && IsThrown == true){
            fuseTime -= Time.deltaTime;
            Debug.Log(fuseTime);
            //Debug.Log(fuseTime);
        }
        if(fuseTime <= 0) {
            Explode();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject);
        
        
            
        //Destroy(gameObject);
    }

    private void Explode(){
        Collider[] colliders = Physics.OverlapSphere(transform.position, blastRad);

        foreach(Collider near in colliders){
            Rigidbody rig = near.GetComponent<Rigidbody>();

            if(rig != null){
                rig.AddExplosionForce(explosionForce, transform.position, blastRad, 1f, ForceMode.Impulse);
            }

        }

        Instantiate(explosionEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
