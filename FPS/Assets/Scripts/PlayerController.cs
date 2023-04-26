using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController), typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    private float playerSpeed = 2.0f;

    private float jumpHeight = 1.0f;

    private float gravityValue = -9.81f;

    private CharacterController controller;
    private PlayerInput playerInput;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    Vector3 moveDirection;

    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction mouseAction;
    private InputAction aimAction;
    private InputAction sprintAction;

    private GameObject Camera;
    public float camera_FOV = 0f;
    public float default_FOV = 60f;
    public float zoomM = 2;
    public Transform Orientation;
    private Transform restPos;
    private Transform zoomPos;
    public GameObject Holder;

    Vector2 mouseInput;
    private void Awake()
    {
        Cursor.visible = false;
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["jump"];
        aimAction = playerInput.actions["Aim"];
        sprintAction = playerInput.actions["Sprint"];
        Camera = GameObject.Find("Main Camera");

        Holder = GameObject.Find("Holder");
        restPos.position = Holder.transform.localPosition;
        zoomPos.position = new Vector3(0.342999995f, -1.38900006f, 0.497999996f);

    }

    private float t = 0;
    
    private IEnumerator ZoomOutE(GameObject Camera, float camera_FOV, GameObject Holder)
    {
        
        while (t > 0)
        {
            t -= 5 * Time.deltaTime;
            camera_FOV = Mathf.Lerp(default_FOV, default_FOV / zoomM, t);
            Camera.GetComponent<Camera>().fieldOfView = camera_FOV;
            Holder.transform.localPosition = new Vector3(Mathf.Lerp(restPos.position.x, zoomPos.position.x, t), Mathf.Lerp(restPos.position.y, zoomPos.position.y, t), Mathf.Lerp(restPos.position.z, zoomPos.position.z, t));
            //Debug.Log(camera_FOV);
            yield return null;
        }

    }
    void Update()
    {
        Camera.GetComponent<Camera>().fieldOfView = camera_FOV;

        if (sprintAction.IsPressed()) playerSpeed = 4.0f; //Sprint
        else playerSpeed = 2.0f;

        if (aimAction.IsPressed()) //aim
        {
            if (t <= 1)
            {
                t += 5 * Time.deltaTime;
            }
            camera_FOV = Mathf.Lerp(default_FOV, default_FOV / zoomM, t);

            //Debug.Log("Aim");
        }
        else if (!aimAction.IsPressed())
        if(aimAction.WasReleasedThisFrame())
        {
            StartCoroutine(ZoomOutE(Camera, camera_FOV, Holder));
                camera_FOV = default_FOV;
        }
        Cursor.lockState = CursorLockMode.Locked;
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0) //jump
        {
            playerVelocity.y = 0f;
        }

        Vector2 input = moveAction.ReadValue<Vector2>();
        Vector3 move = new Vector3(input.x, 0, input.y);
        //Vector3 moveDirection = Orientation.forward * input.y + Orientation.right * input.x;
        controller.Move(move * Time.deltaTime * playerSpeed);

        // Changes the height position of the player..
        if (jumpAction.triggered && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

    }
}