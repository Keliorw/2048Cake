using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundManager : MonoBehaviour
{
    public static BackgroundManager Instance;

    [Header("Объекты в которых надо менять Sprite")]
    [Space(5)]
    public GameObject Background;

    [Header("Объекты в которых надо менять Color")]
    [Space(5)]
    public GameObject BoardBackground;

    [Header("Фоны в виде Sprite")]
    [Space(5)]
    public Sprite[] BackgroundsImage;

    [Header("Цвета для игрового поля")]
    [Space(5)]
    public Color32[] BoardColor;

    [Header("Картинки ячейки для игрового поля")]
    [Space(5)]
    public Sprite[] CellImage;

    [Header("Объекты, которые надо спавнить")]
    [Space(5)]
    public SpawnObjectList[] SpawnObject;

    [Header("Координаты где спавнить объекты")]
    [Space(5)]
    public CoordinateData[] PositionSpawnObject;

    void Awake() 
    {
        if(Instance == null)
            Instance = this;
    }

    void Start()
    {
        int NumberItem = LevelLoader.BackgroundImage;
        Background.GetComponent<Image>().sprite = BackgroundsImage[NumberItem];
        BoardBackground.GetComponent<Image>().color = BoardColor[NumberItem];

        int numerator = 0;
        while(numerator != SpawnObject[NumberItem].SpawnObjectListInLevel.Length)
        {
            SpawnObject[NumberItem].SpawnObjectListInLevel[numerator].localPosition = PositionSpawnObject[NumberItem].PositionObjects[numerator];
            numerator++;
        }
    }
}
