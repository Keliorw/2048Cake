using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private Save save;
    private SwipeDetection swipeDetection;
    public static GameController instance;

    public static int Points { get; private set; }
    public static bool GameStarted { get; private set; }

    [SerializeField]
    private TextMeshProUGUI gameResult;
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

        LevelLoader.Level = 2;
        LevelLoader.Difficulty = 2;
    }

    void Start()
    {
        save = Save.instance;
        swipeDetection = SwipeDetection.instance;
        StartGame(LevelLoader.Level, LevelLoader.Difficulty);
        InstanceButton = GameObject.FindGameObjectsWithTag("ButtonManager")[0].gameObject.GetComponent<ButtonList>().ListButton[0].gameObject.GetComponent<StatusButton>();
    }

    public void StartGame(int Level, int Difficulty)
    {
        gameResult.text = "";
        
        Board.Instance.GenerateBoard(Level, Difficulty);
        GameStarted = true;
        if (!PlayerPrefs.HasKey("Score") && !PlayerPrefs.HasKey("SaveNowBoard")) {
            SetPoints(0);
        } else {
            save.LoadCurrentGame();
            swipeDetection.SaveNowBoard = save.saveNowBoard;
            swipeDetection.SaveBoardForBack = save.saveBoardForBack;
            SetPoints(save.score);
            swipeDetection.SetNowBoard();
        }
    }

    public void Win()
    {
        GameStarted = false;
        gameResult.text = "You Win!";
    }

    public void Lose()
    {
        InstanceButton.isActive = false;
        InstanceButton.UpdateStatus();
        GameStarted = false;
        gameResult.text = "You Lose!";
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

    public void BackMenu() {
        save.SaveGameSettings();
        save.SaveCurrentGame();
        SceneManager.LoadScene(0);
    }
}
