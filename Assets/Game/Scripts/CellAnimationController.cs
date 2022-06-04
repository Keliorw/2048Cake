using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CellAnimationController : MonoBehaviour
{
    public static CellAnimationController Instance {get; private set;}

    private BackgroundManager backgroundManager;

    [SerializeField]
    private CellAnimation animationPref;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;

        DOTween.Init();
    }

    private void Start()
    {
        backgroundManager = BackgroundManager.Instance;
    }
    
    public void SmoothTransition(Cell from, Cell to, bool isMerging, Sprite NowImageBlock)
    {
        CellAnimation CellAnimation = Instantiate(animationPref, transform, false);
        CellAnimation.GetComponent<Image>().sprite = NowImageBlock;
        CellAnimation.GetComponent<RectTransform>().sizeDelta = new Vector2(from.GetComponent<RectTransform>().sizeDelta.x, from.GetComponent<RectTransform>().sizeDelta.y);
        CellAnimation.Move(from, to, isMerging);
    }

    public void SmoothAppear(Cell cell, Sprite NowImageBlock)
    {
        CellAnimation CellAnimation = Instantiate(animationPref, transform, false);
        CellAnimation.GetComponent<Image>().sprite = NowImageBlock;
        CellAnimation.GetComponent<RectTransform>().sizeDelta = new Vector2(cell.GetComponent<RectTransform>().sizeDelta.x, cell.GetComponent<RectTransform>().sizeDelta.y);
        CellAnimation.Appear(cell);
    }
}
