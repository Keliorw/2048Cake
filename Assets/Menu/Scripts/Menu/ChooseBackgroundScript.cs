using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseBackgroundScript : MonoBehaviour
{
    public static ChooseBackgroundScript instance;
    public Image ChooseBackgroundBackground;
    public Sprite[] backgrounds;

    private BackgroundScrolling backgroundScrolling;
    private Save save;
    
    private void Awake() {
        instance = this;
    }
    private void Start() {
        backgroundScrolling = BackgroundScrolling.instance;
        save = Save.instance;
    }

    public void ChooseBackground() {
        save.background = backgrounds[backgroundScrolling.selectedPanID];
        ChooseBackgroundBackground.sprite = backgrounds[backgroundScrolling.selectedPanID];
    }
}
