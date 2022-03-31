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
    private LevelsScrolling levelsScrolling;
    public int selectedLevelID;
    private Sprite[] LevelsUnlockedSprites;
    private Sprite[] LevelsLockedSprites;
    private void Awake() {
        instance = this;
    }
    private void Start() {
        save = Save.instance;
        levelsScrolling = LevelsScrolling.instance;
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
            //TODO: Запустить анимацию через обьекты. Добавить 2 геймобджекта(2 торта и вызывать у них анимацию)
            levelsScrolling.PlayAnimation(true);
        } else if (rotation == false && selectedLevelID > 0) {
            selectedLevelID--;
            LeftArrow.interactable = false;
            BackLevel.SetActive(true);
            //BackLevelSprite.sprite = save.backgrounds[selectedBackgroundID];
            levelsScrolling.PlayAnimation(false);
        }
    }
    public void ActiveButtons() {
        RightArrow.interactable = true;
        LeftArrow.interactable = true;
        BackLevel.SetActive(false);
        //LevelСurrentSprite.sprite = save.backgrounds[selectedBackgroundID];
    }

    public void StartGame() {
        SceneManager.LoadScene(1);
    }
}
