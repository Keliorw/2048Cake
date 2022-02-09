using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Save : MonoBehaviour
{
    public static Save instance;
    public int levelsPassed;
    public int playerBackgrounds;
    public int currentBackground;

    public Sprite[] backgrounds;
     
    private void Awake() {
        instance = this;
        backgrounds = Resources.LoadAll<Sprite>("backgrounds") as Sprite[];
        LoadGame();
    }

    public void SaveGame() {
         PlayerPrefs.SetInt("LevelsPassed", levelsPassed);
         PlayerPrefs.SetInt("PlayerBackgrounds", playerBackgrounds);
         PlayerPrefs.SetInt("CurrentBackground", currentBackground);
         PlayerPrefs.Save();
    }

    public void LoadGame() {
        if (PlayerPrefs.HasKey("LevelsPassed") || PlayerPrefs.HasKey("PlayerBackgrounds") || PlayerPrefs.HasKey("CurrentBackground")) {
            levelsPassed = PlayerPrefs.GetInt("LevelsPassed");
            playerBackgrounds = PlayerPrefs.GetInt("PlayerBackgrounds");
            currentBackground = PlayerPrefs.GetInt("CurrentBackground");
        }
    }

    private void OnApplicationQuit() {
        SaveGame();
    }

    private void OnApplicationPause(bool pauseStatus) {
        SaveGame();
    }
}
