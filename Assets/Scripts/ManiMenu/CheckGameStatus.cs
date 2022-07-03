using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGameStatus : MonoBehaviour
{
    public bool completedGame = false;

    private void Awake()
    {
        if (SaveGame.haveFile() == false)
        {
            SaveData();
        }
    }
    public void SaveData()
    {
        SaveGame.SaveGameData(this);
    }
    public void LoadData()
    {
        GameData data = SaveGame.LoadGameData();
        completedGame = data.gameCompleted;
    } 
    public void EndGame()
    {
        completedGame = true;
    }
}
