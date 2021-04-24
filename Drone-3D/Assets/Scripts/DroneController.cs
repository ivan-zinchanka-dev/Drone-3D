using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneController : MonoBehaviour
{
    private Vector2 previousPosition = default;

    [SerializeField] private DroneBehaviour _player = null;
    [SerializeField] private float _vertHorSpeed = 10.0f;           // X, Y

    public bool IsLocked { get; set; } = true;

    [SerializeField] private MovingConstraints _movingConstraints = new MovingConstraints() { upper = 15.0f, lower = 5.0f };

    [Serializable]
    private struct MovingConstraints {

        public float left;
        public float right;
        public float upper;
        public float lower;       
    }


    private void CorrectPosition(ref Vector3 sourcePosition, in MovingConstraints movingConstraints, float offset) {

        if (offset == 0.0f) throw new ArgumentOutOfRangeException("offset");
        if (offset < 0.0f) offset = Mathf.Abs(offset);
        
        if (sourcePosition.x < movingConstraints.left) {

            sourcePosition.x = movingConstraints.left + offset;
        }

        if (sourcePosition.x > movingConstraints.right) {

            sourcePosition.x = movingConstraints.right - offset;
        }

        if (sourcePosition.y < movingConstraints.lower) {

            Debug.Log(sourcePosition.y + " " + movingConstraints.lower);

            sourcePosition.y = movingConstraints.lower + offset;
        } 
        
        if(sourcePosition.y > movingConstraints.upper) {

            sourcePosition.y = movingConstraints.upper - offset;
        }

    }

    private void OnMouseDrag()
    {
        if (IsLocked) return;

        Vector2 currentPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);


        Vector3 playerPos = _player.Body.position;
        CorrectPosition(ref playerPos, in _movingConstraints, 0.25f);
        _player.Body.position = playerPos;


        Vector2 direction = (currentPosition - previousPosition).normalized;  

        if (_player != null) {            
           
            _player.Body.velocity = new Vector3(direction.x * _vertHorSpeed, direction.y * _vertHorSpeed, _player.Body.velocity.z);
        }

        previousPosition = currentPosition;
    }

    private void OnMouseUp()
    {
        if (IsLocked) return;

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
