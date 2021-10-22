using UnityEngine;

public class Follow : MonoBehaviour
{
    [SerializeField] Transform target = null;
    [SerializeField] float offset = 4.0f;

    void Update()
    {
        if (target != null) {

            //this.transform.position = new Vector3(transform.position.x, transform.position.y, target.transform.position.z - offset);

            this.transform.position = Vector3.Lerp(this.transform.position, new Vector3(transform.position.x, transform.position.y, target.transform.position.z - offset), 0.1f);

        }
    }
}
