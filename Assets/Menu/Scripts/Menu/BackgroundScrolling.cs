using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScrolling : MonoBehaviour
{
    private Animator animator;
    public static BackgroundScrolling instance;
    
    private void Awake() {
        instance = this;
    }
    public void PlayAnimation(bool side) {
        animator = GetComponent<Animator>();
        if(side){
            animator.SetTrigger("PlayRight");
        } else if(!side) {
            animator.SetTrigger("PlayLeft");
        }
    }
}
