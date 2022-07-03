using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    public CheckGameStatus check;
    public GameObject image;
    void Start()
    {
        check.LoadData();
        if(check.completedGame == true)
        {
            image.SetActive(true);
        }
    }

}
