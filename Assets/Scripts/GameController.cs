using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private Save save;
    public static GameController Instance;

    public static int Points { get; private set; }
    public static bool GameStarted { get; private set; }

    [SerializeField]
    private TextMeshProUGUI gameResult;
    [SerializeField]
    private TextMeshProUGUI pointsText;

    [Space(5)]

    [SerializeField]
    private GameObject Menu;


    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }

    void Start()
    {
        save = Save.instance;
        StartGame();
    }

    public void StartGame()
    {
        gameResult.text = "";

        SetPoints(0);
        GameStarted = true;

        Board.Instance.GenerateBoard();
    }

    public void Win()
    {
        GameStarted = false;
        gameResult.text = "You Win!";
    }

    public void Lose()
    {
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
        if(Menu.active == true)
            Menu.SetActive(false);
        else 
            Menu.SetActive(true);
        
    }

    public void BackMenu() {
        save.SaveGame();
        SceneManager.LoadScene(0);
    }
}
