using Assets.Scripts;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public GlobalData globalData;
    public GameObject gameOverUI;
    public GameObject pausePanelUI;
    // Start is called before the first frame update
    void Start()
    {
        if (PauseManager.IsPaused)
            PauseManager.TogglePause();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            PauseManager.TogglePause();
        if (PauseManager.IsPaused)
            pausePanelUI.SetActive(true);
        else
            pausePanelUI.SetActive(false);
    }

    public void GameOver() {
        //try {
            gameOverUI.SetActive(true);
            globalData.resetData();
        /*}
        catch(Exception e) {
            Debug.Log("From GameManager");
        }*/
    }

    public void StartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void MainMenu() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void ExitGame() {
        //For simulate in editor
        UnityEditor.EditorApplication.isPlaying = false;
        //For builed app
        Application.Quit();
    }
}
