using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

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

            foreach (string line in lines)
            {
                string[] parts = line.Split(',');
                if (parts[0] == username)
                {
                    usernameExists = true;
                    if (int.Parse(parts[1]) < score)
                    {
                        highestScore = score;
                    }
                    else
                    {
                        highestScore = int.Parse(parts[1]);
                    }
                }
            }
            
            if (usernameExists && highestScore < score)
            {
                string newLine = username + "," + score.ToString();
                File.WriteAllLines(filePath, lines.Select(line => line.Replace(data, newLine)).ToArray());
            }
            else if (!usernameExists)
            {
                File.AppendAllText(filePath, "\n" + data);
            }
        }
        else
        {
            File.WriteAllText(filePath, data);
        }

        //Debug.Log("Saved at : " + Application.persistentDataPath);
    }
}

