using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ExitTheGame : MonoBehaviour
{
    public Button PlayButton; //  нопка дл€ входа в игру
    public Button quitButton; //  нопка дл€ выхода из игры


    void Start()
    {
        PlayButton.onClick.AddListener(PlayGame);
        quitButton.onClick.AddListener(QuitGame);
    }
    void PlayGame()
    {
        Debug.Log("Play");
        SceneManager.LoadScene(1);
    }

    void QuitGame()
    {
        Debug.Log("Exit");
        Application.Quit();
    }
}
