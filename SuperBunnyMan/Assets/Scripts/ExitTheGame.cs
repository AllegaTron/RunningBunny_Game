using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ExitTheGame : MonoBehaviour
{
    public Button PlayButton; // ������ ��� ����� � ����
    public Button quitButton; // ������ ��� ������ �� ����


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
