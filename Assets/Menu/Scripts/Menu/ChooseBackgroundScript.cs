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
    public GameObject GoalToUnlockText;
    private int[] neededStars = {10,16};
    private void Awake() {
        instance = this;
    }
    private void Start() {
        save = Save.instance;
        backgroundScrolling = BackgroundScrolling.instance;
        ChooseBackgroundСurrentSprite.sprite = save.backgrounds[selectedBackgroundID];
        selectedBackgroundID = save.currentBackground;
        HideChooseButton();
        GoalToUnlockText.SetActive(false);
    }

    public void ChooseBackground() {
        save.currentBackground = selectedBackgroundID;
        LevelLoader.BackgroundImage = selectedBackgroundID;
        save.SaveCurrentBackground();
        HideChooseButton(); 
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
    }
    public void ActiveButtons() {
        RightArrow.interactable = true;
        LeftArrow.interactable = true;
        BackBackground.SetActive(false);
        ChooseBackgroundСurrentSprite.sprite = save.backgrounds[selectedBackgroundID];
        HideChooseButton(); 
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
    }
    private void HideChooseButton () {
        if (selectedBackgroundID == save.currentBackground) {
            ChooseButton.SetActive(false);
        } else {
            ChooseButton.SetActive(true);
        }
        GoalToUnlock();
    }
    private void GoalToUnlock() {
        if(save.playerBackgrounds[selectedBackgroundID] == 0 && selectedBackgroundID != 0) {
            string starsCountToUnlock = "";
            switch (selectedBackgroundID) {
                case 1:
                    starsCountToUnlock = "10";
                    GoalToUnlockText.GetComponent<Text>().text = GoalToUnlockText.GetComponent<Text>().text.Replace("16", starsCountToUnlock);
                    break;
                case 2:
                    starsCountToUnlock = "16";
                    break;
            }
            
            GoalToUnlockText.SetActive(true);
        } else {
            GoalToUnlockText.SetActive(false);
        }
    }

 }
