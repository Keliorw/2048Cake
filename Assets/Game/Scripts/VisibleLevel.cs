using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VisibleLevel : MonoBehaviour
{
    void Update()
    {
        this.GetComponent<TextMeshProUGUI>().text = "Level "+LevelLoader.Level.ToString()+" - "+LevelLoader.Difficulty.ToString();
    }
}
