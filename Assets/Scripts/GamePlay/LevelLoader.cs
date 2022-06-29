using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;

 public void LoadScene(int x)
    {
       StartCoroutine( LoadLevel(SceneManager.GetActiveScene().buildIndex + x));
    }
    public void LoadScene(string x)
    {
        StartCoroutine(LoadLevel(x));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(levelIndex);
    }
    IEnumerator LoadLevel(string x)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(x);
    }
}
