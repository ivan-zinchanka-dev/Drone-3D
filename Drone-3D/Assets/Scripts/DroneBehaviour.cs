using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneBehaviour : MonoBehaviour
{

    [SerializeField] private float _forwardSpeed = 5.0f;
    private Rigidbody _body;


    // Start is called before the first frame update
    void Start()
    {
        _body = GetComponent<Rigidbody>();
        _body.velocity = new Vector3(0.0f, 0.0f, _forwardSpeed);

    }

    // Update is called once per frame
    void Update()
    {
        
        

    }
}
