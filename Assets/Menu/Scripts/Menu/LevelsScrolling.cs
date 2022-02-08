using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LevelsScrolling : MonoBehaviour
{
    [Range(1, 50)]
    [Header("Controllers")]
    public int levelCount;

    [Range(0f, 20f)]
    public float snapSpeed;
    [Header("Other Objects")]
    public GameObject levelPrefab;

    private GameObject[] levelArray;
    private Vector2[] levelPrefabPos;

    private RectTransform contectRect;
    private Vector2 contectVector;
    
    private int selectedLevelID;
    private bool isScrolling;

    public GameObject[] levelID;

    private void Start() {
        contectRect = GetComponent<RectTransform>();
        levelArray = new GameObject[levelCount];
        levelPrefabPos = new Vector2[levelCount];
        for (int i = 0; i < levelCount; i++) {
            levelArray[i] = Instantiate(levelPrefab, transform, false);
            if (i == 0) continue;
            levelArray[i].transform.localPosition = new Vector2(levelArray[i-1].transform.localPosition.x + levelPrefab.GetComponent<RectTransform>().sizeDelta.x, levelArray[i].transform.localPosition.y);
            levelPrefabPos[i] = -levelArray[i].transform.localPosition;
        }
    }

    private void FixedUpdate() {
        float nearestPos = float.MaxValue;
        for (int i = 0; i < levelCount; i++) {
            float distance = Mathf.Abs(contectRect.anchoredPosition.x - levelPrefabPos[i].x);
            if (distance < nearestPos) {
                nearestPos = distance;
                selectedLevelID = i;
            }
        }
        if (isScrolling) return;
        contectVector.x = Mathf.SmoothStep(contectRect.anchoredPosition.x, levelPrefabPos[selectedLevelID].x, snapSpeed * Time.fixedDeltaTime);
        contectRect.anchoredPosition = contectVector;
        
        //TODO: генерировать префабы levelID в количестве уровней
        for (int i = 0; i < levelCount; i++) {
            levelID[i].GetComponent<Image>().color = Color.white;
        }
        levelID[selectedLevelID].GetComponent<Image>().color = Color.red;
    }

    public void Scrolling(bool scroll) {
        isScrolling = scroll;
    }
}
