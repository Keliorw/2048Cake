using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChooseLevelScript : MonoBehaviour
{
    [Range(0,30)]
    public int levelsCount;
    public static ChooseLevelScript instance;
    public GameObject CurrentLevel;
    private Image CurrentLevelSprite;
    public GameObject BackLevel;
    private Image BackLevelSprite;
    public Button RightArrow;
    public Button LeftArrow;
    private Save save;
    public int selectedLevelID;
    private Sprite[] LevelsUnlockedSprites;
    private Sprite[] LevelsLockedSprites;
    private Animator CurrentLevelAnimator;
    private Animator BackLevelAnimator;
    
    private void Awake() {
        instance = this;
    }
    private void Start() {
        save = Save.instance;
        CurrentLevelSprite = CurrentLevel.GetComponent<Image>();
        BackLevelSprite = BackLevel.GetComponent<Image>();
        LevelsUnlockedSprites = Resources.LoadAll<Sprite>("sprites/levelsUnlocked") as Sprite[];
        LevelsLockedSprites = Resources.LoadAll<Sprite>("sprites/levelsLocked") as Sprite[];
        CurrentLevelSprite.sprite = LevelsUnlockedSprites[0];
        BackLevel.SetActive(false);   
    }
    public void Scroll(bool rotation) {
        if (rotation == true && selectedLevelID < levelsCount - 1) {
            selectedLevelID++;
            RightArrow.interactable = false;
            BackLevel.SetActive(true);
            CurrentLevelSprite.sprite = LevelsUnlockedSprites[selectedLevelID-1];
            BackLevelSprite.sprite = LevelsUnlockedSprites[selectedLevelID];
            PlayAnimation(true);
        } else if (rotation == false && selectedLevelID > 0) {
            selectedLevelID--;
            LeftArrow.interactable = false;
            BackLevel.SetActive(true);
            CurrentLevelSprite.sprite = LevelsUnlockedSprites[selectedLevelID+1];
            BackLevelSprite.sprite = LevelsUnlockedSprites[selectedLevelID];
            PlayAnimation(false);
        }
    }
    public void ActiveButtons() {
        RightArrow.interactable = true;
        LeftArrow.interactable = true;
        BackLevel.SetActive(false);
        CurrentLevelSprite.sprite = LevelsUnlockedSprites[selectedLevelID];
    }
    public void PlayAnimation(bool side) {
        CurrentLevelAnimator = CurrentLevel.GetComponent<Animator>();
        BackLevelAnimator = BackLevel.GetComponent<Animator>();
        if(side){
            CurrentLevelAnimator.SetTrigger("PlayLeft");
            BackLevelAnimator.SetTrigger("PlayRight");
        } else if(!side) {
            CurrentLevelAnimator.SetTrigger("PlayRight");
            BackLevelAnimator.SetTrigger("PlayLeft");
        }
    }

    public void ScrollByButtonID (int id) {
        if (id > selectedLevelID) {
            selectedLevelID = id;
            RightArrow.interactable = false;
            BackLevel.SetActive(true);
            CurrentLevelSprite.sprite = LevelsUnlockedSprites[selectedLevelID-1];
            BackLevelSprite.sprite = LevelsUnlockedSprites[selectedLevelID];
            PlayAnimation(true);
        } else if (id < selectedLevelID) {
            selectedLevelID = id;
            LeftArrow.interactable = false;
            BackLevel.SetActive(true);
            CurrentLevelSprite.sprite = LevelsUnlockedSprites[selectedLevelID+1];
            BackLevelSprite.sprite = LevelsUnlockedSprites[selectedLevelID];
            PlayAnimation(false);
        }
    }
    public void StartGame() {
        LevelLoader.Level = selectedLevelID+1;
        LevelLoader.Difficulty = 2;
        save.SaveGameSettings();
        SceneManager.LoadScene(1);
    }
}
