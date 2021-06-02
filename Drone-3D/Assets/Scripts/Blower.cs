using UnityEngine;

public class Blower : MonoBehaviour
{
    private Rigidbody _capturedObject = null;
    [SerializeField] private Vector3 _force = new Vector3( 0.5f, 0.0f, 0.0f);

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {

            other.TryGetComponent<Rigidbody>(out _capturedObject); 
        } 
    }

    private void OnTriggerStay(Collider other)
    {
        if(_capturedObject != null) _capturedObject.AddForce(_force, ForceMode.VelocityChange);
    }


}
