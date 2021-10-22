using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DroneMeshModel : MonoBehaviour
{   
    [SerializeField] private DroneSkin[] _allSkins = null;
    
    private DroneSkin _currentSkinPref = null;
    private DroneSkin _currentSkinClone = null;

    public const string AllSkinsSerialKey = "allDroneSkins";
    public const string CurrentSkinSerialKey = "currentDroneSkin";

    private Color _lockedSkinColor = new Color(1.0f, 1.0f, 1.0f, 0.5f);
    private Color _availableSkinColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);

    public DroneSkin CurrentSkin { 
        
        get { return _currentSkinPref; } 
        
        private set {

            if(_currentSkinClone != null) Destroy(_currentSkinClone.gameObject);
            _currentSkinPref = value;
            _currentSkinClone = Instantiate(_currentSkinPref, this.transform, false);

            PlayerPrefs.SetInt(CurrentSkinSerialKey, (int)GetCurrentSkinId());
            PlayerPrefs.Save();
        } 
    }


    private int BoolToInt(bool value) {

        return (value) ? 1 : 0; 
    }

    public void WriteSkinsToDrive()
    {      
        string buf = string.Empty;

        for (int i = 0; i < _allSkins.Length; i++)
        {
            buf += BoolToInt(_allSkins[i].IsAvailable);
        }

        PlayerPrefs.SetString(AllSkinsSerialKey, buf);
        PlayerPrefs.Save();
    }

    private void ReadSkinsFromDrive() {

        string buffer = PlayerPrefs.GetString(AllSkinsSerialKey, string.Empty);

        if (buffer.Equals(string.Empty)) {

            throw new KeyNotFoundException("Key " + AllSkinsSerialKey + " not found.");        
        }
                       
        for (int i = 0; i < _allSkins.Length; i++) {

            if (buffer[i] == '0')
            {
                _allSkins[i].IsAvailable = false;
            }
            else {

                _allSkins[i].IsAvailable = true;
            }        
        }       
    }

    public List<DroneSkin> GetLockedSkins() {

        List<DroneSkin> result = new List<DroneSkin>(6);

        foreach (var skin in _allSkins)
        {
            if (!skin.IsAvailable) result.Add(skin);      
        }

        return result;
    }


    public void UpdateToggles(Toggle[] toggles) {

        Toggle it;

        for (int i = 0; i <_allSkins.Length; i++) {

            it = toggles[i];

            if (_allSkins[i].IsAvailable)
            {
                if (it.interactable == false) it.GetComponentInChildren<Image>().color = _availableSkinColor;
                
                it.interactable = true;
            }
            else {

                if (it.interactable == true) it.GetComponentInChildren<Image>().color = _lockedSkinColor;
                
                it.interactable = false;             
            }
        }    
    }

    public int? GetCurrentSkinId() {

        for (int i = 0; i < _allSkins.Length; i++) {

            if (_allSkins[i].Equals(CurrentSkin)) {

                return i;            
            }
        }

        return null;
    }


    public void ChooseSkinForId(int skinId) {

        if (_allSkins[skinId].IsAvailable) {

            CurrentSkin = _allSkins[skinId];
        }   
    }

    public bool SetSkinForName(string name) {

        foreach (var skin in _allSkins) {

            if (skin.Name.Equals(name)) {

                CurrentSkin = skin;
                return true;
            }      
        }

        return false;    
    }

    public void Awake()
    {
        if (_allSkins == null || _allSkins.Length == 0) {

            throw new ArgumentNullException("allSkins", "array of skins not set");
        }
    }

    public void Start()
    {
        try
        {
            ReadSkinsFromDrive();
        }
        catch (KeyNotFoundException ex) {

            Debug.Log(ex.Message);
            WriteSkinsToDrive();                  
        }

        ChooseSkinForId(PlayerPrefs.GetInt(CurrentSkinSerialKey, 0));     
    }

}
