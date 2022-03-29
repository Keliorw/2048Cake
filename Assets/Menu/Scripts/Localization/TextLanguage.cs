using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextLanguage : MonoBehaviour
{
    public string language;
    Text text;

    public string textRu;
    public string textEng;
    public string textJap;
    public string textKor;
    private void Start() {
        text = GetComponent<Text>();
    }

    private void Update() {
        language = PlayerPrefs.GetString("Language");

        if (language == "" || language == "Eng") {
            text.text = textEng;
        }
        else if (language == "Ru") {
            text.text = textRu;
        }
        else if (language == "Jap") {
            text.text = textJap;
        }
        else if (language == "Kor") {
            text.text = textKor;
        }
    }
}
