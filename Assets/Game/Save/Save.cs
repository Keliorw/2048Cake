using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Save : MonoBehaviour
{
    public static Save instance;
    private SwipeDetection swipeDetection;
    private GameController gameController;
    public int levelsPassed;
    public int[] playerBackgrounds;
    private string playerBackgroundsSave;
    public List<int> saveNowBoard;
    private string saveNowBoardSave;
    public List<int> saveBoardForBack;
    private string saveBoardForBackSave;
    public int score;
    public int currentBackground;
    public float soundVolume;
    public float musicVolume;

    public Sprite[] backgrounds;
     
    private void Start() {
        swipeDetection = SwipeDetection.instance;
        gameController = GameController.instance;
    }
    private void Awake() {
        instance = this;
        backgrounds = Resources.LoadAll<Sprite>("backgrounds") as Sprite[];
        playerBackgrounds = new int[backgrounds.Length];
        LoadGameSetggins();
    }

    public void SaveGameSettings() {
         PlayerPrefs.SetInt("LevelsPassed", levelsPassed);
         PlayerPrefs.SetInt("CurrentBackground", currentBackground);
         PlayerPrefs.SetFloat("SoundVolume", soundVolume);
         PlayerPrefs.SetFloat("MusicVolume", musicVolume);
         foreach (var backgrounds in playerBackgrounds) playerBackgroundsSave += backgrounds + ",";
         playerBackgroundsSave = playerBackgroundsSave.Remove(playerBackgroundsSave.Length-1);
         PlayerPrefs.SetString("PlayerBackgrounds", playerBackgroundsSave);
         PlayerPrefs.Save();
    }

    public void LoadGameSetggins() {
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

    public void SaveCurrentGame () {
        PlayerPrefs.SetInt("LastLevelPlay", LevelLoader.Level);

        score = gameController.GetPoints();
        PlayerPrefs.SetInt("Score", score);

        saveNowBoard = swipeDetection.SaveBoard(true);
        saveBoardForBack = swipeDetection.SaveBoard(false);

        foreach (var cellsNow in saveNowBoard) saveNowBoardSave += cellsNow + ",";
        saveNowBoardSave = saveNowBoardSave.Remove(saveNowBoardSave.Length-1);
        PlayerPrefs.SetString("SaveNowBoard", saveNowBoardSave);
         
        foreach (var cellsBack in saveBoardForBack) saveBoardForBackSave += cellsBack + ",";
        saveBoardForBackSave = saveBoardForBackSave.Remove(saveBoardForBackSave.Length-1);
        PlayerPrefs.SetString("SaveBoardForBack", saveBoardForBackSave);

        PlayerPrefs.Save();
    }

    public void LoadCurrentGame() {
        if (PlayerPrefs.HasKey("Score") && PlayerPrefs.HasKey("SaveNowBoard") && PlayerPrefs.HasKey("LastLevelPlay")) 
        {
            if(PlayerPrefs.GetInt("LastLevelPlay") == LevelLoader.Level)
            {
                score = PlayerPrefs.GetInt("Score");

                string[] loadedCellsNow = PlayerPrefs.GetString("SaveNowBoard").Split(",".ToCharArray());
                for (int i = 0; i < loadedCellsNow.Length; i++)
                {
                    saveNowBoard.Add(int.Parse(loadedCellsNow[i]));
                } 
                
                string[] loadedCellsBack = PlayerPrefs.GetString("SaveBoardForBack").Split(",".ToCharArray());
                for (int i = 0; i < loadedCellsBack.Length; i++)
                {
                    saveBoardForBack.Add(int.Parse(loadedCellsBack[i]));
                }
            }
            else 
            {
                DeleteSaveLevel();
            }
        }
    }

    public void DeleteSaveLevel()
    {
        if(PlayerPrefs.HasKey("Score") && PlayerPrefs.HasKey("SaveNowBoard") && PlayerPrefs.HasKey("LastLevelPlay")) 
        {
            PlayerPrefs.DeleteKey("Score");
            PlayerPrefs.DeleteKey("SaveNowBoard");
            PlayerPrefs.DeleteKey("LastLevelPlay");
            PlayerPrefs.DeleteKey("SaveBoardForBack");
        }
    }

    private void OnApplicationQuit() {
        SaveGameSettings();
        if(SceneManager.GetActiveScene().name == "Game") {
            SaveCurrentGame();
        }
    }
}
