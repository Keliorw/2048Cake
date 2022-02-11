using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageManager : MonoBehaviour
{
    public static ImageManager Instance;

    public Image[] CellImage;
    public Color[] CellColors;

    [Space(5)]
    public Color PointsDarkColor;
    public Color PointsLightColor;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }

}
