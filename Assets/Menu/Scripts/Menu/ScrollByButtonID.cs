using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class ScrollByButtonID : MonoBehaviour, IPointerClickHandler
{
    private ChooseLevelScript chooseLevelScript;
    private GameObject father;
    private void Start() {
        chooseLevelScript = ChooseLevelScript.instance;
        father = this.transform.parent.gameObject;
    }
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        chooseLevelScript.ScrollByButtonsID(int.Parse(name));
    }
}
