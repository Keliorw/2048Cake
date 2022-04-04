using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fps : MonoBehaviour
{
<<<<<<< HEAD:Assets/Game/Scripts/Fps.cs
=======

    public static float fps;
>>>>>>> 26e3b74d28d9e01bfdc3f19ab614095de04b6681:Assets/Scripts/Fps.cs
    private void Awake() 
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 300;
    }
}
