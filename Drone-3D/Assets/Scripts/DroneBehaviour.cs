using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneBehaviour : MonoBehaviour
{

    [SerializeField] public float forwardSpeed = 5.0f;

    public float ForwardSpeed { 
        
        get { 

            return forwardSpeed; 
        } 
        private set { 

            forwardSpeed = value; 
        } 
    }
    
    private Rigidbody _body;


    void Start()
    {
        _body = GetComponent<Rigidbody>();
        _body.velocity = new Vector3(0.0f, 0.0f, forwardSpeed);
    }


    private void OnCollisionEnter(Collision collision)
    {     
        if (collision.gameObject.GetComponentInParent<Item>() != null) return;
       
        //this.gameObject.SetActive(false);
    }

    void Update()
    {
        
        

    }
}
