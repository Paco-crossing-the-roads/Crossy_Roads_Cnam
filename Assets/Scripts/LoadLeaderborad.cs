using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoadLeaderborad : MonoBehaviour
{
    public SaveData saveDataScript;
    public TextMeshProUGUI usernamesText;
    public TextMeshProUGUI scoreText;
    
    public void LoadLeaderBoard()
    {
        usernamesText.text = UsernamesConsolidation();
        scoreText.text = ScoresConsolidation();
    }

    public string UsernamesConsolidation()
    {
        string usernamesLeaderboard = "";
        int placement = 0;
        
        foreach (var username in saveDataScript.GetLeaderBoardUsernames())
        {
            placement++;
            usernamesLeaderboard += placement.ToString() + " - " + username + "\n";
        }

        return usernamesLeaderboard;
    }
    
    public string ScoresConsolidation()
    {
        string scoresLeaderboard = "";
        int placement = 0;
        
        foreach (var score in saveDataScript.GetLeaderBoardScores())
        {
            placement++;
            scoresLeaderboard += placement.ToString() + " - " + score + "\n";
        }

        return scoresLeaderboard;
    }
}
