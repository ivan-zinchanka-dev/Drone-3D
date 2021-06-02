using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static event Action OnSessionStart = null;
    public static event Action OnSessionPause = null;
    public static event Action OnSessionResume = null;
    public static event Action OnSessionComplete = null;

    public static event Action OnReadPlayerData = null;
    public static Action OnWritePlayerData = null;
    
    public static Func<bool, float?, IEnumerator> StartFinalMenu = null;

    [SerializeField] private float _waitingPositiveResultsMenu = 0.5f;
    [SerializeField] private float _waitingNegativeResultsMenu = 1.5f;

    [SerializeField] private DroneController _droneController = null;

    [SerializeField] private Counter _thanksCounter = null;
    [SerializeField] private RectTransform _levelsMap = null;
    [SerializeField] private RectTransform _shopMenu = null;

    [SerializeField] private GameObject[] _buttons = null;

    [SerializeField] private Button _startButton = null;   
    [SerializeField] private RectTransform _pauseButton = null;
    [SerializeField] private RectTransform _nextLevelButton = null;
    [SerializeField] private RectTransform _restartLevelButton = null;

    [SerializeField] private TMPro.TextMeshProUGUI _gain = null;
    [SerializeField] private bool _switchStartButtonToResume = true;
    
    public static Counter ThanksCounter { get; private set; }

    public void LoadPlayerData() {

        ThanksCounter = _thanksCounter;
        ThanksCounter.CurrentCount = PlayerPrefs.GetInt("thanks", 0);    
    }

    private void UpdateThanksCount() {

        if (ThanksCounter != null) PlayerPrefs.SetInt("thanks", ThanksCounter.CurrentCount);
        PlayerPrefs.Save();
    }

    public void BeginGame()
    {
        if (OnSessionStart != null) {

            OnSessionStart();
        }

        Debug.Log("BEGIN");
    }

    public static void EndGame(bool isSuccessfull) {
        
        OnSessionComplete?.Invoke();
    }

    public void Pause()
    {
        if (OnSessionPause != null)
        {
            OnSessionPause();
        }       
    }

    public void Resume()
    {
        if (OnSessionResume != null)
        {
            OnSessionResume();
        }

        Debug.Log("RESUME");
    }

    public void OpenLevelsMap() {

        _levelsMap.gameObject.SetActive(true);
        _pauseButton.gameObject.SetActive(false);

        foreach (var button in _buttons)
        {
            button.SetActive(false);
        }
    }
    
    public void CloseLevelsMap()
    {
        _levelsMap.gameObject.SetActive(false);
        _pauseButton.gameObject.SetActive(true);

        for (int i = 0; i < _buttons.Length; i++)
        {
            _buttons[i].SetActive(true);
        }
    }

    public void OpenShop() {

        _shopMenu.gameObject.SetActive(true);
        _pauseButton.gameObject.SetActive(false);
        ThanksCounter.SetVisibility(true);

        foreach (var button in _buttons)
        {
            button.SetActive(false);
        }
    }

    public void CloseShop() {

        _shopMenu.gameObject.SetActive(false);
        _pauseButton.gameObject.SetActive(true);
        ThanksCounter.SetVisibility(false);

        foreach (var button in _buttons)
        {
            button.SetActive(true);
        }
    }

    public void Quit() {

        Application.Quit(0);
    }


    private void Awake()
    {      
        OnSessionStart += delegate ()
        {
            if (_droneController != null) _droneController.IsLocked = false;
            _pauseButton?.gameObject.SetActive(true);

            foreach (var button in _buttons)
            {
                button.SetActive(false);
            }

            if (_switchStartButtonToResume &&_startButton != null ) {

                _startButton.onClick = new Button.ButtonClickedEvent();
                _startButton.onClick.AddListener(Resume);
            }            
        };

        OnSessionPause += delegate ()
        {
            if (_droneController != null) _droneController.IsLocked = true;

            for (int i = 0; i < _buttons.Length; i++) {

                _buttons[i].SetActive(true);
            }
        };

        OnSessionResume += delegate ()
        {
            if (_droneController != null) _droneController.IsLocked = false;

            foreach (var button in _buttons)
            {
                button.SetActive(false);
            }
        };

        OnSessionComplete += delegate ()
        {
            if(_droneController != null) _droneController.IsLocked = true;

            if (_pauseButton != null) _pauseButton.gameObject.SetActive(false);

        };

        OnWritePlayerData += UpdateThanksCount;
        OnReadPlayerData += LoadPlayerData;
    }


    private IEnumerator StartFinalMenuForSession(bool sessionIsSuccessfull, float? waitingTime) {
        
        if (sessionIsSuccessfull)
        {
            if (waitingTime == null) yield return new WaitForSeconds(_waitingPositiveResultsMenu);
            else yield return new WaitForSeconds((float)waitingTime);
            _nextLevelButton.gameObject.SetActive(true);
            _gain.SetText("+ " + HumanCounter.Instance.CurrentCount);
        }
        else
        {
            if (waitingTime == null) yield return new WaitForSeconds(_waitingNegativeResultsMenu);
            else yield return new WaitForSeconds((float)waitingTime);
            _restartLevelButton.gameObject.SetActive(true);
        }
    }

    public void Start()
    {               
        OnReadPlayerData();
        StartFinalMenu = StartFinalMenuForSession;

        LevelsManager.OnReloadLevel += delegate ()
        {
            OnSessionStart = null;
            OnSessionPause = null;
            OnSessionResume = null;
            OnSessionComplete = null;
            OnWritePlayerData = null;
        };

    }


}
