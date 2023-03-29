using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook : MonoBehaviour
{
    private PlayerCon controls;
    private float mouseSens = 80f;
    private Vector2 mouseLook;
    private float xRotation = 0f;
    private float yRotation = 0f;
    public Transform playerBody;
    public Transform Orientation;

    void Awake(){
        playerBody = transform.parent;

        controls = new PlayerCon();
    }
    void Update()
    {
        Look();
    }
    private void Look()
    {
        mouseLook = controls.Land.Look.ReadValue<Vector2>();

        float mouseX = mouseLook.x * mouseSens * Time.deltaTime;
        float mouseY = mouseLook.y * mouseSens * Time.deltaTime;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); //clamp

        transform.rotation = UnityEngine.Quaternion.Euler(xRotation, yRotation, 0);
        playerBody.rotation = UnityEngine.Quaternion.Euler(0, yRotation, 0);
    }
    private void OnEnable()
    {
        controls.Enable();
    }
    private void OnDisable()
    {
        controls.Disable();
    }
}
