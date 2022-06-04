using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalizationManager : MonoBehaviour
{
    private void Start() {
        string language = PlayerPrefs.GetString("Language");
        if (language == "" || language == "Eng") {
            LanguageEng();
        }
        else if (language == "Ru") {
            LanguageRU();
        }
        else if (language == "Jap") {
            LanguageJap();
        }
        else if (language == "Kor") {
            LanguageKor();
        }
    }
    public void LanguageRU () {
        string language = "Ru";
        PlayerPrefs.SetString("Language", language);
    }

    public void LanguageEng () {
        string language = "Eng";
        PlayerPrefs.SetString("Language", language);
    }

    public void LanguageJap() {
        string language = "Jap";
        PlayerPrefs.SetString("Language", language);
    }
    public void LanguageKor () {
        string language = "Kor";
        PlayerPrefs.SetString("Language", language);
    }
}
