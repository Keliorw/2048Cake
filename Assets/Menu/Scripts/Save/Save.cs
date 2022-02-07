using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Save : MonoBehaviour
{
    public static Save instance;
    public Sprite background;

    private void Awake() {
        instance = this;
    }
}
