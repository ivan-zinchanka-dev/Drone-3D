using System;
using System.Collections.Generic;
using UnityEngine;

public class HumanChain : MonoBehaviour
{
    [SerializeField] private Vector3 _startPosition = new Vector3(0.0f, -0.5f, 0.0f);
    [SerializeField] private Vector3 _offset = new Vector3(0.0f, -2.5f, 0.0f);

    private Vector3 _currentPosition = default;
    private List<Human> _humans = null;
    public static HumanChain instance = null;

    void Start()
    {       
        _humans = new List<Human>();
        _currentPosition = _startPosition;
        instance = this;

        Human firstHuman = GetComponentInChildren<Human>();
        Add(firstHuman);
 
        DroneBehaviour.DroneFailure += OnDroneCollision;

        LevelsManager.OnReloadLevel += delegate ()
        {
            instance = null;
        };

    }

    public void Add(Human newHuman) {
      
        newHuman.IsFree = false;
        newHuman.transform.parent = this.transform;
        newHuman.transform.localPosition = _currentPosition; 
        _currentPosition += _offset;

        _humans.Add(newHuman);
    }


    public void OnDroneCollision() {

        for (int i = 0; i < _humans.Count; i++)
        {
            _humans[i].Fall();
            _currentPosition -= _offset;
        }       

        _humans.Clear();
    }

    public void OnChainUngrouping() {

        for (int i = 0; i < _humans.Count; i++)
        {
            _humans[i].Fall();
            _currentPosition -= _offset;

            _humans[i].gameObject.GetComponent<Collider>().enabled = false;

            BoxCollider footrest = _humans[i].gameObject.AddComponent<BoxCollider>();
            footrest.center = new Vector3(0.0f, -0.7f, 0.0f);
        }

        HumanCounter.Instance.Add(_humans.Count);
        GameManager.ThanksCounter.Add(_humans.Count);
        GameManager.OnWritePlayerData?.Invoke();

        _humans.Clear();

    }


    public void OnHumanCollision(Human sufferedItem)
    {
        int humanIndex = _humans.IndexOf(sufferedItem);

        if (humanIndex == -1) throw new OutOfMemoryException("This human is not is the chain");                   

        if (humanIndex == 0) humanIndex++;

        for (int i = humanIndex; i <_humans.Count; i++) {

            _humans[i].Fall();
            _currentPosition -= _offset;
        }

        _humans.RemoveRange(humanIndex, _humans.Count - humanIndex);
    }

    public float GetLength() {

        return Mathf.Abs(_currentPosition.y) + Mathf.Abs(_offset.y);    
    }


}
