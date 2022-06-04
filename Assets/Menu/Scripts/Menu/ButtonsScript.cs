using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsScript : MonoBehaviour
{
    public GameObject MenuPanel;
    public GameObject SettingsPanel;
    public GameObject ChooseBackgroundPanel;
    public GameObject BackBackgroundImage;
    private ChooseBackgroundScript chooseBackgroundScript;
    private ChooseLevelScript chooseLevelScript;
    private void Start()
    {
        chooseBackgroundScript = ChooseBackgroundScript.instance;
        chooseLevelScript = ChooseLevelScript.instance;
        MenuPanel.SetActive(true);
        SettingsPanel.SetActive(false);
        ChooseBackgroundPanel.SetActive(false);
    }

    public void SettingsPanelButton () {
        MenuPanel.SetActive(false);
        SettingsPanel.SetActive(true);
        ChooseBackgroundPanel.SetActive(false);
    }
    public void ChooseBackgroundPanelButton () {
        BackBackgroundImage.SetActive(false);
        MenuPanel.SetActive(false);
        SettingsPanel.SetActive(false);
        ChooseBackgroundPanel.SetActive(true);
        chooseBackgroundScript.OpenCurrentBackground();
    }
    public void BackMenuPanelButton () {
        MenuPanel.SetActive(true);
        SettingsPanel.SetActive(false);
        ChooseBackgroundPanel.SetActive(false);
        chooseLevelScript.OpenCurrentLevel();
    }
}
