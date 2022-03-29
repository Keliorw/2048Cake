using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusButton : MonoBehaviour
{
    public static StatusButton Instance;

    public bool isActive = false;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }

    private void Start()
    {
        this.UpdateStatus();
    }

    public void UpdateStatus()
    {
        if(isActive)
        {
            this.gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            this.gameObject.GetComponent<Button>().interactable = true;
        }
        else 
        {
            this.gameObject.GetComponent<Image>().color = new Color32(100, 100, 100, 255);
            this.gameObject.GetComponent<Button>().interactable = false;
        }
    }
}
