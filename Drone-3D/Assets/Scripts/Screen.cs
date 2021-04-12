using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screen : MonoBehaviour
{
    private Vector2 previousPosition = default;

    [SerializeField] private float _forwardSpeed = 5.0f;

    [SerializeField] private Rigidbody _player = null;
    [SerializeField] private float _vertHorSpeed = 10.0f;

    private void OnMouseDrag()
    {
        Vector2 currentPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        Vector2 direction = (currentPosition - previousPosition).normalized;

        

        if (_player != null) {
           
            //Debug.Log("Point: " + currentPosition);

            //Debug.Log("Direction: " + direction);

            _player.velocity = new Vector3(direction.x * _vertHorSpeed, direction.y * _vertHorSpeed, _forwardSpeed);
        }

        previousPosition = currentPosition;
    }

    private void OnMouseUp()
    {
        _player.velocity = new Vector3(0.0f, 0.0f, _forwardSpeed);
    }

    void Start()
    {
        _player.velocity = new Vector3(0.0f, 0.0f, _forwardSpeed);
    }

    void Update()
    {
        
    }
}
