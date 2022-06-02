using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
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
}
