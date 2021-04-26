using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingField : MonoBehaviour
{
    private DroneBehaviour _player = null; 

    public float _landingSpeed = default;
    private Vector3 targetPosition = default;

    private bool _passangersArrived = false;
    [SerializeField] private float _timeBeforeLanding = default;

    private float minHight = default;
    //private float maxHight = default;

    private void OnTriggerEnter(Collider other)
    {
        if (_player != null) return;

        if ((_player = other.GetComponent<DroneBehaviour>()) != null) {
        
            _player.ForwardSpeed = 0.0f;
            minHight = transform.position.y + transform.lossyScale.y / 2.0f + ItemsList.instance.GetLength();
            //maxHight = 9.50f;
            //Debug.Log("Height: " + minHight);
            GameManager.EndGame();
        }
    }


    private void PassengersLanding() {

        ItemsList.instance.OnDroneCollision();
    }

    void Update()
    {
        if (_player == null) return;

        targetPosition = new Vector3(transform.position.x, minHight, transform.position.z);
        _player.transform.position = Vector3.MoveTowards(_player.transform.position, targetPosition, _landingSpeed);

        if (_player.transform.position == targetPosition && !_passangersArrived)
        {
            Invoke("PassengersLanding", _timeBeforeLanding);
            _passangersArrived = true;
        }









    }
}
