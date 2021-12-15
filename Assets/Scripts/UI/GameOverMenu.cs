using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//also used for victory menu
public class GameOverMenu : MonoBehaviour
{
    public void Restart() {
        SceneManager.LoadScene("Main Scene");
    }

    public void Title() {
        SceneManager.LoadScene("Title");
    }

    public void Quit() {
        Application.Quit();
    }
}
