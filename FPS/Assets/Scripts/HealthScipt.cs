using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthScipt : MonoBehaviour
{
    [SerializeField] float HealthVal = 100f;
    public TMP_Text Hp;
    private void Start()
    {
        HealthVal = 100;
    }
    private void Update()
    {
        Hp.text = "HP:" + HealthVal.ToString();
        if (HealthVal <= 0)
        {
            
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
     if (collision.gameObject.tag == "Enemy")
     {
            collision.gameObject.GetComponentInChildren<Transform>().SetParent(null, true);

            if (HealthVal > 0) HealthVal = HealthVal - 10;
           
      }
    }
}


