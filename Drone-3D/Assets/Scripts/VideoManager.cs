using UnityEngine;

public class VideoManager : MonoBehaviour
{
    private void Start() {
       
        Application.LoadLevel(PlayerPrefs.GetInt(LevelsManager.currentLevelIndexKey, LevelsManager.firstLevelIndex));    
    }


}
