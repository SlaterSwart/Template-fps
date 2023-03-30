using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController), typeof(PlayerInput))]
public class PickUp : MonoBehaviour
{
    static bool enemyDead;
    public bool equippedByEnemy;
    private PlayerCon controls;

    private bool FPress;
    private bool Gpress;
    public GameObject gunScript; //reference to gun script
    public Rigidbody rb; //reference to ridge body
    public BoxCollider BC; 
    public Transform player, Holder /*gun holder*/, fpsCam;

    public float pickUpRange;
    public float dropFowardForce, dropUpwardForce;

    public bool equipped;
    public static bool slotFull;
    private PlayerInput playerInput;
    private InputAction dropKey;
    private InputAction pickUpKey;

    private void Start(){
        playerInput = GetComponent<PlayerInput>();
        dropKey = playerInput.actions["Drop"];
        pickUpKey = playerInput.actions["Pickup"];
        if(!equipped){
            gunScript.GetComponent<Gun>().enabled = false;
            rb.isKinematic = false;
            BC.isTrigger = false;
        }
        if(equippedByEnemy){
            rb.isKinematic = true;
            BC.isTrigger = true;
        }
        if(enemyDead){
            equippedByEnemy = false;
            rb.isKinematic = false;
            BC.isTrigger = false;
        }
        if(equipped){
            gunScript.GetComponent<Gun>().enabled = true;
            rb.isKinematic = true;
            BC.isTrigger = true;
            slotFull = true;
        }
    }
    private void Update(){
    
        Vector3 distanceToPlayer = player.position - transform.position;
        if (!equipped && distanceToPlayer.magnitude <= pickUpRange && pickUpKey.triggered && !slotFull) {
            PickingUp();
            //Debug.Log("Pickup");
        }

        
        if (equipped && dropKey.triggered)
        {
            Drop();
            //Debug.Log("Drop");
        }
    }
    private void PickingUp(){
        equipped = true;
        slotFull = true;

        //make weapon a child of the cam and move to hands
        rb.isKinematic = true;
        BC.isTrigger = true;
        transform.SetParent(Holder);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = new Vector3(0.2980717f, 1.432908f, 3.140973f);


     

        //enable gun script
        gunScript.GetComponent<Gun>().enabled = true;

    }
    private void Drop(){
        equipped = false;
        slotFull = false;

        //set parent to null to get rid of it being a child aka drop
        transform.SetParent(null, true);
    
        rb.isKinematic = false;
        BC.isTrigger = false;

        //carry momentum from player
        rb.velocity = player.GetComponent<Rigidbody>().velocity;

        //add force
        rb.AddForce(fpsCam.forward * dropFowardForce, ForceMode.Impulse);
        rb.AddForce(fpsCam.up * dropUpwardForce, ForceMode.Impulse);
        //Random flips
        float random = Random.Range(-1f,1f);
        rb.AddTorque(new Vector3(random, random, random) * 10);
        //disable gun script
        gunScript.GetComponent<Gun>().enabled = false;
    }
}
