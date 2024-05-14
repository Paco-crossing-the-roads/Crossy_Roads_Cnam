using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine.AI;

public class SaveData : MonoBehaviour
{
    public GlobalData globalData;

    public List<string> GetLeaderBoardUsernames()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "pakupakudata.txt");
        List<string> usernames = new List<string>();
        if (File.Exists(filePath))
        {
            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                string[] parts = line.Split(',');
                usernames.Add(parts[0]);
            }
        }

        return usernames;
    }
    
    public List<string> GetLeaderBoardScores()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "pakupakudata.txt");
        List<string> scores = new List<string>();
        if (File.Exists(filePath))
        {
            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                string[] parts = line.Split(',');
                scores.Add(parts[1]);
            }
        }

        return scores;
    }

    public void SavePlayerData()
{
    string filePath = Path.Combine(Application.persistentDataPath, "pakupakudata.txt");
    string username = globalData.playerName;
    int score = globalData.playerScore;
    string data = username + "," + score.ToString();

    if (File.Exists(filePath))
    {
        string[] lines = File.ReadAllLines(filePath);

        bool usernameExists = false;
        int highestScore = 0;

        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i];
            string[] parts = line.Split(',');
            if (parts[0] == username)
            {
                usernameExists = true;
                int existingScore = int.Parse(parts[1]);
                if (existingScore < score)
                {
                    Debug.Log("Score already saved : " + existingScore);
                    Debug.Log("Score made : " + score);
                    highestScore = score;
                    lines[i] = username + "," + highestScore.ToString();
                }
                else
                {
                    Debug.Log("highest : " + existingScore);
                    highestScore = existingScore;
                }
            }
        }

        if (!usernameExists)
        {
            try
            {
                File.AppendAllText(filePath, "\n" + data);
            }
            catch (Exception e)
            {
                Debug.Log("Data not saved");
            }
        }
        else
        {
            try
            {
                File.WriteAllLines(filePath, lines);
            }
            catch (Exception e)
            {
                Debug.Log("Data not saved");
            }
        }
    }
    else
    {
        File.WriteAllText(filePath, data);
    }
}

}

