using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizationManager : MonoBehaviour
{

    public void LanguageRU () {
        string language = "Ru";
        PlayerPrefs.SetString("Language", language);
    }

    public void LanguageEng () {
        string language = "Eng";
        PlayerPrefs.SetString("Language", language);
    }

}
