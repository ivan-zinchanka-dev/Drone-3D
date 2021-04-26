using UnityEngine;

public class Follow : MonoBehaviour
{
    [SerializeField] Transform target = null;
    [SerializeField] float offset = 4.0f;


    private void Start()
    {
        //GameManager.OnSessionComplete += delegate ()
        //{
            
        //};
    }




    void Update()
    {
        if (target != null) {

            this.transform.position = new Vector3(transform.position.x, transform.position.y, target.transform.position.z - offset);
        }
    }
}
