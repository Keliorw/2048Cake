using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundScrolling : MonoBehaviour
{
    public GameObject ChooseBackgroundButton;
    public GameObject ChooseText;
    public GameObject BuyText;
    public static BackgroundScrolling instance;

    [Header("Controllers")]
    public int backgroundCount;

    [Range(0f, 20f)]
    public float snapSpeed;
    [Header("Other Objects")]
    public GameObject backgroundPrefab;

    private GameObject[] instPans;
    private Vector2[] backgroundPrefabPos;

    private RectTransform contectRect;
    private Vector2 contectVector;
    
    public int selectedBackgroundID;
    private bool isScrolling;
    private Save save;

    private void Awake() {
        instance = this;
    }
    private void Start() {
        save = Save.instance;
        backgroundCount = save.backgrounds.Length;

        contectRect = GetComponent<RectTransform>();
        instPans = new GameObject[backgroundCount];
        backgroundPrefabPos = new Vector2[backgroundCount];
        for (int i = 0; i < backgroundCount; i++) {
            instPans[i] = Instantiate(backgroundPrefab, transform, false);
            instPans[i].GetComponent<Image>().sprite = save.backgrounds[i];
            if (i == 0) continue;
            instPans[i].transform.localPosition = new Vector2(instPans[i-1].transform.localPosition.x + backgroundPrefab.GetComponent<RectTransform>().sizeDelta.x, instPans[i].transform.localPosition.y);
            backgroundPrefabPos[i] = -instPans[i].transform.localPosition;
        }
    }

    private void FixedUpdate() {
        float nearestPos = float.MaxValue;
        for (int i = 0; i < backgroundCount; i++) {
            float distance = Mathf.Abs(contectRect.anchoredPosition.x - backgroundPrefabPos[i].x);
            if (distance < nearestPos) {
                nearestPos = distance;
                selectedBackgroundID = i;
            }
        }
        if (save.playerBackgrounds[selectedBackgroundID] == 1) {
            ChooseText.SetActive(true);
            BuyText.SetActive(false);
        } else {
            ChooseText.SetActive(false);
            BuyText.SetActive(true);
        }
        if (selectedBackgroundID == save.currentBackground) {
            ChooseBackgroundButton.SetActive(false);
        } else { 
            ChooseBackgroundButton.SetActive(true);
        }
        if (isScrolling) return;
        contectVector.x = Mathf.SmoothStep(contectRect.anchoredPosition.x, backgroundPrefabPos[selectedBackgroundID].x, snapSpeed * Time.fixedDeltaTime);
        contectRect.anchoredPosition = contectVector;
    }

    public void Scrolling(bool scroll) {
        isScrolling = scroll;
    }
}
