using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseBackgroundScript : MonoBehaviour
{
    public static ChooseBackgroundScript instance;
    public Image ChooseBackgroundСurrentSprite;
    public GameObject BackBackground;
    public Image BackBackgroundSprite;
    public Button RightArrow;
    public Button LeftArrow;
    private Save save;
    private BackgroundScrolling backgroundScrolling;
    public int selectedBackgroundID;
    public GameObject ChooseButton;
    public GameObject CloseBackground;
    public GameObject[] RequiredText;
    private int[] neededStars = {10,16};
    private void Awake() {
        instance = this;
    }
    private void Start() {
        save = Save.instance;
        backgroundScrolling = BackgroundScrolling.instance;
        CloseBackground.SetActive(false);
        for (int i = 0; i < RequiredText.Length; i++) {
            RequiredText[i].SetActive(false);
        }
    }

    public void OpenCurrentBackground() {
        selectedBackgroundID = save.currentBackground;
        ChooseBackgroundСurrentSprite.sprite = save.backgrounds[selectedBackgroundID];
        CheckArrowSides();
        CloseBackgrounds();
    }
    
    public void CloseBackgrounds() {
        CloseBackground.SetActive(false);
        for (int i = 0; i < RequiredText.Length; i++) {
            RequiredText[i].SetActive(false);
        }
        if (selectedBackgroundID == save.currentBackground) {
            ChooseButton.SetActive(false);
        } else {
            ChooseButton.SetActive(true);
        }
        if (save.playerBackgrounds[selectedBackgroundID] == 0) {
            CloseBackground.SetActive(true);
            RequiredText[selectedBackgroundID-1].SetActive(true);
            ChooseButton.SetActive(false);
        }
    }
    public void ChooseBackground() {
        save.currentBackground = selectedBackgroundID;
        LevelLoader.BackgroundImage = selectedBackgroundID;
        save.SaveCurrentBackground();
        ChooseButton.SetActive(false);
    }
    public void Scroll(bool rotation) {
        if (rotation == true && selectedBackgroundID < save.backgrounds.Length - 1) {
            selectedBackgroundID++;
            RightArrow.interactable = false;
            BackBackground.SetActive(true);
            BackBackgroundSprite.sprite = save.backgrounds[selectedBackgroundID];
            backgroundScrolling.PlayAnimation(true);
        } else if (rotation == false && selectedBackgroundID > 0) {
            selectedBackgroundID--;
            LeftArrow.interactable = false;
            BackBackground.SetActive(true);
            BackBackgroundSprite.sprite = save.backgrounds[selectedBackgroundID];
            backgroundScrolling.PlayAnimation(false);
        }
        CloseBackgrounds();
    }
    public void ActiveButtons() {
        RightArrow.interactable = true;
        LeftArrow.interactable = true;
        BackBackground.SetActive(false);
        ChooseBackgroundСurrentSprite.sprite = save.backgrounds[selectedBackgroundID];
        CheckArrowSides();
    }

    public void ScrollByButtonsID (int id) {
        if (id > selectedBackgroundID) {
            selectedBackgroundID = id;
            RightArrow.interactable = false;
            BackBackground.SetActive(true);
            BackBackgroundSprite.sprite = save.backgrounds[selectedBackgroundID];
            backgroundScrolling.PlayAnimation(true);
        } else if (id < selectedBackgroundID) {
            selectedBackgroundID = id;
            LeftArrow.interactable = false;
            BackBackground.SetActive(true);
            BackBackgroundSprite.sprite = save.backgrounds[selectedBackgroundID];
            backgroundScrolling.PlayAnimation(false);
        }
        CloseBackgrounds();
    }
    private void CheckArrowSides() {
        if (selectedBackgroundID == 0) {
            RightArrow.interactable = true;
            LeftArrow.interactable = false;
        } else if (selectedBackgroundID == (save.backgrounds.Length - 1)) {
            RightArrow.interactable = false;
            LeftArrow.interactable = true;
        } else {
            RightArrow.interactable = true;
            LeftArrow.interactable = true;
        }    
    }

 }
