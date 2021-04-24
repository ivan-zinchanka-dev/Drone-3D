using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public bool IsFree = true;

    //public int ind = 0;


    private void OnTriggerEnter(Collider other)
    {      
        Item otherItem = null;

        if (other.TryGetComponent<Item>(out otherItem))
        {
            if (!IsFree && otherItem.IsFree)
            {               
                ItemsList.instance.Add(otherItem);          
            }
        }
        else if (!other.CompareTag("Player") && other.GetComponentInParent<Item>() == null
            && other.GetComponent<Blower>() == null) {

            //Debug.Log("Collision: " + ind + "  " + other.name);

            ItemsList.instance.OnItemCollision(this);
        }
      
    }

    public void Fall()
    {
        this.GetComponent<Collider>().enabled = false;
        transform.parent = null;

        this.gameObject.AddComponent<Rigidbody>();
    }

}
