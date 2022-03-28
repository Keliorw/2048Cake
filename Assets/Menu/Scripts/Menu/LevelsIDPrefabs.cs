using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LevelsIDPrefabs : MonoBehaviour
{
    public Sprite DotActive;
    public Sprite DotUnactive;

    [Range(0,500)]
    public int levelIDPrefabOffset;
    private RectTransform contectRect;
    private GameObject[] levelIDArray;
    public GameObject levelIDPrefab;
    private LevelsScrolling levelsScrolling;

    private float startPosition;
    private void Start() {
        levelsScrolling = LevelsScrolling.instance;

        contectRect = GetComponent<RectTransform>();

        if (levelsScrolling.levelCount % 2==1) {
                startPosition = -(levelsScrolling.levelCount/2)*(levelIDPrefab.GetComponent<RectTransform>().sizeDelta.x + levelIDPrefabOffset);
            } else {
                startPosition = -(levelsScrolling.levelCount/2)*(levelIDPrefab.GetComponent<RectTransform>().sizeDelta.x + levelIDPrefabOffset) + (levelIDPrefab.GetComponent<RectTransform>().sizeDelta.x + levelIDPrefabOffset)/2;
            }

        levelIDArray = new GameObject[levelsScrolling.levelCount];
        for (int i = 0; i < levelsScrolling.levelCount; i++) {
            levelIDArray[i] = Instantiate(levelIDPrefab, transform, false);
            levelIDArray[0].transform.localPosition = new Vector2(startPosition,levelIDArray[i].transform.localPosition.y);
            if (i == 0) continue;
            levelIDArray[i].transform.localPosition = new Vector2(levelIDArray[i-1].transform.localPosition.x + levelIDPrefab.GetComponent<RectTransform>().sizeDelta.x + levelIDPrefabOffset, levelIDArray[i].transform.localPosition.y);
        }
    }

    private void FixedUpdate() {
        for (int i = 0; i < levelsScrolling.levelCount; i++) {
            levelIDArray[i].GetComponent<Image>().sprite = DotUnactive;
        }
        levelIDArray[levelsScrolling.selectedLevelID].GetComponent<Image>().sprite = DotActive;
    }
}
