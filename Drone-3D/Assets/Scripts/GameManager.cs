using System;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static event Action OnSessionStart = null;
    public static event Action OnSessionPause = null;
    public static event Action OnSessionResume = null;
    public static event Action OnSessionComplete = null;


    [SerializeField] private DroneController _droneController = null;

    [SerializeField] private GameObject[] buttons = null;
    [SerializeField] private RectTransform map = null;
    [SerializeField] private RectTransform pauseButton = null;

    public void BeginGame()
    {
        if (OnSessionStart != null) {

            OnSessionStart();
        }        
    }

    public static void EndGame() {

        if (OnSessionComplete != null)
        {
            OnSessionComplete();
        }
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
    }

    public void OpenLevelsMap() {

        map.gameObject.SetActive(true);
        pauseButton.gameObject.SetActive(false);

        foreach (var button in buttons)
        {
            button.SetActive(false);
        }
    }

    public void CloseLevelsMap()
    {
        map.gameObject.SetActive(false);
        pauseButton.gameObject.SetActive(true);

        for (int i = 1; i < buttons.Length; i++)
        {
            buttons[i].SetActive(true);
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

            foreach (var button in buttons)
            {
                button.SetActive(false);
            }
        };

        OnSessionPause += delegate ()
        {
            if (_droneController != null) _droneController.IsLocked = true;

            for (int i = 1; i < buttons.Length; i++) {

                buttons[i].SetActive(true);
            }
        };

        OnSessionResume += delegate ()
        {
            if (_droneController != null) _droneController.IsLocked = false;

            foreach (var button in buttons)
            {
                button.SetActive(false);
            }
        };

        OnSessionComplete += delegate ()
        {
            if(_droneController != null) _droneController.IsLocked = true;

            if (pauseButton != null) pauseButton.gameObject.SetActive(false);

        };

    }


}
