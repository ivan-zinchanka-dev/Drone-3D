using System;
using UnityEngine;

public class DroneBehaviour : MonoBehaviour
{
    [SerializeField] private float _forwardSpeed = 5.0f;
    public static event Action DroneFailure = null;

    public float ForwardSpeed { 
        
        get { 

            return _forwardSpeed; 
        } 
        private set { 

            _forwardSpeed = value; 
        } 
    }
    
    public Rigidbody Body { get; private set; }


    private void Awake()
    {
        Body = GetComponent<Rigidbody>();
        Body.velocity = Vector3.zero;
    }


    private void BeginMoveForward() {

        Body.velocity = new Vector3(0.0f, 0.0f, _forwardSpeed);
    }

    void Start()
    {
        GameManager.OnSessionStart += BeginMoveForward;
        GameManager.OnSessionResume += BeginMoveForward;

        GameManager.OnSessionPause += delegate ()
        {
            Body.velocity = Vector3.zero;
        };        
    }


    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision!");

        Item metObject = null;
        
        if ((metObject = collision.gameObject.GetComponentInParent<Item>()) != null) {

            if (metObject.IsFree) ItemsList.instance.Add(metObject);
            BeginMoveForward();
            return;
        }

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
