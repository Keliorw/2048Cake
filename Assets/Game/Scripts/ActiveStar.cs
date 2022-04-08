using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveStar : MonoBehaviour
{
    public AnimationResultWindow MainAnimator;
    public int number;

    public void ChangeSprite()
    {
        this.transform.parent.GetComponent<Image>().sprite = this.GetComponent<Image>().sprite;
        MainAnimator.StarsAnimationStart(number);
        Destroy(this.gameObject);
    }
}