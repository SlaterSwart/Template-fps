using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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

    public bool equipped;
    public static bool slotFull;

    private void Start(){
        controls = new PlayerCon();
        if(!equipped){
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
    private void Update(){
        FPress = controls.Land.Pickup.ReadValue<bool>();
        Vector3 distanceToPlayer = player.position - transform.position;
        if (!equipped && distanceToPlayer.magnitude <= pickUpRange && FPress && !slotFull) {
            PickingUp();
            Debug.Log("Pickup");
        }

        Gpress = controls.Land.Drop.ReadValue<bool>();
        if (equipped && Gpress)
        {
            Drop();
            Debug.Log("Drop");
        }
    }
    private void PickingUp(){
        equipped = true;
        slotFull = true;

        //make weapon a child of the cam and move to hands
        transform.SetParent(Holder);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;

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
        float random = Random.Range(-1f,1f);
        rb.AddTorque(new Vector3(random, random, random) * 10);
        //disable gun script
        gunScript.GetComponent<Gun>().enabled = false;
    }
}
