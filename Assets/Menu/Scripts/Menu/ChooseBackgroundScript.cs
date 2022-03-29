using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseBackgroundScript : MonoBehaviour
{
    public static ChooseBackgroundScript instance;
    public Image ChooseBackgroundСurrentSprite;
    private Save save;
    public int selectedBackgroundID;
    
    private void Awake() {
        instance = this;
    }
    private void Start() {
        save = Save.instance;
        ChooseBackgroundСurrentSprite.sprite = save.backgrounds[selectedBackgroundID];
    }

    public void ChooseBackground() {
        save.currentBackground = selectedBackgroundID;
    }

    public void Scroll(bool rotation) {
        if (rotation == true && selectedBackgroundID < save.backgrounds.Length - 1) {
            selectedBackgroundID++;
        } else if (rotation == false && selectedBackgroundID > 0) {
            selectedBackgroundID--;
        }
        ChooseBackgroundСurrentSprite.sprite = save.backgrounds[selectedBackgroundID];
    }
}
