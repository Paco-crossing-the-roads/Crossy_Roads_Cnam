using UnityEngine;

[CreateAssetMenu(fileName = "GlobalData", menuName = "ScriptableObjects/Global Data", order = 51)]
public class GlobalData : ScriptableObject
{
    public bool playerHasStartedMoving = false;
    public string playerName;
    public int playerScore;
    public string volumeOn;

    public void resetData() {
        playerHasStartedMoving = false;
    }
}
