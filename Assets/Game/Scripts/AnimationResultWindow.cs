using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationResultWindow : MonoBehaviour
{
    private Animator animator;
    [SerializeField]
    private GameObject[] Stars;
    public void StarsAnimationStart()
    {
        Debug.Log("Появляются звёздочки");
    }
}
