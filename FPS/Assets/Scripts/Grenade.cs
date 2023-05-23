using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
    public bool IsThrown;

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
        }
        else{
            //expolsion
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject);
        
        
            
        //Destroy(gameObject);
    }
}
