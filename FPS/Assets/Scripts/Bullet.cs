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
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy"){
            
            collision.gameObject.GetComponentInChildren<Transform>().SetParent(null, true);
            Destroy(collision.gameObject);
        }
        Destroy(gameObject);
    }
}
