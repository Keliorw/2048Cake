using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButtonScript : MonoBehaviour
{
    public int levelID;
    private LevelsScrolling levelsScrolling;
    private Save save;
    private void Start() {
        levelsScrolling = LevelsScrolling.instance;
        save = Save.instance;
    }

    public void StartGame () {
        save.SaveGame();
        SceneManager.LoadScene(1);
    }
}
