using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private Save save;
    private SwipeDetection swipeDetection;
    public static GameController instance;

    public static int Points { get; private set; }
    public static bool GameStarted { get; private set; }

    [SerializeField]
    private GameObject gameResult;
    [SerializeField]
    private TextMeshProUGUI pointsText;

    [Space(5)]

    [SerializeField]
    private GameObject Menu;

    private StatusButton InstanceButton;


    private void Awake()
    {
        if(instance == null)
            instance = this;

        // LevelLoader.Level = 1;
        // LevelLoader.Difficulty = 1;
    }

    void Start()
    {
        save = Save.instance;
        swipeDetection = SwipeDetection.instance;
        StartGame();
        InstanceButton = GameObject.FindGameObjectsWithTag("ButtonManager")[0].gameObject.GetComponent<ButtonList>().ListButton[0].gameObject.GetComponent<StatusButton>();
    }

    public void NextLevel()
    {
        if(LevelLoader.Difficulty == 3)
        {
            LevelLoader.Level++;
            LevelLoader.Difficulty = 1;
        }
        else 
        {
            LevelLoader.Difficulty++;
        }
        gameResult.SetActive(false);
        save.DeleteSaveLevel();
        StartGame(true);
    }

    public void StartGame(bool NextLevel = false)
    {
        if(gameResult.activeSelf == true)
            gameResult.SetActive(false);

        Board.Instance.GenerateBoard(NextLevel);
        GameStarted = true;

        if (!PlayerPrefs.HasKey("Score") && !PlayerPrefs.HasKey("SaveNowBoard") && !PlayerPrefs.HasKey("LastLevelPlay")) {
            SetPoints(0);
        } else {
            if(NextLevel == false && PlayerPrefs.GetInt("LastLevelPlay") == LevelLoader.Level)
            {
                save.LoadCurrentGame();
                swipeDetection.SaveNowBoard = save.saveNowBoard;
                swipeDetection.SaveBoardForBack = save.saveBoardForBack;
                SetPoints(save.score);
                swipeDetection.SetNowBoard();
            }
            else 
            {
                SetPoints(0);
            }
            
        }
    }

    private void CheckNextLevel(Button button)
    {
        if(LevelLoader.Level == Board.Instance.levelSettings.Length && LevelLoader.Difficulty == 3)
        {
            button.interactable = false;
        }
    }

    private void DisableButtonOnLose(GameObject button, bool On)
    {
        Color32 Color;
        button.GetComponent<Button>().interactable = On;
        if(On)
            Color = new Color32(255, 255, 255, 255);
        else
            Color = new Color32(100, 100, 100, 255);
        button.GetComponent<Image>().color = Color;
    }

    public void Win()
    {
        GameStarted = false;
        gameResult.SetActive(true);
        CheckNextLevel(gameResult.transform.GetChild(4).GetComponent<Button>());
        DisableButtonOnLose(gameResult.transform.GetChild(4).gameObject, true);
        gameResult.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "You Win!";
        if(LevelLoader.Difficulty == 1) {
            save.levelsPassed++;
        }
        save.SaveStars();
    }

    public void Lose()
    {
        InstanceButton.isActive = false;
        InstanceButton.UpdateStatus();
        GameStarted = false;
        gameResult.SetActive(true);
        DisableButtonOnLose(gameResult.transform.GetChild(4).gameObject, false);
        gameResult.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "You Lose!";
    }

    public int GetPoints()
    {
        return Points;
    }

    public void AddPoints(int points)
    {
        SetPoints(Points += points);
    }

    private void SetPoints (int points)
    {
        Points = points;
        pointsText.text = Points.ToString();
    }

    public void OpenGameMenu()
    {
        if(Menu.activeSelf == true)
            Menu.SetActive(false);
        else 
            Menu.SetActive(true);
        
    }

    public void BackMenu(bool Flag) {
        save.SaveGameSettings();
        if(Flag)
            save.SaveCurrentGame();
        else
            save.DeleteSaveLevel();
        SceneManager.LoadScene(0);
    }
}
