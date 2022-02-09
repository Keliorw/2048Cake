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

    public Image MenuСurrentBackgroundSprite;
    public Image SettingsСurrentBackgroundSprite;
    public Image ChooseBackgroundСurrentSprite;
    public Image ShopСurrentBackgroundSprite;

    private Save save;
    private void Start()
    {
        save = Save.instance;
        MenuPanel.SetActive(true);
        SettingsPanel.SetActive(false);
        ChooseBackgroundPanel.SetActive(false);
        ShopPanel.SetActive(false);
        MenuСurrentBackgroundSprite.sprite = save.backgrounds[save.currentBackground];
    }

    public void SettingsPanelButton () {
        MenuPanel.SetActive(false);
        SettingsPanel.SetActive(true);
        ChooseBackgroundPanel.SetActive(false);
        ShopPanel.SetActive(false);
        SettingsСurrentBackgroundSprite.sprite = save.backgrounds[save.currentBackground];
    }
    public void ChooseBackgroundPanelButton () {
        MenuPanel.SetActive(false);
        SettingsPanel.SetActive(false);
        ChooseBackgroundPanel.SetActive(true);
        ShopPanel.SetActive(false);
        ChooseBackgroundСurrentSprite.sprite = save.backgrounds[save.currentBackground];
    }
    public void ShopPanelButton () {
        MenuPanel.SetActive(false);
        SettingsPanel.SetActive(false);
        ChooseBackgroundPanel.SetActive(false);
        ShopPanel.SetActive(true);
        ShopСurrentBackgroundSprite .sprite = save.backgrounds[save.currentBackground];
    }
    public void BackMenuPanelButton () {
        MenuPanel.SetActive(true);
        SettingsPanel.SetActive(false);
        ChooseBackgroundPanel.SetActive(false);
        ShopPanel.SetActive(false);
        MenuСurrentBackgroundSprite.sprite = save.backgrounds[save.currentBackground];
    }
}
