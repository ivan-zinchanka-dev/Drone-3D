using System;
using UnityEngine;

public class DroneBehaviour : MonoBehaviour
{
    [SerializeField] private float _forwardSpeed = 5.0f;
    public static event Action DroneFailure = null;
    private ParticleSystem _explosion;

    

    const float _timeToDelete = 2.5f;

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

        _explosion = GetComponentInChildren<ParticleSystem>();
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

        DroneFailure += Explosion;
        DroneFailure += BeginFall;
    }

    private void Explosion() {

        if (_explosion != null)
        {
            _explosion.transform.parent = null;
            _explosion.Play(true);
            _explosion = null;
        }
    }

    private void BeginFall() {

        Body.constraints = RigidbodyConstraints.None;
        Body.useGravity = true;
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
        
        if (DroneFailure != null) DroneFailure();
        
        Invoke("DeleteFromScene", _timeToDelete);
    }

    void DeleteFromScene() {

        this.gameObject.SetActive(false);        
    }

    void Update()
    {
        
        

    }
}
