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
    public float soundVolume;
    public float musicVolume;

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
         PlayerPrefs.SetFloat("SoundVolume", soundVolume);
         PlayerPrefs.SetFloat("MusicVolume", musicVolume);
         PlayerPrefs.Save();
    }

    public void LoadGame() {
        if (PlayerPrefs.HasKey("LevelsPassed") || PlayerPrefs.HasKey("PlayerBackgrounds") || PlayerPrefs.HasKey("CurrentBackground") || PlayerPrefs.HasKey("SoundVolume") || PlayerPrefs.HasKey("MusicVolume")) {
            levelsPassed = PlayerPrefs.GetInt("LevelsPassed");
            playerBackgrounds = PlayerPrefs.GetInt("PlayerBackgrounds");
            currentBackground = PlayerPrefs.GetInt("CurrentBackground");
            soundVolume = PlayerPrefs.GetFloat("SoundVolume");
            musicVolume = PlayerPrefs.GetFloat("MusicVolume");
        }
    }

    private void OnApplicationQuit() {
        SaveGame();
    }

    private void OnApplicationPause(bool pauseStatus) {
        SaveGame();
    }
}
