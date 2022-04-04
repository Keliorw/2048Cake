using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fps : MonoBehaviour
{
    private void Awake() 
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 300;
    }
}
