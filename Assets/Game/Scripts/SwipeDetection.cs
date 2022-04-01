using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeDetection : MonoBehaviour
{
    public static SwipeDetection instance;
    private Save save;
    public static event OnSwipeInput SwipeEvent;
    public delegate void OnSwipeInput(Vector2 direction);

    private Vector2 tapPosition;
    private Vector2 swipeDelta;

    private float deadZone = 80;

    private bool isSwiping;
    private bool isMobile;

    [SerializeField]
    public List<int> SaveBoardForBack;
    public List<int> SaveNowBoard;
    private int SaveScore;
    private StatusButton InstanceButton;

    [SerializeField]
    private GameObject GameBoard;
    
    private void Awake() {
        if(instance == null)
            instance = this;
    }
    void Start()
    {
        save = Save.instance;
        isMobile = Application.isMobilePlatform;
        InstanceButton = GameObject.FindGameObjectsWithTag("ButtonManager")[0].gameObject.GetComponent<ButtonList>().ListButton[0].gameObject.GetComponent<StatusButton>();
    }

    void Update()
    {
        if(!isMobile)
        {
            if(Input.GetMouseButtonDown(0))
            {
                isSwiping = true;
                tapPosition = Input.mousePosition;
            }
            else if (Input.GetMouseButtonUp(0))
                ResetSwipe(); 
        }
        else 
        {
            if(Input.touchCount > 0)
            {
                if(Input.GetTouch(0).phase == TouchPhase.Began)
                {

                    isSwiping = true;
                    tapPosition = Input.GetTouch(0).position;
                } 
                else if (Input.GetTouch(0).phase == TouchPhase.Canceled ||
                        Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    ResetSwipe();    
                }
            }
        }
        if(isSwiping)
            CheckSwipe();
    }

    private void CheckSwipe()
    {
        swipeDelta = Vector2.zero;

        if(isSwiping)
        {
            if(!isMobile && Input.GetMouseButton(0))
                swipeDelta = (Vector2)Input.mousePosition - tapPosition;
            else if (Input.touchCount > 0)
                swipeDelta = Input.GetTouch(0).position - tapPosition;
        }

        if(swipeDelta.magnitude > deadZone)
        {            
            SaveBoard();
            InstanceButton.isActive = true;
            InstanceButton.UpdateStatus();
            if(Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y))
                SwipeEvent(swipeDelta.x > 0 ? Vector2.right : Vector2.left);
            else
                SwipeEvent(swipeDelta.y > 0 ? Vector2.up : Vector2.down);

            ResetSwipe();
        }

        
    }

    private void ResetSwipe()
    {
        isSwiping = false;

        tapPosition = Vector2.zero;
        swipeDelta = Vector2.zero;
    }

    public List<int> SaveBoard(bool back = false)
    {
        SaveScore = GameController.instance.GetPoints();
        if(back == true)
        {
            if(SaveNowBoard.Count != 0)
                SaveNowBoard.Clear();
            foreach (Transform child in GameBoard.transform)
            {
                SaveNowBoard.Add(child.GetComponent<Cell>().Value);
            }
            return SaveNowBoard;
        }
        else 
        {
            if(SaveBoardForBack.Count != 0)
                SaveBoardForBack.Clear();
            foreach (Transform child in GameBoard.transform)
            {
                SaveBoardForBack.Add(child.GetComponent<Cell>().Value);
            }

            return SaveBoardForBack;
        }
    }

    public void BackOneMove()
    {
        InstanceButton.isActive = false;
        InstanceButton.UpdateStatus();
        int numerator = 0;
        GameObject GameBoard = GameObject.FindGameObjectsWithTag("GameBoard")[0].gameObject;
        foreach (int value in SaveBoardForBack)
        {
            GameBoard.transform.GetChild(numerator).GetComponent<Cell>().SetValue(0, 0, value, true, false);
            numerator++;
        }
        GameController.instance.AddPoints(SaveScore-GameController.instance.GetPoints());
        SaveBoardForBack.Clear();
    }

    public void SetNowBoard() 
    {
        int numerator = 0;
        GameObject GameBoard = GameObject.FindGameObjectsWithTag("GameBoard")[0].gameObject;
        foreach (int value in SaveNowBoard)
        {
            GameBoard.transform.GetChild(numerator).GetComponent<Cell>().SetValue(0, 0, value, true, false);
            numerator++;
        }
    }
}
