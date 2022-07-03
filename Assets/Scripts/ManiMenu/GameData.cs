using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class GameData
{
    public bool gameCompleted;

    public GameData(CheckGameStatus check)
    {
        gameCompleted = check.completedGame;
    }
}
