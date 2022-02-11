using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LevelsScrolling : MonoBehaviour
{
    public static LevelsScrolling instance;

    [Range(1, 50)]
    [Header("Controllers")]
    public int levelCount;

    [Range(0f, 20f)]
    public float snapSpeed;
    [Header("Other Objects")]
    public GameObject levelPrefab;

    private GameObject[] levelArray;
    private Vector2[] levelPrefabPos;

    public Sprite[] levelsLocked;

    public Sprite[] levelsUnlocked;

    private RectTransform contectRect;
    private Vector2 contectVector;    
    public int selectedLevelID;
    private bool isScrolling;

    private void Awake() {
        instance = this;
    }
    private void Start() {
        contectRect = GetComponent<RectTransform>();
        levelArray = new GameObject[levelCount];
        levelPrefabPos = new Vector2[levelCount];
        levelsLocked = Resources.LoadAll<Sprite>("sprites/levelsLocked") as Sprite[];
        levelsUnlocked = Resources.LoadAll<Sprite>("sprites/levelsUnlocked") as Sprite[];
        for (int i = 0; i < levelCount; i++) {
            levelPrefab.transform.GetChild(1).GetComponent<Text>().text = "Level " + (i+1).ToString();
            //TODO: изменение спрайтов кнопок. Функция плей в кнопках(второе наверное отдельным скриптом)
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
    }

    public void Scrolling(bool scroll) {
        isScrolling = scroll;
    }
}
