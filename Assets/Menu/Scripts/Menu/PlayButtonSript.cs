using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButtonSript : MonoBehaviour
{
    public int levelID;
    private LevelsScrolling levelsScrolling;
    private Save save;

    private void Start() {
        levelsScrolling = LevelsScrolling.instance;
        save = Save.instance;
    }

    public void StartGame () {
        if (levelsScrolling.selectedLevelID == save.levelsPassed) {
            save.levelsPassed++;
        }
    }
}
