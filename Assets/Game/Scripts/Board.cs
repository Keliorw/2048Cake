using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Board : MonoBehaviour
{
    public static Board Instance;

    private BackgroundManager backgroundManager;

    [Header("Переменные поля")]
    public float CellSize;
    public float Spacing;
    public int BoardSize;
    public int InitCellCount;

    [Space(10)]
    [SerializeField]
    private Cell cellPref;
    [SerializeField]
    private RectTransform rt;

    private Cell[,] board;

    private bool anyCellMoved;
 
    public Image TargetRound;
    public LevelSettings[] levelSettings;
    

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }

    private void Start()
    {
        backgroundManager = BackgroundManager.Instance;
        SwipeDetection.SwipeEvent += OnInput;
    }

    private void OnInput(Vector2 direction)
    {
        if(!GameController.GameStarted)
            return;
        
        anyCellMoved = false;
        ResetCellsFlags();

        Move(direction);

        if(anyCellMoved)
        {
            GenerateRandomCell();
            CheckGameResult();
        }
    }

    private void Move(Vector2 direction)
    {
        int startXY = direction.x > 0 || direction.y < 0 ? BoardSize - 1 : 0;
        int dir = direction.x != 0 ? (int)direction.x : -(int)direction.y;

        for(int i = 0; i < BoardSize; i++)
        {
            for(int k = startXY; k >= 0 && k < BoardSize; k -= dir)
            {
                var cell = direction.x != 0 ? board[k, i] : board[i, k];

                if(cell.IsEmpty)
                    continue;
                
                var cellToMerge = FindCellToMerge(cell, direction);
                if(cellToMerge != null)
                {
                    cell.MergeWithCell(cellToMerge, backgroundManager.CellImage[LevelLoader.BackgroundImage]);
                    anyCellMoved = true;

                    continue;
                }

                var emptyCell = FindEmptyCell(cell, direction);
                if(emptyCell != null)
                {
                    cell.MoveToCell(emptyCell, backgroundManager.CellImage[LevelLoader.BackgroundImage]);
                    anyCellMoved = true;
                }
            }
        }
    }

    private Cell FindCellToMerge(Cell cell, Vector2 direction)
    {
        int startX = cell.X + (int)direction.x;
        int startY = cell.Y - (int)direction.y;

        for(int x = startX, y = startY; 
            x >= 0 && x < BoardSize && y >= 0 && y < BoardSize;
            x += (int)direction.x, y -= (int)direction.y)
        {
            if(board[x, y].IsEmpty)
                continue;

            if(board[x, y].Value == cell.Value && !board[x, y].HasMerge)
                return board[x, y];

            break;
        }

        return null;
    }

    private Cell FindEmptyCell(Cell cell, Vector2 direction)
    {
        Cell emptyCell = null;

        int startX = cell.X + (int)direction.x;
        int startY = cell.Y - (int)direction.y;

        for(int x = startX, y = startY; 
            x >= 0 && x < BoardSize && y >= 0 && y < BoardSize;
            x += (int)direction.x, y -= (int)direction.y)
        {
            if(board[x, y].IsEmpty)
                emptyCell = board[x, y];
            else 
                break;
        }

        return emptyCell;
    }

    private void CheckGameResult()
    {
        bool lose = true;

        for (int x = 0; x < BoardSize; x++)
        {
            for(int y = 0; y < BoardSize; y++)
            {
                if(board[x, y].Value == Cell.MaxValue)
                {
                    GameController.instance.Win();
                    return;
                }

                if (lose &&
                    board[x, y].IsEmpty ||
                    FindCellToMerge(board[x, y], Vector2.left) ||
                    FindCellToMerge(board[x, y], Vector2.right) ||
                    FindCellToMerge(board[x, y], Vector2.up) ||
                    FindCellToMerge(board[x, y], Vector2.down)
                )
                {
                    lose = false;
                }
            }
        }

        if(lose)
            GameController.instance.Lose();
    }

    private void CreateBoard()
    {
        board = new Cell[BoardSize, BoardSize];

        float boardWidth = BoardSize * (CellSize + Spacing) + Spacing;
        rt.sizeDelta = new Vector2(boardWidth, boardWidth);

        float startX = -(boardWidth / 2) + (CellSize / 2) + Spacing;
        float startY = (boardWidth / 2) - (CellSize / 2) - Spacing;

        for (int x = 0; x < BoardSize; x++)
        {
            for(int y = 0; y < BoardSize; y++)
            {
                var cell = Instantiate(cellPref, transform, false);
                cell.GetComponent<Image>().sprite = backgroundManager.CellImage[LevelLoader.BackgroundImage];
                var position = new Vector2(startX + (x * (CellSize + Spacing)), startY - (y * (CellSize + Spacing)));
                cell.transform.localPosition = position;

                board[x, y] = cell;

                cell.SetValue(x, y, 0);
                cell.SetSize((int)CellSize, (int)CellSize);
            }
        }
    }

    private void ClearBoard()
    {
        for(int i = this.transform.childCount-1; i >= 0; i--)
        {
            Destroy(this.transform.GetChild(i).gameObject);
        }
    }

    public void GenerateBoard(bool NextLevel = false)
    {
        switch(LevelLoader.Difficulty)
        {
            case 1:
                BoardSize = 5;
                CellSize = 150;
                Spacing = 20;
                break;
            case 2:
                BoardSize = 4;
                CellSize = 190;
                Spacing = 25;
                break;
            case 3:
                BoardSize = 3;
                CellSize = 230;
                Spacing = 30;
                break;
        }

        
        if(board == null)
        {
            CreateBoard();
        } 
        else if(NextLevel == true)
        {
            board = null;
            ClearBoard();
            CreateBoard();
        }

        int winScore = levelSettings[LevelLoader.Level-1].WinScore-(LevelLoader.Difficulty-1);

        for(int x = 0; x < BoardSize; x++)
        {
            for(int y = 0; y < BoardSize; y++)
            {
                board[x, y].SetValue(x, y, 0);
                board[x, y].SetMaxValue(winScore);
            }
        }

        ImageManager.Instance.CellSprite = levelSettings[LevelLoader.Level-1].LevelImage;
        TargetRound.sprite = levelSettings[LevelLoader.Level-1].LevelImage[winScore];

        if (!PlayerPrefs.HasKey("Score") && !PlayerPrefs.HasKey("SaveNowBoard") && !PlayerPrefs.HasKey("LastLevelPlay")) 
        {
            for(int i = 0; i < InitCellCount; i++)
                GenerateRandomCell();
        }
        else if(PlayerPrefs.GetInt("LastLevelPlay") != LevelLoader.Level)
        {
            for(int i = 0; i < InitCellCount; i++)
                GenerateRandomCell();
        }
    }

    private void GenerateRandomCell()
    {
        var emptyCells = new List<Cell>();
        for (int x = 0; x < BoardSize; x++)
            for(int y = 0; y < BoardSize; y++)
                if(board[x, y].IsEmpty)
                    emptyCells.Add(board[x, y]);

        if(emptyCells.Count == 0)
            throw new System.Exception("Пустых клеток нету");

        int value = Random.Range(0, 10) == 0 ? 2 : 1;

        var cell = emptyCells[Random.Range(0, emptyCells.Count)];
        cell.SetValue(cell.X, cell.Y, value, false);

        CellAnimationController.Instance.SmoothAppear(cell, backgroundManager.CellImage[LevelLoader.BackgroundImage]);
    }

    private void ResetCellsFlags()
    {
        for (int x = 0; x < BoardSize; x++)
            for(int y = 0; y < BoardSize; y++)
                board[x, y].ResetFlags();
    }
}
