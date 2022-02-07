using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsScript : MonoBehaviour
{
    public GameObject MenuPanel;
    public GameObject SettingsPanel;
    public GameObject ChooseBackgroundPanel;
    public GameObject ShopPanel;

    public Image MenuBackground;
    public Image SettingsBackground;
    public Image ChooseBackgroundBackground;
    public Image ShopBackground;

    private Save save;
    private void Start()
    {
        save = Save.instance;
        MenuPanel.SetActive(true);
        SettingsPanel.SetActive(false);
        ChooseBackgroundPanel.SetActive(false);
        ShopPanel.SetActive(false);
        MenuBackground.sprite = save.background;
    }

    public void SettingsPanelButton () {
        MenuPanel.SetActive(false);
        SettingsPanel.SetActive(true);
        ChooseBackgroundPanel.SetActive(false);
        ShopPanel.SetActive(false);
        SettingsBackground.sprite = save.background;
    }
    public void ChooseBackgroundPanelButton () {
        MenuPanel.SetActive(false);
        SettingsPanel.SetActive(false);
        ChooseBackgroundPanel.SetActive(true);
        ShopPanel.SetActive(false);
        ChooseBackgroundBackground.sprite = save.background;
    }
    public void ShopPanelButton () {
        MenuPanel.SetActive(false);
        SettingsPanel.SetActive(false);
        ChooseBackgroundPanel.SetActive(false);
        ShopPanel.SetActive(true);
        ShopBackground.sprite = save.background;
    }
    public void BackMenuPanelButton () {
        MenuPanel.SetActive(true);
        SettingsPanel.SetActive(false);
        ChooseBackgroundPanel.SetActive(false);
        ShopPanel.SetActive(false);
        MenuBackground.sprite = save.background;
    }
}
