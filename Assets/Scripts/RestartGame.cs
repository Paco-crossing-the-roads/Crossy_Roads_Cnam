using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGameButton : MonoBehaviour
{
    public TMP_InputField usernameInput;
    public GlobalData globalData;
    public SaveData saveDataScript;
    
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
}