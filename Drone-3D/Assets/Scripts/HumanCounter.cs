using UnityEngine;
using System.Collections;

public class HumanCounter : Counter
{

    [SerializeField] private float _countingDelay = 0.5f;       // 0.3f 

    public static HumanCounter Instance { get; private set; } = null;

    private int addend = default;

    public override void Add(int count) {

        addend = count;
        StartCoroutine(AddToView());        
    }

    private IEnumerator AddToView() {

        while (addend > 0)
        {
            _counterView.text = (++_currentCount).ToString();
            addend--;

            yield return new WaitForSeconds(_countingDelay);
        }

        StartCoroutine(GameManager.StartFinalMenu(true, null));
        
    } 

    public void Awake()
    {
        Instance = this;
        _counterView.text = _currentCount.ToString();
    }

    private void Start()
    {      
        GameManager.OnSessionStart += delegate ()
        {
            _counterView.gameObject.SetActive(false);
        };

        GameManager.OnSessionResume += delegate ()
        {
            _counterView.gameObject.SetActive(false);
        };

        GameManager.OnSessionComplete += delegate ()
        {
            _counterView.gameObject.SetActive(true);
        };

        LevelsManager.OnReloadLevel += delegate ()
        {
            Instance = null;
        };

    }

}

