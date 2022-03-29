using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsScript : MonoBehaviour
{
    public GameObject MenuPanel;
    public GameObject SettingsPanel;
    public GameObject ChooseBackgroundPanel;
    private void Start()
    {
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
        MenuPanel.SetActive(false);
        SettingsPanel.SetActive(false);
        ChooseBackgroundPanel.SetActive(true);
    }
    public void BackMenuPanelButton () {
        MenuPanel.SetActive(true);
        SettingsPanel.SetActive(false);
        ChooseBackgroundPanel.SetActive(false);
    }
}
