using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseBackgroundScript : MonoBehaviour
{
    public static ChooseBackgroundScript instance;
    public Image ChooseBackgroundСurrentSprite;
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
        //TODO: пофиксить ошибку при закрытой панели
        save.currentBackground = backgroundScrolling.selectedBackgroundID;
        ChooseBackgroundСurrentSprite.sprite = save.backgrounds[save.currentBackground];
    }
}
