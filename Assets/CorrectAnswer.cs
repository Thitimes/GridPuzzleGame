using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorrectAnswer : MonoBehaviour
{
    public static CorrectAnswer instance;
    [SerializeField]
    private bool correctAnswerVN = false;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    public bool getBool()
    {
        return correctAnswerVN;
    }
    public void setBool(bool x)
    {
        correctAnswerVN = x;
    }
}
