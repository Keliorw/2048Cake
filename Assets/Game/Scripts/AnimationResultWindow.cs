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

    public void SetDefaultStars()
    {
        for (int i = 0; i < Stars.Length; i++)
        {
            Stars[i].GetComponent<Image>().sprite = UnactiveStar;
        }
    }
    public void StarsAnimationStart()
    {
        for (int i = 0; i < Stars.Length; i++)
        {
            if(i < LevelLoader.Difficulty)
            {
                GameObject star = Instantiate(ActiveStar, Stars[i].transform);
            }
        }
    }
}
