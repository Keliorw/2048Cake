using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsScript : MonoBehaviour
{
    public GameObject MenuPanel;
    public GameObject SettingsPanel;
    public GameObject ChoosecurrentBackgroundSpritePanel;
    public GameObject ShopPanel;

    public Image MenucurrentBackgroundSprite;
    public Image SettingscurrentBackgroundSprite;
    public Image ChoosecurrentBackgroundSpritecurrentBackgroundSprite;
    public Image ShopcurrentBackgroundSprite;

    private Save save;
    private void Start()
    {
        save = Save.instance;
        MenuPanel.SetActive(true);
        SettingsPanel.SetActive(false);
        ChoosecurrentBackgroundSpritePanel.SetActive(false);
        ShopPanel.SetActive(false);
        MenucurrentBackgroundSprite.sprite = save.currentBackgroundSprite;
    }

    public void SettingsPanelButton () {
        MenuPanel.SetActive(false);
        SettingsPanel.SetActive(true);
        ChoosecurrentBackgroundSpritePanel.SetActive(false);
        ShopPanel.SetActive(false);
        SettingscurrentBackgroundSprite.sprite = save.currentBackgroundSprite;
    }
    public void ChoosecurrentBackgroundSpritePanelButton () {
        MenuPanel.SetActive(false);
        SettingsPanel.SetActive(false);
        ChoosecurrentBackgroundSpritePanel.SetActive(true);
        ShopPanel.SetActive(false);
        ChoosecurrentBackgroundSpritecurrentBackgroundSprite.sprite = save.currentBackgroundSprite;
    }
    public void ShopPanelButton () {
        MenuPanel.SetActive(false);
        SettingsPanel.SetActive(false);
        ChoosecurrentBackgroundSpritePanel.SetActive(false);
        ShopPanel.SetActive(true);
        ShopcurrentBackgroundSprite.sprite = save.currentBackgroundSprite;
    }
    public void BackMenuPanelButton () {
        MenuPanel.SetActive(true);
        SettingsPanel.SetActive(false);
        ChoosecurrentBackgroundSpritePanel.SetActive(false);
        ShopPanel.SetActive(false);
        MenucurrentBackgroundSprite.sprite = save.currentBackgroundSprite;
    }
}
