using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Save : MonoBehaviour
{
    public static Save instance;
    public int levelsPassed;

    //TODO при добавлении нового фона сделать проверку чтобы ошибка не дропалась
    public int[] playerBackgrounds;
    private string playerBackgroundsSave;
    public int currentBackground;
    public float soundVolume;
    public float musicVolume;

    public Sprite[] backgrounds;
     
    private void Awake() {
        instance = this;
        backgrounds = Resources.LoadAll<Sprite>("backgrounds") as Sprite[];
        playerBackgrounds = new int[backgrounds.Length];
        LoadGame();
    }

    public void SaveGame() {
         PlayerPrefs.SetInt("LevelsPassed", levelsPassed);
         PlayerPrefs.SetInt("CurrentBackground", currentBackground);
         PlayerPrefs.SetFloat("SoundVolume", soundVolume);
         PlayerPrefs.SetFloat("MusicVolume", musicVolume);
         foreach (var backgrounds in playerBackgrounds) playerBackgroundsSave += backgrounds + ",";
         playerBackgroundsSave = playerBackgroundsSave.Remove(playerBackgroundsSave.Length-1);
         PlayerPrefs.SetString("PlayerBackgrounds", playerBackgroundsSave);
         PlayerPrefs.Save();
    }

    public void LoadGame() {
        if (PlayerPrefs.HasKey("LevelsPassed") || PlayerPrefs.HasKey("PlayerBackgrounds") || PlayerPrefs.HasKey("CurrentBackground") || PlayerPrefs.HasKey("SoundVolume") || PlayerPrefs.HasKey("MusicVolume")) {
            levelsPassed = PlayerPrefs.GetInt("LevelsPassed");
            currentBackground = PlayerPrefs.GetInt("CurrentBackground");
            soundVolume = PlayerPrefs.GetFloat("SoundVolume");
            musicVolume = PlayerPrefs.GetFloat("MusicVolume");
            string[] loadedBackgrounds = PlayerPrefs.GetString("PlayerBackgrounds").Split(",".ToCharArray());
            for (int i = 0; i < backgrounds.Length; i++)
            {
                playerBackgrounds[i] = int.Parse(loadedBackgrounds[i]);
            }  
        }
    }

    private void OnApplicationQuit() {
        SaveGame();
    }
}
