using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VNmanager : MonoBehaviour
{
    public StoryScene currentScene;
    public BottomBarController bottomBar;
    private State state = State.IDLE;

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
    }
    private void NextSentence()
    {
        if (state == State.IDLE && bottomBar.IsCompleted())
        {
            if (bottomBar.IsLastSentence())
            {
                PlayScene(currentScene.nextScene);
            }
            bottomBar.PlayNextSentence();
        }
    }

    private void PlayScene(StoryScene scene)
    {
        StartCoroutine(SwitchScene(scene));
    }

    private IEnumerator SwitchScene(StoryScene scene)
    {
        state = State.ANIMATE;
        currentScene = scene;
        bottomBar.Hide();
        yield return new WaitForSeconds(1f);
        bottomBar.ClearText();
        //bottomBar.Show();
        //yield return new WaitForSeconds(1f);
        //bottomBar.PlayScene(scene);
        state = State.IDLE;
    }
}
