using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManaager : MonoBehaviour
{
    public string Answer;
    public VNmanager vnManager;
    public StoryScene story;
    private string InputString;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReadStringInput(string s)
    {
        
        InputString = s;
        Debug.Log(InputString);
    }

    public void CheckInputAnswer()
    {
        if(InputString == Answer)
        {
            //change bool condition
            vnManager.PlayAfterButton(story);
        }
        
    }
}
