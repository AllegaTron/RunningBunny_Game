using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Синглтон экземпляр GameManager
    public static GameManager instance;

    // Переменные для хранения глобального состояния игры
    public int score;
    public bool isGameOver;
    
    public GameObject pauseMenuUI; // Панель UI для меню паузы
    public GameObject playPanel; // Панель UI для запуска
    public GameObject pausePanel; // Панель UI для паузы
    public GameObject winPanel; // Панель UI для победы
    public Button resumeButton; // Кнопка для возобновления игры
    public Button pauseButton; // Кнопка для приостанавливания игры
    public Button quitButton; // Кнопка для выхода в меню

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
        // Инициализация переменных
        score = 0;
        isGameOver = false;
        HUD_HP.SetActive(false);

        // Скрыть меню паузы при запуске игры
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;

        // Назначить функции кнопкам
        resumeButton.onClick.AddListener(ResumeGame);
        pauseButton.onClick.AddListener(PauseGame);
        quitButton.onClick.AddListener(QuitToMenu);

        // Инициализация текущего уровня здоровья
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

    // Метод для обновления уровня здоровья
    public void SetHealth(int hp)
    {
        currentHP = Mathf.Clamp(hp, 0, 100);
        UpdateHealthHUD();
    }

    // Метод для обновления отображения HUD
    private void UpdateHealthHUD()
    {
        // Скрыть все панели
        panel100HP.SetActive(false);
        panel75HP.SetActive(false);
        panel50HP.SetActive(false);
        panel25HP.SetActive(false);
        panel0HP.SetActive(false);

        // Показать соответствующую панель в зависимости от текущего уровня здоровья
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

    // Метод для увеличения счета
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
        pauseMenuUI.SetActive(true); // Показать меню паузы
        pausePanel.SetActive(false); // Скрыть паузу
        HUD_HP.SetActive(false);
        Time.timeScale = 0f; // Остановить время в игре
        isPaused = true;
    }

    void ResumeGame()
    {
        pauseMenuUI.SetActive(false); // Скрыть меню паузы
        pausePanel.SetActive(true); // Показать паузу
        HUD_HP.SetActive(true);
        Time.timeScale = 1f; // Возобновить время в игре
        isPaused = false;
        Debug.Log("Play");
    }

    void QuitToMenu()
    {
        SceneManager.LoadScene(0);
    }

    // Метод для завершения игры
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
