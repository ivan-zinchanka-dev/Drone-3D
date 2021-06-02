using UnityEngine;
using TMPro;

public class Counter : MonoBehaviour
{
    [SerializeField] protected int _currentCount = 0;
    [SerializeField] protected TMP_Text _counterView = null;

    public int CurrentCount { 
        
        get { return _currentCount; } 
        
        set {     
            _currentCount = value;
            _counterView.SetText(_currentCount.ToString());
        } 
    }
    
    //public int GetCount()
    //{
    //    return _currentCount;
    //}

    //public void SetCount(int newCount) {

    //    _currentCount = newCount;
    //    _counterView.SetText(_currentCount.ToString());
    //}

    public virtual void Add(int count) {

        _currentCount += count;
        _counterView.SetText(_currentCount.ToString());
    }

    public void SetVisibility(bool isVisible) {

        _counterView.gameObject.SetActive(isVisible);    
    }


}
