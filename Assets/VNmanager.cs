using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VNmanager : MonoBehaviour
{
    public StoryScene currentScene;
    public BottomBarController bottomBar;
    public GameObject choiceSelection;
    private State state = State.IDLE;
    private bool changeScene = false;
    private string NextUnityScene;
    private bool alreadyClick;

    PlayerMovement inputActions;

    private enum State
    {
        IDLE,ANIMATE
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }
    private void OnDisable()
    {
        inputActions.Disable();
    }
    private void Awake()
    {
        alreadyClick = false;
        inputActions = new PlayerMovement();
        inputActions.UI.Click.performed += ctx => NextSentence();
    }
    void Start()
    {
        bottomBar.PlayScene(currentScene);
    }
    private void NextSentence()
    {
        if (state == State.IDLE && bottomBar.IsCompleted())
        {
            

            if (changeScene == true && bottomBar.IsLastSentence())
            {
                SceneManager.LoadScene(NextUnityScene);
            }
            else if(bottomBar.IsLastSentence()&& changeScene == false)
            {
                PlaySceneButton();   
            
            }
            if (alreadyClick)
            {
                bottomBar.skipDialogue();
            }
            bottomBar.PlayNextSentence();

        }
    }

    private void PlaySceneButton()
    {
        StartCoroutine(SwitchToButton());
    }
    public void PlayAfterButton(StoryScene scene)
    {
        changeScene = true;
        StartCoroutine(ChangeToNextScene(scene));
        NextUnityScene = currentScene.nextScene;
    }

    private IEnumerator SwitchToButton()
    {
        state = State.ANIMATE;
 
        bottomBar.Hide();
        bottomBar.currentImage.SetActive(false);
        yield return new WaitForSeconds(1f);
        bottomBar.ClearText();
        if (changeScene == false)
        {
            choiceSelection.SetActive(true);
        }
        state = State.IDLE;
    }

    private IEnumerator ChangeToNextScene(StoryScene scene)
    {
        state = State.ANIMATE;
        currentScene = scene;
        choiceSelection.SetActive(false);
        bottomBar.ClearText();
        bottomBar.Show();
        bottomBar.currentImage.SetActive(true);
        yield return new WaitForSeconds(1f);
        bottomBar.PlayScene(scene);
        state = State.IDLE;
        
        
    }
}
