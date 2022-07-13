using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VNmanager : MonoBehaviour
{
    public StoryScene currentScene;
    public LevelLoader levelLoader;
    public BottomBarController bottomBar;
    public GameObject choiceSelection;
    public GameObject boxAnswer;
    private State state = State.IDLE;
    [SerializeField]
    private bool changeScene = false;
    private string NextUnityScene;
    public string[] nextSceneToLoad;

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
       
        
        inputActions = new PlayerMovement();
        inputActions.UI.Click.performed += ctx => NextSentence();
    }
    void Start()
    {
        bottomBar.PlayScene(currentScene);
        NextUnityScene = nextSceneToLoad[Random.Range(0,nextSceneToLoad.Length)];
        bottomBar.PlayNextSentence();
    }
    private void NextSentence()
    {
        if (state == State.IDLE && bottomBar.IsCompleted())
        {

            if (changeScene == true && bottomBar.IsLastSentence())
            {
                levelLoader.LoadScene(NextUnityScene);
            }
            else if (bottomBar.IsLastSentence() && changeScene == false && boxAnswer.activeInHierarchy == false) 
            {
                PlaySceneButton();   
            
            }
            bottomBar.PlayNextSentence();
            //inputActions.UI.Click.performed += ctx => bottomBar.skipDialogue();
        }
    }

    public void PlaySceneButton()
    {
        StartCoroutine(SwitchToButton());
    }
    public void PlayAfterButton(StoryScene scene)
    {
        changeScene = true;
        StartCoroutine(ChangeToNextScene(scene));
        bottomBar.PlayNextSentence();
       // NextUnityScene = currentScene.nextScene[Random.Range(0,currentScene.nextScene.Length)];
    }
    public void ChangeToBoxAnswer()
    {
        StartCoroutine(ChangeToAnsBox());
    }
    private IEnumerator SwitchToButton()
    {
        state = State.ANIMATE;
 
        bottomBar.Hide();
        bottomBar.currentImage.SetActive(false);
        if(boxAnswer != null)
        {
            boxAnswer.SetActive(false);
        }
        yield return new WaitForSeconds(1f);
        bottomBar.ClearText();
        if (changeScene == false && boxAnswer.activeInHierarchy == false)
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
        boxAnswer.SetActive(false);
        bottomBar.ClearText();
        bottomBar.Show();
        bottomBar.currentImage.SetActive(true);
        yield return new WaitForSeconds(1f);
        bottomBar.PlayScene(scene);
        state = State.IDLE;
        
        
    }

    private IEnumerator ChangeToAnsBox()
    {
        state = State.ANIMATE;

        choiceSelection.SetActive(false);
        boxAnswer.SetActive(true);
        yield return new WaitForSeconds(1f);
        state = State.IDLE;
    }
    public void changeBackInput()
    {
        inputActions.UI.Click.performed += ctx => NextSentence();
    }
}
