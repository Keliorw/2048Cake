using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationResultWindow : MonoBehaviour
{
    private Animator animator;
    [SerializeField]
    private GameObject[] Stars;

    public GameObject ActiveStar;
    public Sprite UnactiveStar;

    public bool Win;

    public void SetDefaultStars()
    {
        for (int i = 0; i < Stars.Length; i++)
        {
            Stars[i].GetComponent<Image>().sprite = UnactiveStar;
        }
    }
    public void StarsAnimationStart(int number)
    {
        if(number < LevelLoader.Difficulty && (Stars.Length) > number && this.Win)
        {
            GameObject star = Instantiate(ActiveStar, Stars[number].transform);
            ActiveStar Anim = star.GetComponent<ActiveStar>();
            number++;
            Anim.number = number;
            Anim.MainAnimator = this;
            
        }
        else
        {
            return;
        }
    }
}
