using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController), typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    public float playerSpeed = 2.0f;
    public float sprintSpeed;
    private float jumpHeight = 1.0f;

    private float gravityValue = -9.81f;

    private CharacterController controller;
    private PlayerInput playerInput;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    Vector3 moveDirection;

    private InputAction moveAction;
    private InputAction jumpAction;
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

    private float OGSpeed;

    Vector2 mouseInput;
    private void Awake()
    {
        Cursor.visible = false;
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];
        aimAction = playerInput.actions["Aim"];
        sprintAction = playerInput.actions["Sprint"];
        Camera = GameObject.Find("Main Camera");
        OGSpeed = playerSpeed;
        //restPos.position = new Vector3(0.626f, -1.5f, 0.5f);//Holder.transform.localPosition;
        //zoomPos.position = new Vector3(0.342999995f, -1.38900006f, 0.497999996f);

    }

    private float t = 0;
    
    private IEnumerator ZoomOutE(GameObject Camera, float camera_FOV, GameObject Holder)
    {
        
        while (t > 0)
        {
            t -= 5 * Time.deltaTime;
            camera_FOV = Mathf.Lerp(default_FOV, default_FOV / zoomM, t);
            Camera.GetComponent<Camera>().fieldOfView = camera_FOV;
            //Holder.transform.localPosition = Vector3.Lerp(restPos.position, zoomPos.position, t);
            //Debug.Log(camera_FOV);
            yield return null;
        }

    }
    void Update()
    {
        Camera.GetComponent<Camera>().fieldOfView = camera_FOV;

        if (sprintAction.IsPressed()) playerSpeed = sprintSpeed; //Sprint
        else playerSpeed = OGSpeed;

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