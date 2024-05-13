using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public GlobalData globalData;
    public GameObject gameOverUI;
    // Start is called before the first frame update
    void Start()
    {
        PauseManager.isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PauseManager.TogglePause();
        }
    }

    public void GameOver() {
        gameOverUI.SetActive(true);
        globalData.resetData();
    }

    public void StartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
