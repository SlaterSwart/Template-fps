using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController), typeof(PlayerInput))]
public class PickUp : MonoBehaviour
{
    private PlayerCon controls;

    private bool FPress;
    private bool Gpress;
    public GameObject gunScript; //reference to gun script
    public Rigidbody rb; //reference to ridge body
    public BoxCollider BC; 
    public Transform player, Holder /*gun holder*/, fpsCam;

    public float pickUpRange;
    public float dropFowardForce, dropUpwardForce;

    private PlayerInput playerInput;
    private InputAction pickingUpAction;
    private InputAction dropAction;
    public bool equipped;
    public static bool slotFull;

    private void Start() {
        playerInput = GetComponent<PlayerInput>();
        pickingUpAction = playerInput.actions["Pickup"];
        dropAction = playerInput.actions["Drop"];

        if (!equipped){
            gunScript.GetComponent<Gun>().enabled = false;
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
    private void Update() { 
        Vector3 distanceToPlayer = player.position - transform.position;
        if (!equipped && distanceToPlayer.magnitude <= pickUpRange && pickingUpAction.triggered && !slotFull) {
            PickingUp();
            Debug.Log("Pickup");
        }


        if (equipped /*&& dropAction.triggered*/)
        {
            if (dropAction.triggered)
            {
            Drop();

            Debug.Log("Drop");
            }
          
        }
    }
    private void PickingUp(){
        equipped = true;
        slotFull = true;

        //make weapon a child of the cam and move to hands
        transform.SetParent(Holder);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = new Vector3(0.2980717f, 1.432908f, 3.140973f);

        rb.isKinematic = true;
        BC.isTrigger = true;

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
        float random = Random.Range(-0.4f,0.2f);
        rb.AddTorque(new Vector3(random, random, random) * 5);
        //disable gun script
        gunScript.GetComponent<Gun>().enabled = false;
    }
}
