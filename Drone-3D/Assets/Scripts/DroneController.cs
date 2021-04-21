using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneController : MonoBehaviour
{
    private Vector2 previousPosition = default;

    [SerializeField] private DroneBehaviour _player = null;
    [SerializeField] private float _vertHorSpeed = 10.0f;

    private void OnMouseDrag()
    {
        Vector2 currentPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        Vector2 direction = (currentPosition - previousPosition).normalized;  

        if (_player != null) {            

            _player.Body.velocity = new Vector3(direction.x * _vertHorSpeed, direction.y * _vertHorSpeed, _player.Body.velocity.z);
        }

        previousPosition = currentPosition;
    }

    private void OnMouseUp()
    {
        _player.Body.velocity = new Vector3(0.0f, 0.0f, _player.Body.velocity.z);
    }

    void Start()
    {
        _player.Body.velocity = new Vector3(0.0f, 0.0f, _player.Body.velocity.z);
    }

    void Update()
    {
        
    }
}
