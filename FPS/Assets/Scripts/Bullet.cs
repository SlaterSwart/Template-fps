using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float Fov;
    public float life = 3;
    private void Awake()
    {
        Destroy(gameObject, life);
    }
    private void Update()
    {
       
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy") Destroy(collision.gameObject);
        Destroy(gameObject);
    }
}
