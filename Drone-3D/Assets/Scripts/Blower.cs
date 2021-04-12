using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blower : MonoBehaviour
{
    private Rigidbody _capturedObject = null;
    [SerializeField] private Vector3 _force = new Vector3( 0.5f, 0.0f, 0.0f);

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {

            _capturedObject = other.GetComponent<Rigidbody>(); 
        } 
    }

    private void OnTriggerStay(Collider other)
    {
        _capturedObject.AddForce(_force, ForceMode.VelocityChange);
    }


}
