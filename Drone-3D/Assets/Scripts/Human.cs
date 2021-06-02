using UnityEngine;

public class Human : MonoBehaviour
{
    public bool IsFree = true;
    private Renderer _mesh = null;
    private ParticleSystem _blood = null;

    public bool isFalling = false;

    private void Start()
    {
        _mesh = GetComponentInChildren<Renderer>();
        _blood = GetComponentInChildren<ParticleSystem>();

        if (_mesh != null && _blood != null) {

            _blood.startColor = _mesh.material.color;
        }        
    }

    private bool IsBarrier(Collider other) {

        return !other.CompareTag("Player")
            && other.GetComponentInParent<Human>() == null
            && !other.TryGetComponent<Blower>(out Blower blower)
            && !other.TryGetComponent<LandingField>(out LandingField lanf);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (isFalling)
        {
            if (other.CompareTag("Impassable"))
            {
                if (_blood != null)
                {
                    _blood.Play();
                    _blood.transform.parent = null;
                }

                Destroy(this.gameObject);
            }

            return;
        }

        Human otherHuman = null;

        if (other.TryGetComponent<Human>(out otherHuman) && otherHuman.isFalling) {

            return;        
        }

        if (other.TryGetComponent<Human>(out otherHuman))
        {
            if (!IsFree && otherHuman.IsFree)
            {
                HumanChain.instance.Add(otherHuman);
            }
        }
        else if (IsBarrier(other))
        {
            try
            {
                HumanChain.instance.OnHumanCollision(this);
            }
            catch (System.Exception ex) {

                Debug.Log(ex.Message);           
            }           
        }
                    
    }   

    public void Fall()
    {
        isFalling = true;    
        transform.parent = null;

        this.gameObject.AddComponent<Rigidbody>();
    }

}
