using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveStar : MonoBehaviour
{
    public void DeleteSelf()
    {
        this.transform.parent.GetComponent<Image>().sprite = this.GetComponent<Image>().sprite;
        Destroy(this.gameObject);
    }
}
