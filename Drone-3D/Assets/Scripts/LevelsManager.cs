using System;
using UnityEngine;

public class LevelsManager : MonoBehaviour
{
    public static event Action OnReloadLevel;

    [SerializeField] private int _currentLevel = 1;
    [SerializeField] private int _countOfLevels = 5;

    public const int firstLevelIndex = 1;
    public const string currentLevelIndexKey = "currentLevelIndex";


    public static LevelsManager Instance { get; private set; } = null;

    public int CurrentLevelIndex { get; private set; } 

    private void Start()
    {
        Instance = this;
        CurrentLevelIndex = _currentLevel;

        PlayerPrefs.SetInt(currentLevelIndexKey, CurrentLevelIndex);
        PlayerPrefs.Save();   
    }

    public int GetNextLevel() {

        if (CurrentLevelIndex == _countOfLevels)
        {
            return firstLevelIndex;
        }
        else {

            return ++CurrentLevelIndex;
        }
    }

    public void StartNextLevel() {

        CurrentLevelIndex = GetNextLevel();
        ChooseLevel(CurrentLevelIndex);    
    }

    public void RestartCurrentLevel() {

        ChooseLevel(CurrentLevelIndex);
    }


    public void ChooseLevel(int levelIndex) {

        OnReloadLevel?.Invoke();
        Application.LoadLevel(levelIndex); 
    }



}
