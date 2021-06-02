using UnityEngine;

public class LandingField : MonoBehaviour
{
    private DroneBehaviour _player = null; 

    public float _landingSpeed = default;
    private Vector3 targetPosition = default;

    private bool _passangersArrived = false;
    [SerializeField] private float _timeBeforeLanding = default;

    private float minHight = default;

    private void OnTriggerEnter(Collider other)
    {
        if (_player != null) return;

        if (other.TryGetComponent<DroneBehaviour>(out _player)) {
        
            _player.ForwardSpeed = 0.0f;
            minHight = transform.position.y + 2.0f + transform.lossyScale.y / 2.0f + HumanChain.instance.GetLength();
            
            Collider[] colliders = _player.GetComponents<Collider>();

            foreach (var collider in colliders) {

                collider.enabled = false;
            }

            GameManager.EndGame(true);
        }
    }


    private void PassengersLanding() {
         
        HumanChain.instance.OnChainUngrouping();
    }

    void Update()
    {
        if (_player == null) return;

        targetPosition = new Vector3(transform.position.x, minHight, transform.position.z);
        _player.transform.position = Vector3.MoveTowards(_player.transform.position, targetPosition, _landingSpeed * Time.deltaTime);

        if (_player.transform.position == targetPosition && !_passangersArrived)
        {
            Invoke(nameof(PassengersLanding), _timeBeforeLanding);
            _passangersArrived = true;
        }

    }
}
