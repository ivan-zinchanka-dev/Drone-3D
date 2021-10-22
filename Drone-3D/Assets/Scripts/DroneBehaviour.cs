using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DroneBehaviour : MonoBehaviour
{
    [SerializeField] private float _forwardSpeed = 5.0f;
    [SerializeField] private Rigidbody _body = null;
    public static event Action DroneFailure = null;
    [SerializeField] private ParticleSystem _explosion = null;

    const float _timeToDelete = 2.5f;

    public float ForwardSpeed { 
        
        get { 

            return _forwardSpeed; 
        } 
        set { 

            _forwardSpeed = value;
            BeginMoveForward();
        } 
    }
    
    public Rigidbody Body { get { return _body; } }


    private void Awake()
    {
        Body.velocity = Vector3.zero;
    }

    private void BeginMoveForward() {

        Body.velocity = new Vector3(0.0f, 0.0f, _forwardSpeed);
    }


    private void OnEnable()
    {
        GameManager.OnSessionStart += BeginMoveForward;
        GameManager.OnSessionResume += BeginMoveForward;

        GameManager.OnSessionPause += delegate ()
        {
            Body.velocity = Vector3.zero;
        };

        DroneFailure += Explosion;
        DroneFailure += BeginFall;

        LevelsManager.OnReloadLevel += delegate ()
        {
            DroneFailure = null;
        };
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

        LevelsManager.Instance.StartCoroutine(GameManager.StartFinalMenu(false, null));
    }    

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision!");

        Human meetedObject = collision.gameObject.GetComponentInParent<Human>();
        
        if (meetedObject != null) {

            if (meetedObject.IsFree) HumanChain.instance.Add(meetedObject);
            BeginMoveForward();
            return;
        }
        
        if (DroneFailure != null) DroneFailure();

        StartCoroutine(DeleteFromScene());

        //Invoke(nameof(DeleteFromScene), _timeToDelete);
    }

    private IEnumerator DeleteFromScene() {

        yield return new WaitForSeconds(_timeToDelete);

        this.gameObject.SetActive(false);
    }

    //void DeleteFromScene() {

    //    this.gameObject.SetActive(false);        
    //}

}
