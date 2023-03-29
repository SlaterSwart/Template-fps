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

    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction mouseAction;
    private InputAction aimAction;

    private GameObject Camera;
    public float camera_FOV = 0f;

    Vector2 mouseInput;
    private void Awake()
    {
        Cursor.visible = false;
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["jump"];
        aimAction = playerInput.actions["Aim"];
        Camera = GameObject.Find("Main Camera");
        
    }


    void Update()
    {
        Camera.GetComponent<Camera>().fieldOfView = camera_FOV;
        if (aimAction.IsPressed())
        {
            camera_FOV = 40;
            //Debug.Log("Aim");
        }
        else if(!aimAction.IsPressed()) camera_FOV = 60;
        Cursor.lockState = CursorLockMode.Locked;
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector2 input = moveAction.ReadValue<Vector2>();
        Vector3 move = new Vector3(input.x, 0, input.y);
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