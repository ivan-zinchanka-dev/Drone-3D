using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class Roulette : MonoBehaviour
{
    [SerializeField] private int rate = 100;
    public int Rate { get { return rate; } set { rate = value; } }

    [SerializeField] private Counter _thanksCounter = null;
    [SerializeField] private DroneMeshModel _droneMeshModel = null;
    [SerializeField] private Toggle[] _toggles = null;


    public static Roulette Instance { get; private set; }


    private void Start()
    {
        _droneMeshModel.UpdateToggles(_toggles);
        int currentDroneSkinId = PlayerPrefs.GetInt(DroneMeshModel.CurrentSkinSerialKey, 0);
        _toggles[currentDroneSkinId].isOn = true;
    }


    public void UnlockRandom() {

        if (Rate <= _thanksCounter.CurrentCount) {

            List<DroneSkin> lockedSkins = _droneMeshModel.GetLockedSkins();

            int chosen = Random.Range(0, lockedSkins.Count);
            int i = 0;

            foreach (var it in lockedSkins) {

                if (i == chosen) {

                    it.IsAvailable = true;               
                }

                i++;            
            }

            _droneMeshModel.WriteSkinsToDrive();

            _droneMeshModel.UpdateToggles(_toggles);

            _thanksCounter.Add(-1 * Rate);
            GameManager.OnWritePlayerData?.Invoke();
        }
    
    }

    private void Awake()
    {
        Instance = this;
    }




}

