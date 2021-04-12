using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsList : MonoBehaviour
{
    [SerializeField] private Vector3 _startPosition = new Vector3(0.0f, -0.25f, 0.0f);
    [SerializeField] private Vector3 _offset = new Vector3(0.0f, -0.5f, 0.0f);

    private Vector3 _currentPosition = default;
    private List<Item> _items = null;
    public static ItemsList instance = null;

    void Start()
    {       
        _items = new List<Item>();
        _currentPosition = _startPosition;
        instance = this;

        Item firstItem = GetComponentInChildren<Item>();
        Add(firstItem);
 
        DroneBehaviour.DroneFailure += OnDroneCollision;      
    }

    public void Add(Item newItem) {
      
        newItem.IsFree = false;
        newItem.transform.parent = this.transform;
        newItem.transform.localPosition = _currentPosition; 
        _currentPosition += _offset;

        _items.Add(newItem);

        newItem.ind = _items.IndexOf(newItem);

    }


    public void OnDroneCollision() {

        for (int i = 0; i < _items.Count; i++)
        {
            _items[i].Fall();
            _currentPosition -= _offset;
        }

        _items.Clear();
    }

    public void OnItemCollision(Item sufferedItem)
    {
        int itemIndex = _items.IndexOf(sufferedItem);
        Debug.Log("Index: " + itemIndex);

        if (itemIndex == 0) itemIndex++;

        for (int i = itemIndex; i <_items.Count; i++) {

            _items[i].Fall();
            _currentPosition -= _offset;
        }

        _items.RemoveRange(itemIndex, _items.Count - itemIndex);
    }


    void Update()
    {
        
    }
}
