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

    private StatusButton InstanceButton;


    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }

    void Start()
    {
        save = Save.instance;
        StartGame();
        InstanceButton = GameObject.FindGameObjectsWithTag("ButtonManager")[0].gameObject.GetComponent<ButtonList>().ListButton[0].gameObject.GetComponent<StatusButton>();
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
