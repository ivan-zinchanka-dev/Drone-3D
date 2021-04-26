using System;
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


    private Vector3 CorrectVelocity(Vector3 sourcePosition, Vector3 sourceVelocity, in MovingConstraints movingConstraints) {

        Vector3 correctedVelocity = sourceVelocity;

        if (sourcePosition.x < movingConstraints.left && sourceVelocity.x < 0.0f ||
            sourcePosition.x > movingConstraints.right && sourceVelocity.x > 0.0f) {

            correctedVelocity.x = 0.0f;
        }
      
        if (sourcePosition.y < movingConstraints.lower && sourceVelocity.y < 0.0f ||
            sourcePosition.y > movingConstraints.upper && sourceVelocity.y > 0.0f) {

            correctedVelocity.y = 0.0f;
        } 

        return correctedVelocity;
    }

    private void OnMouseDrag()
    {
        if (IsLocked || _player == null) return;

        Vector2 currentPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);      
        Vector2 direction = (currentPosition - previousPosition).normalized;
                
        _player.Body.velocity = new Vector3(direction.x * _vertHorSpeed, direction.y * _vertHorSpeed, _player.Body.velocity.z);
        _player.Body.velocity = CorrectVelocity(_player.Body.position, _player.Body.velocity, in _movingConstraints); 

        previousPosition = currentPosition;
    }

    private void OnMouseUp()
    {
        if (IsLocked || _player == null) return;

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
