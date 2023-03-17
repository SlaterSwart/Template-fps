using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{   
    [SerializeField] float sensX = 80f;
    [SerializeField] float sensY = 10f;
    float mouseX, mouseY;

    private void Update(){
        transform.Rotate(Vector3.up, mouseX * Time.deltaTime);
    }

    public void ReceiveInput (Vector2 mouseInput){
        mouseX = mouseInput.x * sensX;
        mouseY = mouseInput.y *sensY;

    }

}
