using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Save : MonoBehaviour
{
    public static Save instance;
    public Sprite currentBackgroundSprite;

    public int levelsPassed;
    public int playerBackgrounds;
     
    private void Awake() {
        instance = this;
    }

    public void SaveGame() {
         PlayerPrefs.SetInt("levelsPassed", levelsPassed);
         PlayerPrefs.SetInt("playerBackgrounds", playerBackgrounds);
         PlayerPrefs.Save();
    }
}
