using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputManaager : MonoBehaviour
{
    public string Answer;
    public VNmanager vnManager;
    public TMP_InputField inputString;
    public StoryScene story;
    private CorrectAnswer correctAnswer;

    private void Awake()
    {
        if (GameObject.Find("CorrectAnswer"))
        {
            correctAnswer = GameObject.Find("CorrectAnswer").GetComponent<CorrectAnswer>();
        }
        inputString.onValidateInput +=
         delegate (string s, int i, char c) { return char.ToUpper(c); };
    }
    public void CheckInputAnswer()
    {
        if(inputString.text == Answer.ToUpper())
        {
            if(correctAnswer != null)
            {
                correctAnswer.setBool(true);
            }
            //change bool condition
            vnManager.PlayAfterButton(story);
        }
        
    }
}
