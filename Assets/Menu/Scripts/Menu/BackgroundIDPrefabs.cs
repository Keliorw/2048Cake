using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundIDPrefabs : MonoBehaviour
{
    public Sprite DotActive;
    public Sprite DotUnactive;
    public Sprite DotChosen;

    [Range(0,500)]
    public int backgroundIDPrefabOffset;
    private RectTransform contectRect;
    private GameObject[] backgroundIDArray;
    public GameObject backgroundIDPrefab;

    private Vector2[] backgroundIDPrefabPos;
    private ChooseBackgroundScript chooseBackgroundScript;
    private float startPosition;
    private float maxLength;

    private float screenSize;
    private Save save;
    private void Start() {
        chooseBackgroundScript = ChooseBackgroundScript.instance;
        save = Save.instance;

        contectRect = GetComponent<RectTransform>();

        if (save.backgrounds.Length % 2==1) {
                startPosition = -(save.backgrounds.Length/2)*(backgroundIDPrefab.GetComponent<RectTransform>().sizeDelta.x + backgroundIDPrefabOffset);
            } else {
                startPosition = -(save.backgrounds.Length/2)*(backgroundIDPrefab.GetComponent<RectTransform>().sizeDelta.x + backgroundIDPrefabOffset) + (backgroundIDPrefab.GetComponent<RectTransform>().sizeDelta.x + backgroundIDPrefabOffset)/2;
            }

        backgroundIDArray = new GameObject[save.backgrounds.Length];
        backgroundIDPrefabPos = new Vector2[save.backgrounds.Length];
        for (int i = 0; i < save.backgrounds.Length; i++) {
            backgroundIDArray[i] = Instantiate(backgroundIDPrefab, transform, false);
            backgroundIDArray[0].transform.localPosition = new Vector2(startPosition,backgroundIDArray[i].transform.localPosition.y);
            if (i == 0) continue;
            backgroundIDArray[i].transform.localPosition = new Vector2(backgroundIDArray[i-1].transform.localPosition.x + backgroundIDPrefab.GetComponent<RectTransform>().sizeDelta.x + backgroundIDPrefabOffset, backgroundIDArray[i].transform.localPosition.y);
            backgroundIDPrefabPos[i] = -backgroundIDArray[i].transform.localPosition;
        }
    }

    private void FixedUpdate() {
        for (int i = 0; i < save.backgrounds.Length; i++) {
            backgroundIDArray[i].GetComponent<Image>().sprite = DotUnactive;
        }
        backgroundIDArray[chooseBackgroundScript.selectedBackgroundID].GetComponent<Image>().sprite = DotActive;
        backgroundIDArray[save.currentBackground].GetComponent<Image>().sprite = DotChosen;
    }
}
