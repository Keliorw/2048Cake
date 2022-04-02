using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fps : MonoBehaviour
{

    public static float fps;
    public Text FpsText;
    private void Awake() 
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 300;
    }

    private void OnGUI() 
    {
        fps = 1.0f / Time.deltaTime;
        FpsText.text = "FPS: " + ((int)fps).ToString();
    }
}
