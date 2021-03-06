using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    public int X {get; private set;}
    public int Y {get; private set;}

    public int SizeX {get; private set;}
    public int SizeY {get; private set;}

    public int Value {get; private set;}
    public int Points => IsEmpty ? 0 : (int)Mathf.Pow(2, Value);

    public bool IsEmpty => Value == 0;
    public bool HasMerge { get; private set; }

    public static int MaxValue { get; private set; }

    [SerializeField]
    private Image image;
    [SerializeField]
    private Image points;

    private CellAnimation currentAnimation;

    public void SetValue(int x, int y, int value, bool updateUI = true, bool updateCoordinats = true)
    {
        if(updateCoordinats) 
        {
            X = x;
            Y = y;
        }
        
        Value = value;

        if(updateUI)
            UpdateCell();
    }

    public void SetSize(int SizeX, int SizeY)
    {
        this.SizeX = SizeX;
        this.SizeY = SizeY;
        this.GetComponent<RectTransform>().sizeDelta = new Vector2(this.SizeX, this.SizeY);
    }

    public void SetMaxValue(int Value)
    {
        MaxValue = Value;
    }

    public void IncreaseValue()
    {
        Value++;
        HasMerge = true;

        GameController.instance.AddPoints(Points);
    }

    public void ResetFlags()
    {
        HasMerge = false;
    }

    public void MergeWithCell(Cell otherCell, Sprite NowImageBlock)
    {
        CellAnimationController.Instance.SmoothTransition(this, otherCell, true, NowImageBlock);

        otherCell.IncreaseValue();
        SetValue(X, Y, 0);
    }

    public void MoveToCell(Cell target, Sprite NowImageBlock)
    {
        CellAnimationController.Instance.SmoothTransition(this, target, false, NowImageBlock);

        target.SetValue(target.X, target.Y, Value, false);
        SetValue(X, Y, 0);
    }

    public void UpdateCell()
    {
        points.sprite = ImageManager.Instance.CellSprite[Value];
        points.color = Value == 0 ?  ImageManager.Instance.PointsDarkColor : ImageManager.Instance.PointsLightColor;
    }

    public void SetAnimation(CellAnimation animation)
    {
        currentAnimation = animation;
    }

    public void CancelAnimation()
    {
        if(currentAnimation != null)
            currentAnimation.Destroy();
    }
}
