using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalizationManager : MonoBehaviour
{
    public Text GoalToUnlockText;
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
        GoalToUnlockText.text = "Осталось набрать x звезд";
        PlayerPrefs.SetString("Language", language);
    }

    public void LanguageEng () {
        string language = "Eng";
        GoalToUnlockText.text = "x stars left";
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
