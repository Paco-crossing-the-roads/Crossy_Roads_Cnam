using Assets.Scripts;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public GlobalData globalData;
    public GameObject gameOverUI;
    public GameObject pausePanelUI;
    public GameObject leaderboardUI;
    public TMP_InputField usernameInput;
    public SaveData saveDataScript;
    // public AudioClip gameSound;

    void Start()
    {
        if (PauseManager.IsPaused)
            PauseManager.TogglePause();
    }

    void Update()
    {
        if (pausePanelUI != null) {
            if (Input.GetKeyDown(KeyCode.P))
            {
                PauseManager.TogglePause();
            }
            else if (PauseManager.IsPaused)
            {
                pausePanelUI.SetActive(true);
            }
            else
            {
                pausePanelUI.SetActive(false);
            }
        }
    }

    public void GameOver() {
        try {
            gameOverUI.SetActive(true);
            globalData.playerHasStartedMoving = false;
            globalData.resetData();
        }
        catch(Exception e) {
            Debug.Log("From GameManager");
        }
    }

    public void Leaderboard()
    {
        leaderboardUI.SetActive(true);
    }

    public void StartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // public void GameSound() {
    //     SoundManager.instance.PlaySFX(gameSound);
    // }


    public void OnRestartButtonClick()
    {
        if (usernameInput.text != "")
        {
            globalData.playerName = usernameInput.text;
        }
        else
        {
            globalData.playerName = "unnamed";
        }
        saveDataScript.SavePlayerData();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        if (usernameInput.text != "")
        {
            globalData.playerName = usernameInput.text;
        }
        else
        {
            globalData.playerName = "unnamed";
        }
        saveDataScript.SavePlayerData();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void BackFromMainMenuToMainMenu()
    {
        if (leaderboardUI != null)
        {
            leaderboardUI.SetActive(false);
        }
    }

    public void ExitGame() {
        //For simulate in editor
        UnityEditor.EditorApplication.isPlaying = false;
        //For builed app
        Application.Quit();
    }
}
