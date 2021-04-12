using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneBehaviour : MonoBehaviour
{
    [SerializeField] public float forwardSpeed = 5.0f;
    public static event Action DroneFailure = null;

    public float ForwardSpeed { 
        
        get { 

            return forwardSpeed; 
        } 
        private set { 

            forwardSpeed = value; 
        } 
    }
    
    public Rigidbody Body { get; private set; }


    void Start()
    {
        Body = GetComponent<Rigidbody>();
        Body.velocity = new Vector3(0.0f, 0.0f, forwardSpeed);
    }


    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision!");


        if (collision.gameObject.GetComponentInParent<Item>() != null) return;

        Body.constraints = RigidbodyConstraints.None;
        Body.useGravity = true;

        if (DroneFailure != null) DroneFailure();

        Invoke("Explosion", 2.5f);

    }

    void Explosion() {

        this.gameObject.SetActive(false);

    }

    void Update()
    {
        
        

    }
}
