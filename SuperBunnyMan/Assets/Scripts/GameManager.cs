using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // �������� ��������� GameManager
    public static GameManager instance;

    // ���������� ��� �������� ����������� ��������� ����
    public int score;
    public bool isGameOver;
    
    public GameObject pauseMenuUI; // ������ UI ��� ���� �����
    public GameObject playPanel; // ������ UI ��� �������
    public GameObject pausePanel; // ������ UI ��� �����
    public GameObject winPanel; // ������ UI ��� ������
    public Button resumeButton; // ������ ��� ������������� ����
    public Button pauseButton; // ������ ��� ����������������� ����
    public Button quitButton; // ������ ��� ������ � ����

    public GameObject panel100HP;
    public GameObject panel75HP;
    public GameObject panel50HP;
    public GameObject panel25HP;
    public GameObject panel0HP;
    public GameObject HUD_HP;

    private bool isPaused = false;
    public int currentHP;

    void Awake()
    {
        instance = this;

        Application.targetFrameRate = 60;
    }

    void Start()
    {
        // ������������� ����������
        score = 0;
        isGameOver = false;
        HUD_HP.SetActive(false);

        // ������ ���� ����� ��� ������� ����
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;

        // ��������� ������� �������
        resumeButton.onClick.AddListener(ResumeGame);
        pauseButton.onClick.AddListener(PauseGame);
        quitButton.onClick.AddListener(QuitToMenu);

        // ������������� �������� ������ ��������
        currentHP = 100;
        UpdateHealthHUD();
    }

    void Update()
    {
        currentHP = PlayerController.currentHealth;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    // ����� ��� ���������� ������ ��������
    public void SetHealth(int hp)
    {
        currentHP = Mathf.Clamp(hp, 0, 100);
        UpdateHealthHUD();
    }

    // ����� ��� ���������� ����������� HUD
    private void UpdateHealthHUD()
    {
        // ������ ��� ������
        panel100HP.SetActive(false);
        panel75HP.SetActive(false);
        panel50HP.SetActive(false);
        panel25HP.SetActive(false);
        panel0HP.SetActive(false);

        // �������� ��������������� ������ � ����������� �� �������� ������ ��������
        if (currentHP == 100)
        {
            panel100HP.SetActive(true);
        }
        else if (currentHP == 75)
        {
            panel75HP.SetActive(true);
        }
        else if (currentHP == 50)
        {
            panel50HP.SetActive(true);
        }
        else if (currentHP == 25)
        {
            panel25HP.SetActive(true);
        }
        else if (currentHP == 0)
        {
            panel0HP.SetActive(true);
        }
    }

    // ����� ��� ���������� �����
    public void AddScore(int points)
    {
        if (!isGameOver)
        {
            score += points;
            Debug.Log("Score: " + score);
        }
    }

    void PauseGame()
    {
        pauseMenuUI.SetActive(true); // �������� ���� �����
        pausePanel.SetActive(false); // ������ �����
        HUD_HP.SetActive(false);
        Time.timeScale = 0f; // ���������� ����� � ����
        isPaused = true;
    }

    void ResumeGame()
    {
        pauseMenuUI.SetActive(false); // ������ ���� �����
        pausePanel.SetActive(true); // �������� �����
        HUD_HP.SetActive(true);
        Time.timeScale = 1f; // ����������� ����� � ����
        isPaused = false;
        Debug.Log("Play");
    }

    void QuitToMenu()
    {
        SceneManager.LoadScene(0);
    }

    // ����� ��� ���������� ����
    public void GameOver()
    {
        isGameOver = true;
        Debug.Log("Game Over!");
        StartCoroutine(ReloadSceneAfterDelay(5));
    }

    public void Win()
    {
        StartCoroutine(ReloadSceneAfterDelay(5));
        winPanel.SetActive(true);
        Debug.Log("Win!");
    }
    IEnumerator ReloadSceneAfterDelay(int delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
