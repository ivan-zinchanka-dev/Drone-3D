using UnityEngine;
using UnityEngine.EventSystems;

public class DroneController : MonoBehaviour /*, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler */
{
    [SerializeField] private float _forwardSpeed = 5.0f;
    [SerializeField] private float _vertHorSpeed = 5.0f;

    private Rigidbody _body;

    private Vector2 previousPosition = default;

    //public void OnBeginDrag(PointerEventData eventData)
    //{
    //    Debug.Log("BEGIN!");
    //}

    //public void OnDrag(PointerEventData eventData)
    //{
    //    Vector2 currentPosition = eventData.position;
    //    Vector2 direction = (currentPosition - previousPosition).normalized;

    //    _body.velocity = new Vector3(direction.x * _vertHorSpeed, direction.y * _vertHorSpeed, _forwardSpeed);

    //    previousPosition = currentPosition;

    //    Debug.Log("Direction: " + direction);
    //}

    //public void OnEndDrag(PointerEventData eventData)
    //{
    //    _body.velocity = new Vector3(0.0f, 0.0f, _forwardSpeed);

    //}

    //public void OnPointerDown(PointerEventData eventData)
    //{
        

    //    Debug.Log("Position: " + eventData.pressPosition);
    //}

    void Start()
    {
        _body = GetComponent<Rigidbody>();
        _body.velocity = new Vector3(0.0f, 0.0f, _forwardSpeed);

    }


    void Update()
    {
        
    }
}
