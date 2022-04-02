using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class ScrollBackgroundsByButtonID : MonoBehaviour, IPointerClickHandler
{
    private ChooseBackgroundScript chooseBackgroundScript;
    private GameObject father;
    private void Start() {
        chooseBackgroundScript = ChooseBackgroundScript.instance;
        father = this.transform.parent.gameObject;
    }
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        chooseBackgroundScript.ScrollByButtonsID(int.Parse(name));
    }
}
