using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LevelsIDPrefabs : MonoBehaviour
{
    [Range(0,500)]
    public int levelIDPrefabOffset;
    private RectTransform contectRect;
    private GameObject[] levelIDArray;
    public GameObject levelIDPrefab;

    private Vector2[] levelIDPrefabPos;

    private LevelsScrolling levelsScrolling;
    private void Start() {
        levelsScrolling = LevelsScrolling.instance;

        contectRect = GetComponent<RectTransform>();

        levelIDArray = new GameObject[levelsScrolling.levelCount];
        levelIDPrefabPos = new Vector2[levelsScrolling.levelCount];
        for (int i = 0; i < levelsScrolling.levelCount; i++) {
            levelIDArray[i] = Instantiate(levelIDPrefab, transform, false);
            if (i == 0) continue;
            levelIDArray[i].transform.localPosition = new Vector2(levelIDArray[i-1].transform.localPosition.x + levelIDPrefab.GetComponent<RectTransform>().sizeDelta.x + levelIDPrefabOffset, levelIDArray[i].transform.localPosition.y);
            levelIDPrefabPos[i] = -levelIDArray[i].transform.localPosition;
        }
    }

    private void FixedUpdate() {
        for (int i = 0; i < levelsScrolling.levelCount; i++) {
            levelIDArray[i].GetComponent<Image>().color = Color.white;
        }
        levelIDArray[levelsScrolling.selectedLevelID].GetComponent<Image>().color = Color.red;
    }
}
