using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject gameOverMenu;
    
    public static bool isGamePaused = false;
    public static bool isGameRunning = true;
    public static bool isGameOver = false;

    public static GameManager gameManager;
    private InventoryManager inventoryManager;
    public Player player;
    private void Awake()
    {
        if (gameManager == null)
        {
            gameManager = this;
        }
    }

    private void Start()
    {
        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(false);
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGamePaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

        if (isGamePaused && pauseMenu != null)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        if (isGameOver && gameOverMenu != null)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void ResumeGame()
    {
        player.isDead = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isGamePaused = false;
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isGamePaused = true;
    }

    public bool GameRunning()
    {
        return isGameRunning;
    }

    public void GameOver()
    {
        isGameOver = true;
        isGameRunning = false;
        gameOverMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        isGameRunning = true;
        gameOverMenu.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        inventoryManager.HideDashMaskIcon();
        inventoryManager.HideBarrierMaskIcon();
        inventoryManager.HideRepelMaskIcon();
    }
}
