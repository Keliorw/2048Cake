using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckAnimationEnd : MonoBehaviour
{
    private ChooseBackgroundScript chooseBackgroundScript;
    private ChooseLevelScript chooseLevelScript;
    private void Start() {
        chooseBackgroundScript = ChooseBackgroundScript.instance;
        chooseLevelScript = ChooseLevelScript.instance;
    }
    public void isAnimationEnd () {
        chooseBackgroundScript.ActiveButtons();
    }
    public void isAnimationPlayButtonEnd () {
        chooseLevelScript.ActiveButtons();
    }
}
