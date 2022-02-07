using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundScrolling : MonoBehaviour
{
    public static BackgroundScrolling instance;

    [Range(1, 50)]
    [Header("Controllers")]
    public int panCount;

    [Range(0f, 20f)]
    public float snapSpeed;
    [Header("Other Objects")]
    public GameObject panPrefab;

    private GameObject[] instPans;
    private Vector2[] pansPos;

    private RectTransform contectRect;
    private Vector2 contectVector;
    
    public int selectedPanID;
    private bool isScrolling;

    public GameObject[] levelID;

    private ChooseBackgroundScript chooseBackgroundScript;

    private void Awake() {
        instance = this;
    }
    private void Start() {
        chooseBackgroundScript = ChooseBackgroundScript.instance;
        contectRect = GetComponent<RectTransform>();
        instPans = new GameObject[panCount];
        pansPos = new Vector2[panCount];
        for (int i = 0; i < panCount; i++) {
            instPans[i] = Instantiate(panPrefab, transform, false);
            instPans[i].GetComponent<Image>().sprite = chooseBackgroundScript.backgrounds[i];
            if (i == 0) continue;
            instPans[i].transform.localPosition = new Vector2(instPans[i-1].transform.localPosition.x + panPrefab.GetComponent<RectTransform>().sizeDelta.x, instPans[i].transform.localPosition.y);
            pansPos[i] = -instPans[i].transform.localPosition;
        }
        for (int i = 0; i < panCount; i++) {
            
        }
    }

    private void FixedUpdate() {
        float nearestPos = float.MaxValue;
        for (int i = 0; i < panCount; i++) {
            float distance = Mathf.Abs(contectRect.anchoredPosition.x - pansPos[i].x);
            if (distance < nearestPos) {
                nearestPos = distance;
                selectedPanID = i;
            }
        }
        if (isScrolling) return;
        contectVector.x = Mathf.SmoothStep(contectRect.anchoredPosition.x, pansPos[selectedPanID].x, snapSpeed * Time.fixedDeltaTime);
        contectRect.anchoredPosition = contectVector;
        
        //TODO: генерировать префабы levelID в количестве уровней
        for (int i = 0; i < panCount; i++) {
            levelID[i].GetComponent<Image>().color = Color.white;
        }
        levelID[selectedPanID].GetComponent<Image>().color = Color.red;
    }

    public void Scrolling(bool scroll) {
        isScrolling = scroll;
    }
}
