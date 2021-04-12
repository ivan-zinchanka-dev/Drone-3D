using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public bool IsFree = true;

    public int ind = 0;

    private void OnTriggerEnter(Collider other)
    {      
        Item otherItem = null;

        if (other.TryGetComponent<Item>(out otherItem))
        {
            if (!IsFree && otherItem.IsFree)
            {
                Debug.Log("Add other!");
                ItemsList.instance.Add(otherItem);          
            }
        }
        else if (!other.CompareTag("Player") && !other.TryGetComponent<Item>(out otherItem) 
            && (other.GetComponentInParent<Item>() == null)) {

            Debug.Log("Collision: " + ind + "  " + other.name);

            ItemsList.instance.OnCollision(this);
        }
      
    }

    public void Fall()
    {
        this.GetComponent<Collider>().enabled = false;
        transform.parent = null;

        this.gameObject.AddComponent<Rigidbody>();
    }

}
