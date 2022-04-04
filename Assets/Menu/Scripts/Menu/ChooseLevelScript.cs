using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChooseLevelScript : MonoBehaviour
{
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
    public GameObject CurrentLevelPlayButton;
    public GameObject BackLevelPlayButton;
    public Image[] StarsSprite;
    public Image[] BackStarsSprite;
    public Sprite ActiveStar;
    public Sprite UnactiveStar;
    private int previousSelectedLevelID;
    private void Awake() {
        instance = this;
    }
    private void Start() {
        save = Save.instance;
        CurrentLevelSprite = CurrentLevelPlayButton.GetComponent<Image>();
        BackLevelSprite = BackLevelPlayButton.GetComponent<Image>();
        LevelsUnlockedSprites = Resources.LoadAll<Sprite>("sprites/levelsUnlocked") as Sprite[];
        LevelsLockedSprites = Resources.LoadAll<Sprite>("sprites/levelsLocked") as Sprite[];
        CurrentLevelSprite.sprite = LevelsUnlockedSprites[0];
        for (int i = 0; i < StarsSprite.Length; i++) {
            if(i < save.stars[0]) {
                StarsSprite[i].sprite = ActiveStar;
            } else {
                StarsSprite[i].sprite = UnactiveStar;
            }
        }
        previousSelectedLevelID = 0;
        BackLevel.SetActive(false);  
    }
    public void Scroll(bool rotation) {
        if (rotation == true && selectedLevelID < save.levelsCount - 1) {
            selectedLevelID++;
            RightArrow.interactable = false;
            BackLevel.SetActive(true);
            CurrentLevelSprite.sprite = LevelsUnlockedSprites[selectedLevelID];
            BackLevelSprite.sprite = LevelsUnlockedSprites[selectedLevelID-1];
            for (int i = 0; i < StarsSprite.Length; i++) {
                if(i < save.stars[selectedLevelID]) {
                    StarsSprite[i].sprite = ActiveStar;
                } else {
                    StarsSprite[i].sprite = UnactiveStar;
                }
            }
            for (int i = 0; i < BackStarsSprite.Length; i++) {
                if(i < save.stars[selectedLevelID-1]) {
                    BackStarsSprite[i].sprite = ActiveStar;
                } else {
                    BackStarsSprite[i].sprite = UnactiveStar;
                }
            }
            LockLevel(selectedLevelID);
            PlayAnimation(true);
        } else if (rotation == false && selectedLevelID > 0) {
            selectedLevelID--;
            LeftArrow.interactable = false;
            BackLevel.SetActive(true);
            CurrentLevelSprite.sprite = LevelsUnlockedSprites[selectedLevelID];
            BackLevelSprite.sprite = LevelsUnlockedSprites[selectedLevelID+1];
            for (int i = 0; i < StarsSprite.Length; i++) {
                if(i < save.stars[selectedLevelID]) {
                    StarsSprite[i].sprite = ActiveStar;
                } else {
                    StarsSprite[i].sprite = UnactiveStar;
                }
            }
            for (int i = 0; i < BackStarsSprite.Length; i++) {
                if(i < save.stars[selectedLevelID+1]) {
                    BackStarsSprite[i].sprite = ActiveStar;
                } else {
                    BackStarsSprite[i].sprite = UnactiveStar;
                }
            }
            LockLevel(selectedLevelID);
            PlayAnimation(false);
        }
    }
    public void ActiveButtons() {
        BackLevel.SetActive(false);
        CurrentLevelSprite.sprite = LevelsUnlockedSprites[selectedLevelID];
        RightArrow.interactable = true;
        LeftArrow.interactable = true;
    }
    public void PlayAnimation(bool side) {
        CurrentLevelAnimator = CurrentLevel.GetComponent<Animator>();
        BackLevelAnimator = BackLevel.GetComponent<Animator>();
        if(side){
            CurrentLevelAnimator.SetTrigger("PlayRight");
            BackLevelAnimator.SetTrigger("PlayLeft");
        } else if(!side) {
            CurrentLevelAnimator.SetTrigger("PlayLeft");
            BackLevelAnimator.SetTrigger("PlayRight");
        }
    }

    public void ScrollByButtonsID (int id) {
        if (id > selectedLevelID) {
            RightArrow.interactable = false;
            BackLevel.SetActive(true);
            CurrentLevelSprite.sprite = LevelsUnlockedSprites[id];
            BackLevelSprite.sprite = LevelsUnlockedSprites[selectedLevelID];
            for (int i = 0; i < StarsSprite.Length; i++) {
                if(i < save.stars[id]) {
                    StarsSprite[i].sprite = ActiveStar;
                } else {
                    StarsSprite[i].sprite = UnactiveStar;
                }
            }
            for (int i = 0; i < BackStarsSprite.Length; i++) {
                if(i < save.stars[selectedLevelID]) {
                    BackStarsSprite[i].sprite = ActiveStar;
                } else {
                    BackStarsSprite[i].sprite = UnactiveStar;
                }
            }
            LockLevel(id);
            selectedLevelID = id;
            PlayAnimation(true);
        } else if (id < selectedLevelID) {
            LeftArrow.interactable = false;
            BackLevel.SetActive(true);
            CurrentLevelSprite.sprite = LevelsUnlockedSprites[id];
            BackLevelSprite.sprite = LevelsUnlockedSprites[selectedLevelID];
            for (int i = 0; i < StarsSprite.Length; i++) {
                if(i < save.stars[id]) {
                    StarsSprite[i].sprite = ActiveStar;
                } else {
                    StarsSprite[i].sprite = UnactiveStar;
                }
            }
            for (int i = 0; i < BackStarsSprite.Length; i++) {
                if(i < save.stars[selectedLevelID]) {
                    BackStarsSprite[i].sprite = ActiveStar;
                } else {
                    BackStarsSprite[i].sprite = UnactiveStar;
                }
            }
            LockLevel(id);
            selectedLevelID = id;
            PlayAnimation(false);
        }
    }
    private void LockLevel(int id) {
        if (id >= save.levelsPassed) {
            if (previousSelectedLevelID < save.levelsPassed) {
                CurrentLevel.GetComponent<Image>().color = new Color32(100,100,100,255);
                BackLevel.GetComponent<Image>().color = new Color32(255,255,255,255);
                CurrentLevelPlayButton.GetComponent<Button>().interactable = false;
                BackLevelPlayButton.GetComponent<Button>().interactable = true;
                for (int i = 0; i < StarsSprite.Length; i++) {
                    StarsSprite[i].color = new Color32(100,100,100,255);
                }
                for (int i = 0; i < BackStarsSprite.Length; i++) {
                    BackStarsSprite[i].color = new Color32(255,255,255,255);
                }
            } else {
                CurrentLevel.GetComponent<Image>().color = new Color32(100,100,100,255);
                BackLevel.GetComponent<Image>().color = new Color32(100,100,100,255);
                CurrentLevelPlayButton.GetComponent<Button>().interactable = false;
                BackLevelPlayButton.GetComponent<Button>().interactable = false;
                for (int i = 0; i < StarsSprite.Length; i++) {
                    StarsSprite[i].color = new Color32(100,100,100,255);
                }
                for (int i = 0; i < BackStarsSprite.Length; i++) {
                    BackStarsSprite[i].color = new Color32(100,100,100,255);
                }
            }
        }  else {
            if (previousSelectedLevelID >= save.levelsPassed) {
                CurrentLevel.GetComponent<Image>().color = new Color32(255,255,255,255);
                BackLevel.GetComponent<Image>().color = new Color32(100,100,100,255);
                CurrentLevelPlayButton.GetComponent<Button>().interactable = true;
                BackLevelPlayButton.GetComponent<Button>().interactable = false;
                for (int i = 0; i < StarsSprite.Length; i++) {
                    StarsSprite[i].color = new Color32(255,255,255,255);
                }
                for (int i = 0; i < BackStarsSprite.Length; i++) {
                    BackStarsSprite[i].color = new Color32(100,100,100,255);
                }
            } else {
                CurrentLevel.GetComponent<Image>().color = new Color32(255,255,255,255);
                BackLevel.GetComponent<Image>().color = new Color32(255,255,255,255);
                CurrentLevelPlayButton.GetComponent<Button>().interactable = true;
                BackLevelPlayButton.GetComponent<Button>().interactable = true;
                for (int i = 0; i < StarsSprite.Length; i++) {
                    StarsSprite[i].color = new Color32(255,255,255,255);
                }
                for (int i = 0; i < BackStarsSprite.Length; i++) {
                    BackStarsSprite[i].color = new Color32(255,255,255,255);
                }
            }
        }
        previousSelectedLevelID = id;
    }
    public void StartGame() {
        LevelLoader.Level = selectedLevelID+1;
        LevelLoader.Difficulty = (save.stars[selectedLevelID] + 1) == 4 ? save.stars[selectedLevelID] : (save.stars[selectedLevelID] + 1);
        save.SaveGameSettings();
        SceneManager.LoadScene(1);
    }
}
