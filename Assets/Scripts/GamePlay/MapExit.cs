using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapExit : MonoBehaviour
{
    [SerializeField]
    private string sceneName;
    [SerializeField]
    private LevelLoader levelLoader;
    [SerializeField]
    private bool islastLevel;

    [SerializeField]
    private CheckGameStatus checkGame;
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            levelLoader.LoadScene(sceneName);
        }
        if (islastLevel)
        {
            checkGame.EndGame();
            checkGame.SaveData();
        }
    }
}
